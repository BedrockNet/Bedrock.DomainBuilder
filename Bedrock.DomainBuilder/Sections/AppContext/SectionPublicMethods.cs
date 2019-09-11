using System;

namespace Bedrock.DomainBuilder.Sections.AppContext
{
    public class SectionPublicMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPublicMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), "#region Public Methods");
            BuildMethods();
            Add(Tab(2), "#endregion");

            return Container.ToString();
        }
        #endregion

        #region Private Methods
        private void BuildMethods()
        {
            BuildGetTypesMethod();
        }

        private void BuildGetTypesMethod()
        {
            Add(Tab(2), "public static Type[] GetTypes()");
            Add(Tab(2), "{");
            Add(Tab(3), "return new Type[]");
            Add(Tab(3), "{");

            EntityTypes.Each(e =>
            {
                var entityName = Pass.Code.Escape(e);
                Add(Tab(4), string.Format("typeof({0}),", entityName));
            });

            Container.Remove(Container.Length - 3, 1);

            Add(Tab(3), "};");
            Add(Tab(2), "}");
        }
        #endregion
    }
}
