using System;

namespace Bedrock.DomainBuilder.Sections.Mapping
{
    public class SectionClass<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionClass(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(false, Tab(), string.Format("public partial class {0}Map : IEntityTypeConfiguration<{1}>", EntityNameSingularized, EntityNameSingularized));
            Add(false, NewLine(), Tab(), "{");
        }

        protected override void BuildFramework()
        {
            Add(false, Tab(), string.Format("public partial class {0}Map : EntityTypeConfiguration<{1}>", EntityNameSingularized, EntityNameSingularized));
            Add(false, NewLine(), Tab(), "{");
        }
        #endregion
    }
}
