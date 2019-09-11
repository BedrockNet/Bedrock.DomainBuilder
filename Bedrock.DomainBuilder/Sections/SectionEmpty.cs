using System;

namespace Bedrock.DomainBuilder.Sections
{
    public class SectionEmpty<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionEmpty(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            return string.Empty;
        }
        #endregion
    }
}
