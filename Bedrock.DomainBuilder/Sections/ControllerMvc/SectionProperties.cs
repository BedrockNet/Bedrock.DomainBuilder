using System;

namespace Bedrock.DomainBuilder.Sections.ControllerMvc
{
    public class SectionProperties<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionProperties(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            BuildProtectedProperties();
            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildProtectedProperties()
        {
            Add(Tab(2), "#region Protected Properties");
            Add(false, Tab(2), "#endregion", NewLine());
        }
        #endregion
    }
}
