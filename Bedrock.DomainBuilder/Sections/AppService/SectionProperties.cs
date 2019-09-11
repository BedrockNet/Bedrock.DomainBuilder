using System;

namespace Bedrock.DomainBuilder.Sections.AppService
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
            Add(Tab(2), "#region Properties");
            BuildProperties();
            Add(false, Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildProperties()
        {
            Add(Tab(2), string.Format("protected I{0}Service {0}Service {{ get; set; }}", EntityName));
        }
        #endregion
    }
}
