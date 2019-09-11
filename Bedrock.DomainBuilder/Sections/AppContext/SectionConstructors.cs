using System;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.AppContext
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
            AddConstructors();
            Add(Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddConstructors()
        {
            Add(Tab(2), string.Format("public {0}{1}Context", Settings.Domain, SchemaCamelCased));
            Add(Tab(2), "(");

            EntityTypes.Each(e =>
            {
                var entityName = Pass.Code.Escape(e);
                var entityNamePluralized = Pluralize(entityName).LowerCaseFirstCharacter();
                var format = !Settings.IsIncludePrimaryKey ? "Lazy<IRepositoryOrmId<{0}, {1}>> {2}," : "Lazy<IRepositoryOrm<{0}>> {2},";

                Add(Tab(3), string.Format(format, entityName, GetPrimaryKeyType(e), entityNamePluralized));
            });

            Container.Remove(Container.Length - 3, 1);

            Add(Tab(2), ")");
            Add(Tab(2), "{");

            EntityTypes.Each(e =>
            {
                var entityNamePluralized = Pluralize(Pass.Code.Escape(e));
                var entityNamePluralizedLowered = entityNamePluralized.LowerCaseFirstCharacter();

                Add(Tab(3), string.Format("Lazy{0} = {1};", entityNamePluralized, entityNamePluralizedLowered));
            });

            Add(Tab(2), "}");
        }
        #endregion
    }
}
