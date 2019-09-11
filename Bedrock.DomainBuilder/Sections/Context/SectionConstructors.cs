using System;

namespace Bedrock.DomainBuilder.Sections.Context
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

            CoreBuildConstructorParameterless();
            CoreBuildConstrcutorConnectionString();
            CoreBuildConstructorOptions();

            Add(Tab(2), "#endregion");
        }

        protected override void BuildFramework()
        {
            Add(Tab(2), "#region Constructors");

            FrameworkBuildConstructorStatic();
            FrameworkBuildConstructorParameterless();
            FrameworkBuildConstrcutorConnectionString();

            Add(Tab(2), "#endregion");
        }
        #endregion       

        #region Private Methods (Core)
        private void CoreBuildConstructorParameterless()
        {
            Add(Tab(2), string.Format("public {0}{1}Context(IDomainEventDispatcher domainEventDispatcher, BedrockConfiguration bedrockConfiguration) : base(domainEventDispatcher, bedrockConfiguration) {{ }}", Settings.Domain, SchemaCamelCased), NewLine());
        }

        private void CoreBuildConstrcutorConnectionString()
        {
            Add(Tab(2), string.Format("public {0}{1}Context(IDomainEventDispatcher domainEventDispatcher, string nameOrConnectionString, BedrockConfiguration bedrockConfiguration) : base(nameOrConnectionString, domainEventDispatcher, bedrockConfiguration) {{ }}", Settings.Domain, SchemaCamelCased), NewLine());
        }

        private void CoreBuildConstructorOptions()
        {
            Add(Tab(2), string.Format("public {0}{1}Context(IDomainEventDispatcher domainEventDispatcher, DbContextOptions options, BedrockConfiguration bedrockConfiguration) : base(options, domainEventDispatcher, bedrockConfiguration) {{ }}", Settings.Domain, SchemaCamelCased));
        }
        #endregion

        #region Private Methods (Framework)
        private void FrameworkBuildConstructorStatic()
        {
            Add(Tab(2), string.Format("static {0}{1}Context()", Settings.Domain, SchemaCamelCased));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("Database.SetInitializer<{0}{1}Context>(null);", Settings.Domain, SchemaCamelCased));
            Add(Tab(2), "}", NewLine());
        }

        private void FrameworkBuildConstructorParameterless()
        {
            Add(Tab(2), string.Format("public {0}{1}Context() {{ }}", Settings.Domain, SchemaCamelCased), NewLine());
        }

        private void FrameworkBuildConstrcutorConnectionString()
        {
            Add(Tab(2), string.Format("public {0}{1}Context(string nameOrConnectionString) : base(nameOrConnectionString) {{ }}", Settings.Domain, SchemaCamelCased));
        }
        #endregion
    }
}
