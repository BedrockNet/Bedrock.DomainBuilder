using System;
using System.Data.Metadata.Edm;
using System.Linq;

using Bedrock.DomainBuilder.EntityFramework;

namespace Bedrock.DomainBuilder.Sections.Entity
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
            if (!Settings.IsIncludeProperties)
                return;

            BuildSimpleProperties();
            BuildNavigationProperties();
        }

        private void BuildSimpleProperties()
        {
            var simpleProperties = GetPropertiesConfigIdAudit();
            var keyName = GetCommonKeyPropertyName();

            simpleProperties.Each(p => BuildSimpleProperty(p, keyName));

            if (simpleProperties.Any())
                Container.Remove(Container.Length - 2, 2);
        }

        private void BuildSimpleProperty(EdmProperty property, string keyName)
        {
            var typeUsage = Pass.Code.Escape(property.TypeUsage);

            if (typeUsage.StartsWith("System.Data.Spatial."))
                typeUsage = typeUsage.Replace("System.Data.Spatial.", "System.Data.Entity.Spatial.");

            var propertyName = Pass.Code.Escape(property);

            if (propertyName == keyName)
                propertyName = Settings.CommonKey;

            if (Settings.IsTracked)
                Add(Tab(2), string.Format("[Tracked(\"{0}\")]", propertyName.ToSeparatedWords()));

            Add(Tab(2), string.Format("{0} {1} {2} {{ get; set; }}", Accessibility.ForProperty(property), typeUsage, propertyName), NewLine());
        }

        private void BuildNavigationProperties()
        {
            var navigationProperties = Navigations;
            var collectionNavigationProperties = CollectionNavigations;

            if (navigationProperties.Any())
                Add();

            navigationProperties.Each(p =>
            {
                Add(Tab(2), "public virtual ", Pass.Code.Escape(p.ToEndMember.GetEntityType()), " ", Pass.Code.Escape(p), " { get; set; }");
                Add();
            });

            if (navigationProperties.Any())
                Container.Remove(Container.Length - 2, 2);

            if (collectionNavigationProperties.Any())
                Add();


            collectionNavigationProperties.Each(p =>
            {
                Add(Tab(2), "public virtual ICollection<", Pass.Code.Escape(p.ToEndMember.GetEntityType()), "> ", Pass.Code.Escape(p), " { get; set; }");
                Add();
            });

            if (collectionNavigationProperties.Any())
                Container.Remove(Container.Length - 2, 2);
        }
        #endregion
    }
}
