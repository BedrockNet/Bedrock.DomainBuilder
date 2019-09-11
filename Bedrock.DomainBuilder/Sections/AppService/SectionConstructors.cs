using System;

namespace Bedrock.DomainBuilder.Sections.AppService
{
    public class SectionConstructors<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionConstructors(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(Tab(2), "#region Constructors");
            Add(Tab(2), string.Format("public {0}ApplicationService(I{0}Service {1}Service, BedrockConfiguration bedrockConfiguration) : base(bedrockConfiguration, {1}Service)", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("{0}Service = {1}Service;", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "}");
            Add(Tab(2), "#endregion");
        }

        protected override void BuildFramework()
        {
            Add(Tab(2), "#region Constructors");
            Add(Tab(2), string.Format("public {0}ApplicationService(I{0}Service {1}Service) : base({1}Service)", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("{0}Service = {1}Service;", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "}");
            Add(Tab(2), "#endregion");
        }
        #endregion
    }
}
