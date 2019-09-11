using System;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionPublicMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPublicMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(Tab(2), "#region Public Methods");
            Add(Tab(2), "#endregion");
        }

        protected override void BuildFramework()
        {
            Add(Tab(2), "#region Public Methods");
            AddCrud();
            Add(Tab(2), "#endregion");
        }
        #endregion

        #region Private Methods
        private void AddCrud()
        {
            if (Settings.IsIncludeCrudEntity && !IsEnumeration)
            {
                AddCreate();
                AddUpdate();
                AddDelete();
            }
        }

        private void AddCreate()
        {
            var createBuilder = new SectionPublicMethodsCreate<T>(Pass);
            Container.Append(createBuilder.Build());
        }

        private void AddUpdate()
        {
            var updateBuilder = new SectionPublicMethodsUpdate<T>(Pass);
            Container.Append(updateBuilder.Build());
        }

        private void AddDelete() { }
        #endregion
    }
}
