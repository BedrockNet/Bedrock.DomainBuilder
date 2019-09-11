using System;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.AppContext
{
    public class SectionInterfaceProperties<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionInterfaceProperties(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), string.Format("#region {0} Properties", Settings.Domain));
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
                var format = !Settings.IsIncludePrimaryKey ? "public IRepositoryOrmId<{0}, {1}> {2}" : "public IRepositoryOrm<{0}> {2}";
                var newLine = ++i != EntityTypes.Count() ? NewLine() : null;

                Add(Tab(2), string.Format(format, entityName, GetPrimaryKeyType(e), entityNamePluralized));
                Add(Tab(2), "{");
                Add(Tab(3), "get");
                Add(Tab(3), "{");
                Add(Tab(4), string.Format("Lazy{0}.Value.Enlist(Session);", entityNamePluralized));
                Add(Tab(4), string.Format("return Lazy{0}.Value;", entityNamePluralized));
                Add(Tab(3), "}");
                Add(Tab(2), "}", newLine);
            });
        }
        #endregion
    }
}
