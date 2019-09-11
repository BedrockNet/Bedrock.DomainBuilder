using System;

namespace Bedrock.DomainBuilder.Sections.Context
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
            Add(false, Tab(), string.Format("public partial class {0}{1}Context : {2}", Settings.Domain, SchemaCamelCased, Settings.ContextType));
            Add(false, NewLine(), Tab(), "{");

            return base.Build();
        }
        #endregion
    }
}
