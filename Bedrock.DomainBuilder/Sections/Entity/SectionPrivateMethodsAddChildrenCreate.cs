using System;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionPrivateMethodsAddChildrenCreate<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPrivateMethodsAddChildrenCreate(BuildPass<T> pass) : base(pass, true) { }
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

            Add(Tab(2), string.Format("private static void AddChildrenForCreate({0} new{0}, {0} template)", EntityName));
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
            CollectionNavigations.Each(p => AddChildCollection(p));
        }

        private void AddChild(NavigationProperty property)
        {
            if (property.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                return;

            if (Settings.IsIgnoreRootChildren && Pass.Roots.Contains(property.Name))
                return;

            InnerContainer.Append(AddReturn(Tab(3), string.Format("if(template.{0} != null)", Pass.Code.Escape(property))));
            InnerContainer.Append(AddReturn(Tab(4), string.Format("new{0}.{1} = {1}.Create(template.{1});", EntityName, Pass.Code.Escape(property))));
            InnerContainer.Append(AddReturn());
        }

        private void AddChildCollection(NavigationProperty property)
        {
            if (Settings.IsIgnoreRootChildren && Pass.Roots.Contains(property.ToEndMember.Name))
                return;

            InnerContainer.Append(AddReturn(Tab(3), "template.", Pass.Code.Escape(property), ".Each(c =>"));
            InnerContainer.Append(AddReturn(Tab(3), "{"));
            InnerContainer.Append(AddReturn(Tab(4), "var child = ", Pass.Code.Escape(property.ToEndMember), ".Create(c);"));
            InnerContainer.Append(AddReturn(Tab(4), "new", EntityName, ".", Pass.Code.Escape(property), ".Add(child);"));
            InnerContainer.Append(AddReturn(Tab(3), "});"));
            InnerContainer.Append(AddReturn());
        }
        #endregion
    }
}
