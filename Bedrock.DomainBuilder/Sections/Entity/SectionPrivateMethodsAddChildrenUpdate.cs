using System;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionPrivateMethodsAddChildrenUpdate<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPrivateMethodsAddChildrenUpdate(BuildPass<T> pass) : base(pass, true) { }
        #endregion

        #region Properties
        private StringBuilder InnerContainer = new StringBuilder();
        #endregion

        #region ISection Methods
        public override string Build()
        {
            if (!Navigations.Any() && !CollectionNavigations.Any())
                return string.Empty;

            AddChildren();

            if (InnerContainer.Length == 0)
            {
                //return string.Empty;
            }
            else
                InnerContainer.Remove(InnerContainer.Length - 2, 2);

            Add(Tab(2), string.Format("private void AddChildrenForUpdate({0} template)", EntityName));
            Add(Tab(2), "{");

            Add(false, InnerContainer.ToString());

            Add(Tab(2), "}", NewLine());

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddChildren()
        {
            Navigations.Each(p => AddChild(p));
            CollectionNavigations.Each(p => AddChildCollections(p));
        }

        private void AddChild(NavigationProperty property)
        {
            if (property.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                return;

            if (Settings.IsIgnoreRootChildren && Pass.Roots.Contains(property.Name))
                return;

            var updateProperty = GetComparisonPropertyForUpdate(property.ToEndMember.Name);
            var pluralizedChild = Pass.Code.Escape(property);

            if (updateProperty.Item1 == null)
            {
                Pass.Builder.BuilderWarning(Pass.BuildSection.ToString(), GetWarningMessage(property));
                return;
            }

            var propertyName = Pass.Code.Escape(property);

            InnerContainer.Append(AddReturn(Tab(3), string.Format("if(template.{0} != null && template.{0}.{1} == {2})", propertyName, updateProperty.Item1, updateProperty.Item2)));
            InnerContainer.Append(AddReturn(Tab(4), string.Format("{0} = {0}.Create(template.{0}, {1});", propertyName, Settings.IsRecursiveCrud.ToString().LowerCaseFirstCharacter())));
            InnerContainer.Append(AddReturn());
        }

        private void AddChildCollections(NavigationProperty property)
        {
            if (Settings.IsIgnoreRootChildren && Pass.Roots.Contains(property.ToEndMember.Name))
                return;

            var updateProperty = GetComparisonPropertyForUpdate(property.ToEndMember.Name);
            var pluralizedChild = Pass.Code.Escape(property);

            if (updateProperty.Item1 == null)
            {
                Pass.Builder.BuilderWarning(Pass.BuildSection.ToString(), GetWarningMessage(property));
                return;
            }

            InnerContainer.Append(AddReturn(Tab(3), "template.", pluralizedChild, ".Where(c => c.", updateProperty.Item1, " == ", updateProperty.Item2, ").Each(c =>"));
            InnerContainer.Append(AddReturn(Tab(3), "{"));
            InnerContainer.Append(AddReturn(Tab(4), string.Format("var child = {0}.Create(c, {1});", Pass.Code.Escape(property.ToEndMember), Settings.IsRecursiveCrud.ToString().LowerCaseFirstCharacter())));
            InnerContainer.Append(AddReturn(Tab(4), pluralizedChild, ".Add(child);"));
            InnerContainer.Append(AddReturn(Tab(3), "});"));
            InnerContainer.Append(AddReturn());
        }

        private string GetWarningMessage(NavigationProperty property)
        {
            return string.Format("Unable to create child operation within generated code method \"AddChildrenForUpdate()\" for entity \"{0}\" and navigation property \"{1}\".  Unable to find single key or Identity column for {2}.", EntityName, Pass.Code.Escape(property), property.ToEndMember.Name);
        }
        #endregion
    }
}
