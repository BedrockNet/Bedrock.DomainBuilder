using System;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionPublicMethodsCreate<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPublicMethodsCreate(BuildPass<T> pass) : base(pass, true) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), string.Format("public static {0} Create({0} template)", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("return Create(template, {0});", Settings.IsRecursiveCrud.ToString().ToLower()));
            Add(Tab(2), "}", NewLine());

            Add(Tab(2), string.Format("public static {0} Create({0} template, bool isAddChildren)", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), "var returnValue = new ", EntityName, "()");
            Add(Tab(3), "{");

            AddFieldAssignments();

            Add(Tab(3), "};", NewLine());

            if (Navigations.Any() || CollectionNavigations.Any())
            {
                Add(Tab(3), "if(isAddChildren)");
                Add(Tab(4), "AddChildrenForCreate(returnValue, template);", NewLine());
            }

            //if (IsRoot)
            //	Add(Tab(3), "context.", EntityNamePluralized, ".Add(returnValue);", NewLine());

            Add(Tab(3), "return returnValue;");
            Add(Tab(2), "}", NewLine());

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddFieldAssignments()
        {
            foreach (var property in GetPropertyNamesWithCommonKey())
                AddFieldAssignment(property);

            Container.Remove(Container.Length - 3, 1);
        }

        private void AddFieldAssignment(string property)
        {
            Add(Tab(4), Pass.Code.Escape(property), " = ", "template.", property, ",");
        }
        #endregion
    }
}
