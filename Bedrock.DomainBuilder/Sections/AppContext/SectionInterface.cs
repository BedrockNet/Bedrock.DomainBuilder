using System;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.AppContext
{
    public class SectionInterface<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionInterface(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(), string.Format("public interface I{0}{1}Context : ISessionAware", Settings.Domain, SchemaCamelCased));
            Add(Tab(), "{");
            AddProperties();
            Add(Tab(), "}");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddProperties()
        {
            Add(Tab(2), "#region Properties");

            EntityTypes.Each((e, i) =>
            {
                var entityName = Pass.Code.Escape(e);
                var entityNamePluralized = Pluralize(entityName);
                var format = !Settings.IsIncludePrimaryKey ? "IRepositoryOrmId<{0}, {1}> {2} {{ get; }}" : "IRepositoryOrm<{0}> {2} {{ get; }}";
                var newLine = ++i != EntityTypes.Count() ? NewLine() : null;

                Add(Tab(2), string.Format(format, entityName, GetPrimaryKeyType(e), entityNamePluralized), newLine);
            });

            Add(Tab(2), "#endregion");
        }
        #endregion
    }
}
