using System;

namespace Bedrock.DomainBuilder.Sections.AppContext
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
            return string.Empty;
        }
        #endregion
    }
}
