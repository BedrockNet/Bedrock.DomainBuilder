using System;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionPublicMethodsUpdate<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPublicMethodsUpdate(BuildPass<T> pass) : base(pass, true) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), string.Format("public void Update({0} template)", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("Update(template, {0}, {0}, {0});", Settings.IsRecursiveCrud.ToString().ToLower()));
            Add(Tab(2), "}", NewLine());
            Add(Tab(2), string.Format("public void Update({0} template, bool isUpdateChildren, bool isAddChildren, bool isDeleteChildren)", EntityName));
            Add(Tab(2), "{");

            AddFieldAssignments();

            if (Navigations.Any() || CollectionNavigations.Any())
            {
                Add();
                Add(Tab(3), "if(isUpdateChildren)");
                Add(Tab(4), "UpdateChildren(template);", NewLine());
                Add(Tab(3), "if(isAddChildren)");
                Add(Tab(4), "AddChildrenForUpdate(template);", NewLine());
                Add(Tab(3), "if(isDeleteChildren)");
                Add(Tab(4), "DeleteChildrenForUpdate(template);");
            }

            //if (IsRoot)
            //	Add(NewLine(), Tab(3), "context.", EntityNamePluralized, ".Update(this);");

            Add(Tab(2), "}");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddFieldAssignments()
        {
            foreach (var property in GetPropertyNamesWithCommonKey())
                AddFieldAssignment(property);
        }

        private void AddFieldAssignment(string property)
        {
            Add(Tab(3), Pass.Code.Escape(property), " = ", "template.", property, ";");
        }
        #endregion
    }
}
