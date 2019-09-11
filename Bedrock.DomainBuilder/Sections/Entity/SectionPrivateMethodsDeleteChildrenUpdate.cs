using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionPrivateMethodsDeleteChildrenUpdate<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPrivateMethodsDeleteChildrenUpdate(BuildPass<T> pass) : base(pass, true) { }
        #endregion

        #region Properties
        private StringBuilder InnerContainer = new StringBuilder();
        #endregion

        #region ISection Methods
        public override string Build()
        {
            if (!Navigations.Any() && !CollectionNavigations.Any())
                return string.Empty;

            AddDeleteChildren();

            if (InnerContainer.Length == 0)
            {
                //return string.Empty;
            }
            else
                InnerContainer.Remove(InnerContainer.Length - 2, 2);

            Add(Tab(2), string.Format("private void DeleteChildrenForUpdate({0} template)", EntityName));
            Add(Tab(2), "{");

            Add(false, InnerContainer.ToString());

            Add(Tab(2), "}");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddDeleteChildren()
        {
            Navigations.Each(p => AddChild(p));
            CollectionNavigations.Each(p => AddDeleteChildCollections(p));
        }

        private void AddChild(NavigationProperty property)
        {
            if (property.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                return;

            if (Settings.IsIgnoreRootChildren && Pass.Roots.Contains(property.Name))
                return;

            var propertyName = Pass.Code.Escape(property);
            var propertyNamePluralized = Pluralize(propertyName);

            InnerContainer.Append(AddReturn(Tab(3), string.Format("if({0} != null && template.{0} == null)", propertyName)));
            InnerContainer.Append(AddReturn(Tab(4), string.Format("AppContext.{0}.Remove({1});", propertyNamePluralized, propertyName)));
            InnerContainer.Append(AddReturn());
        }

        private void AddDeleteChildCollections(NavigationProperty property)
        {
            if (Settings.IsIgnoreRootChildren && Pass.Roots.Contains(property.ToEndMember.Name))
                return;

            var properties = GetPropertiesForComparison(property.ToEndMember.Name);

            if (properties.Count() == 0)
            {
                Pass.Builder.BuilderWarning(Pass.BuildSection.ToString(), GetWarningMessage(property));
                return;
            }

            var pluralizedChild = Pass.Code.Escape(property);
            var equalityString = CreateKeyEqualityString(properties);

            InnerContainer.Append(AddReturn(Tab(3), string.Format("var {0}ToDelete = {1}.Where(s => !template.{1}.Any(t => {2})).ToList();", pluralizedChild.LowerCaseFirstCharacter(), pluralizedChild, equalityString)));
            InnerContainer.Append(AddReturn(Tab(3), pluralizedChild.LowerCaseFirstCharacter(), "ToDelete.Each(c => AppContext.", pluralizedChild, ".Remove(c));"));
            InnerContainer.Append(AddReturn());
        }

        private string CreateKeyEqualityString(IEnumerable<string> properties)
        {
            if (properties.Count() == 1)
                return string.Format("t.{0} == s.{0}", properties.First());
            else
                return string.Join(" && ", properties.Select(p => "t." + p + " == s." + p));
        }

        private string GetWarningMessage(NavigationProperty property)
        {
            return string.Format("Unable to create child operation within generated code method \"DeleteChildrenForUpdate()\" for entity \"{0}\" and navigation property \"{1}\".  Unable to find key(s) or Identity column for {2}.", EntityName, Pass.Code.Escape(property), property.ToEndMember.Name);
        }
        #endregion
    }
}
