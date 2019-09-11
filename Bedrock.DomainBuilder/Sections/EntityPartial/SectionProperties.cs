using System;

namespace Bedrock.DomainBuilder.Sections.EntityPartial
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
            Add(Tab(2), "#endregion");

            return base.Build();
        }
        #endregion
    }
}
