using System;

namespace Bedrock.DomainBuilder.Sections.EntityPartial
{
    public class SectionPrivateMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPrivateMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), "#region Private Methods");
            AddCrud();
            Add(false, Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddCrud()
        {
            Add(Tab(2), "partial void Initialize() { }", NewLine());
            Add(Tab(2), "partial void ValidateInternal(ValidationContext validationContext) { }");
        }
        #endregion
    }
}
