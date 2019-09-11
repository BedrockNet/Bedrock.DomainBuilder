using System;

namespace Bedrock.DomainBuilder.Sections.AutoMapperProfile
{
    public class SectionConstructors<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionConstructors(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), "#region Constructors");
            Add(Tab(2), string.Format("public {0}Profile()", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("AutoMapperConfiguration.MapperConfigurationExpression.CreateMap<{0}, {0}Contract>();", EntityName));
            Add(Tab(3), string.Format("AutoMapperConfiguration.MapperConfigurationExpression.CreateMap<{0}Contract, {0}>();", EntityName));
            Add(Tab(2), "}");
            Add(Tab(2), "#endregion");

            return base.Build();
        }
        #endregion
    }
}
