using System;

namespace Bedrock.DomainBuilder.Sections.AppContext
{
    public class SectionClass<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionClass(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(false, Tab(), string.Format("public partial class {0}{1}Context : SessionAwareBase, I{0}{1}Context", Settings.Domain, SchemaCamelCased));
            Add(false, NewLine(), Tab(), "{");

            return base.Build();
        }
        #endregion
    }
}
