using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionPrivateMethodsUpdateChildren<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPrivateMethodsUpdateChildren(BuildPass<T> pass) : base(pass, true) { }
        #endregion

        #region Properties
        private StringBuilder InnerContainer = new StringBuilder();
        #endregion

        #region ISection Methods
        public override string Build()
        {
            if (!Navigations.Any() && !CollectionNavigations.Any())
                return string.Empty;

            AddUpdateChildren();

            if (InnerContainer.Length == 0)
            {
                //return string.Empty;
            }
            else
                InnerContainer.Remove(InnerContainer.Length - 2, 2);

            Add(Tab(2), string.Format("private void UpdateChildren({0} template)", EntityName));
            Add(Tab(2), "{");

            Add(false, InnerContainer.ToString());

            Add(Tab(2), "}", NewLine());

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddUpdateChildren()
        {
            Navigations.Each(p => AddUpdateChild(p));
            CollectionNavigations.Each(p => AddUpdateChildCollections(p));
        }

        private void AddUpdateChild(NavigationProperty property)
        {
            if (property.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                return;

            if (Settings.IsIgnoreRootChildren && Pass.Roots.Contains(property.Name))
                return;

            var properties = GetPropertiesForComparison(property.ToEndMember.Name);

            if (properties.Count() == 0)
            {
                Pass.Builder.BuilderWarning(Pass.BuildSection.ToString(), GetWarningMessage(property));
                return;
            }

            var propertyName = Pass.Code.Escape(property);
            var equalityString = CreateKeyEqualityString(properties, propertyName);

            InnerContainer.Append(AddReturn(Tab(3), string.Format("if(template.{0} != null && {1})", propertyName, equalityString)));
            InnerContainer.Append(AddReturn(Tab(4), string.Format("{0}.Update(template.{0}, {1}, {1}, {1});", propertyName, Settings.IsRecursiveCrud.ToString().LowerCaseFirstCharacter())));
            InnerContainer.Append(AddReturn());
        }

        private void AddUpdateChildCollections(NavigationProperty property)
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
            var equalityString = CreateKeyEqualityStringCollections(properties);

            InnerContainer.Append(AddReturn(Tab(3), pluralizedChild, ".ToList().Each(c =>"));
            InnerContainer.Append(AddReturn(Tab(3), "{"));
            InnerContainer.Append(AddReturn(Tab(4), "var childTemplate = template"));
            InnerContainer.Append(AddReturn(Tab(9), ".", pluralizedChild));
            InnerContainer.Append(AddReturn(Tab(9), string.Format(".FirstOrDefault(tc => {0});", equalityString)));
            InnerContainer.Append(AddReturn());
            InnerContainer.Append(AddReturn(Tab(4), "if(childTemplate != null)"));
            InnerContainer.Append(AddReturn(Tab(5), string.Format("c.Update(childTemplate, {0}, {0}, {0});", Settings.IsRecursiveCrud.ToString().LowerCaseFirstCharacter())));
            InnerContainer.Append(AddReturn(Tab(3), "});"));
            InnerContainer.Append(AddReturn());
        }

        private string CreateKeyEqualityString(IEnumerable<string> properties, string property)
        {
            if (properties.Count() == 1)
                return string.Format("template.{0}.{1} == {0}.{1}", property, properties.First());
            else
                return string.Join(" && ", properties.Select(p => "template." + property + "." + p + " == " + property + "." + p));
        }

        private string CreateKeyEqualityStringCollections(IEnumerable<string> properties)
        {
            if (properties.Count() == 1)
                return string.Format("tc.{0} == c.{0}", properties.First());
            else
                return string.Join(" && ", properties.Select(p => "tc." + p + " == c." + p));
        }

        private string GetWarningMessage(NavigationProperty property)
        {
            return string.Format("Unable to create child operation within generated code method \"UpdateChildren()\" for entity \"{0}\" and navigation property \"{1}\".  Unable to find key(s) or Identity column for {2}.", EntityName, Pass.Code.Escape(property), property.ToEndMember.Name);
        }
        #endregion
    }
}
