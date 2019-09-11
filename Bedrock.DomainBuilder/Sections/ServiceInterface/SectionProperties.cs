using System;

namespace Bedrock.DomainBuilder.Sections.ServiceInterface
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
            Add(Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildProperties() { }
        #endregion
    }
}
