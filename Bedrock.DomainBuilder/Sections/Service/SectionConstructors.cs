using System;

namespace Bedrock.DomainBuilder.Sections.Service
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
            Add(Tab(2), string.Format("public {0}Service(I{1}{2}Context context, BedrockConfiguration bedrockConfiguration) : base(bedrockConfiguration, context) {{ }}", EntityName, Settings.Domain.ToString(), SchemaCamelCased));
            Add(Tab(2), "#endregion");
        }

        protected override void BuildFramework()
        {
            Add(Tab(2), "#region Constructors");
            Add(Tab(2), string.Format("public {0}Service(I{1}{2}Context context) : base(context) {{ }}", EntityName, Settings.Domain.ToString(), SchemaCamelCased));
            Add(Tab(2), "#endregion");
        }
        #endregion
    }
}
