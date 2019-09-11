using System;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.AppContext
{
    public class SectionProperties<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionProperties(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), "#region Properties");
            BuildProperties();
            Add(Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildProperties()
        {
            EntityTypes.Each((e, i) =>
            {
                var entityName = Pass.Code.Escape(e);
                var entityNamePluralized = Pluralize(entityName);
                var format = !Settings.IsIncludePrimaryKey ? "Lazy<IRepositoryOrmId<{0}, {1}>> Lazy{2} {{ get; set; }}" : "Lazy<IRepositoryOrm<{0}>> Lazy{2} {{ get; set; }}";
                var newLine = ++i != EntityTypes.Count() ? NewLine() : null;

                Add(Tab(2), string.Format(format, entityName, GetPrimaryKeyType(e), entityNamePluralized), newLine);
            });
        }
        #endregion
    }
}
