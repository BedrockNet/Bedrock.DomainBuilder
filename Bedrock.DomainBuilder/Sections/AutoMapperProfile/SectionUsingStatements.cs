using System;

namespace Bedrock.DomainBuilder.Sections.AutoMapperProfile
{
    public class SectionUsingStatements<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionUsingStatements(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(string.Format("using {0};", Settings.NamespaceEntity));
            Add(string.Format("using {0};", Settings.NamespaceContract), NewLine());
            Add(string.Format("using {0}.Mapper.AutoMapper;", Settings.NamespaceShared));
            Add("using AM = AutoMapper;");

            return base.Build();
        }
        #endregion
    }
}
