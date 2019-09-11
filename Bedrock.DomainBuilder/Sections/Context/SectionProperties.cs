using System;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.Context
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
            BuildPublicProperties();
            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildPublicProperties()
        {
            Add(Tab(2), "#region Public Properties");

            EntityTypes.Each((e, i) =>
            {
                var entity = Pass.Code.Escape(e);
                var newLine = ++i != EntityTypes.Count() ? NewLine() : null;

                Add(Tab(2), string.Format("public DbSet<{0}> {1} {{ get; set; }}", Singularize(entity), Pluralize(entity)), newLine);
            });

            Add(false, Tab(2), "#endregion", NewLine());
        }
        #endregion
    }
}
