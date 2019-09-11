using System;

namespace Bedrock.DomainBuilder.Sections.Enumeration
{
    public class SectionNamespaceClose<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionNamespaceClose(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Container.Append("}");
            return base.Build();
        }
        #endregion
    }
}
