using System;

namespace Bedrock.DomainBuilder.Sections.ControllerApi
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
            Add(Tab(2), string.Format("protected I{0}ApplicationService {0}Service {{ get; set; }}", EntityName));
            Add(false, Tab(2), "#endregion", NewLine());
        }
        #endregion
    }
}
