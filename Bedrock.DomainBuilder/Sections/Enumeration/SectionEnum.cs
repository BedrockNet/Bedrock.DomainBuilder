using System;

namespace Bedrock.DomainBuilder.Sections.Enumeration
{
    public class SectionEnum<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionEnum(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(false, Tab(), string.Format("public enum {0}", EntityName));
            Add(false, NewLine(), Tab(), "{");

            return base.Build();
        }
        #endregion
    }
}
