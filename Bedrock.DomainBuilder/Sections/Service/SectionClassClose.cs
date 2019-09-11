using System;

namespace Bedrock.DomainBuilder.Sections.Service
{
    public class SectionClassClose<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionClassClose(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(false, Tab(), "}");
            return base.Build();
        }
        #endregion
    }
}
