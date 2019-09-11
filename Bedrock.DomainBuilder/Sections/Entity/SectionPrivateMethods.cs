using System;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionPrivateMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPrivateMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(Tab(2), "#region Private Methods");
            AddCrud();
            Add(false, Tab(2), "#endregion");
        }

        protected override void BuildFramework()
        {
            Add(Tab(2), "#region Private Methods");
            AddCrud();
            Add(false, Tab(2), "#endregion");
        }
        #endregion

        #region Private Methods
        private void AddCrud()
        {
            AddInitialize();

            if (Settings.IsIncludeCrudEntity && !IsEnumeration)
            {
                Add();

                AddAddChildrenForCreate();
                AddUpdateChildren();
                AddAddChildrenForUpdate();
                AddDeleteChildrenForUpdate();
            }
        }

        private void AddInitialize()
        {
            Add(Tab(2), "partial void Initialize();", NewLine());
            Add(Tab(2), "partial void ValidateInternal(ValidationContext validationContext);");
        }

        private void AddAddChildrenForCreate()
        {
            var builder = new SectionPrivateMethodsAddChildrenCreate<T>(Pass);
            Container.Append(builder.Build());
        }

        private void AddUpdateChildren()
        {
            var builder = new SectionPrivateMethodsUpdateChildren<T>(Pass);
            Container.Append(builder.Build());
        }

        private void AddAddChildrenForUpdate()
        {
            var builder = new SectionPrivateMethodsAddChildrenUpdate<T>(Pass);
            Container.Append(builder.Build());
        }

        private void AddDeleteChildrenForUpdate()
        {
            var builder = new SectionPrivateMethodsDeleteChildrenUpdate<T>(Pass);
            Container.Append(builder.Build());
        }
        #endregion
    }
}
