using System;

namespace Bedrock.DomainBuilder.Sections.Context
{
    public class SectionProtectedMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionProtectedMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(Tab(2), "#region DbContext Members");
            Add(Tab(2), "protected override void OnModelCreating(ModelBuilder modelBuilder)");
            Add(Tab(2), "{");
            Add(Tab(3), "IgnoreProperties(modelBuilder);", NewLine());

            Add(Tab(3), $"modelBuilder.ApplyConfigurationsFromAssembly(typeof({Settings.Domain}{SchemaCamelCased}Context).Assembly);");

			Add();
			Add(Tab(3), "base.OnModelCreating(modelBuilder);");

			Add(Tab(2), "}");
            Add(Tab(2), "#endregion");
        }

        protected override void BuildFramework()
        {
            Add(Tab(2), "#region DbContext Members");
            Add(Tab(2), "protected override void OnModelCreating(DbModelBuilder modelBuilder)");
            Add(Tab(2), "{");
            Add(Tab(3), "IgnoreProperties(modelBuilder);", NewLine());

            EntityTypes.Each(e =>
            {
                var entity = Pass.Code.Escape(e);
                Add(Tab(3), string.Format("modelBuilder.Configurations.Add(new {0}Map());", Singularize(entity)));
            });

            Add(Tab(2), "}");
            Add(Tab(2), "#endregion");
        }
        #endregion
    }
}
