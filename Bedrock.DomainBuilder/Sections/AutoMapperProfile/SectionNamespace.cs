using System;

namespace Bedrock.DomainBuilder.Sections.AutoMapperProfile
{
    public class SectionNamespace<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionNamespace(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(string.Format("namespace {0}.Mapping.AutoMapper", Settings.NamespaceInfrastructure));
            Add(false, "{");

            return base.Build();
        }
        #endregion
    }
}
