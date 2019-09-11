using System;
using System.Data.Metadata.Edm;
using System.Linq;

using Bedrock.DomainBuilder.EntityFramework;

namespace Bedrock.DomainBuilder.Sections.Contract
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
            Add(false, Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildProperties()
        {
            BuildSimpleProperties();
            BuildNavigationProperties();
        }

        private void BuildSimpleProperties()
        {
            var keyName = GetCommonKeyPropertyName();
            GetPropertiesConfigIdAudit().Each(p => BuildSimpleProperty(p, keyName));

            Container.Remove(Container.Length - 2, 2);
        }

        private void BuildSimpleProperty(EdmProperty property, string keyName)
        {
            var typeUsage = Pass.Code.Escape(property.TypeUsage);
            var maxLength = Pass.Code.Tools.GetPropertyMaxLength(property);
            var requiresRequiredAttribute = !Pass.Code.Tools.IsNullable(property) && !Pass.Code.Tools.IsValueType(property.TypeUsage.EdmType);
            var requiresMaxLengthAttribute = maxLength > 0;

            if (typeUsage.StartsWith("System.Data.Spatial."))
                typeUsage = typeUsage.Replace("System.Data.Spatial.", "System.Data.Entity.Spatial.");

            var propertyName = Pass.Code.Escape(property);

            if (propertyName == keyName)
                propertyName = Settings.CommonKey;

            if (requiresRequiredAttribute)
                Add(Tab(2), "[Required]");

            if (requiresMaxLengthAttribute)
                Add(Tab(2), string.Format("[MaxLength({0})]", maxLength));

            Add(Tab(2), string.Format("{0} {1} {2} {{ get; set; }}", Accessibility.ForProperty(property), typeUsage, propertyName), NewLine());
        }

        private void BuildNavigationProperties()
        {
            var navigationProperties = Navigations;
            var collectionNavigationProperties = CollectionNavigations;

            navigationProperties.Each(p =>
            {
                Add(Tab(2), "public ", Pass.Code.Escape(p.ToEndMember.GetEntityType()), Settings.ContractName, " ", Pass.Code.Escape(p), " { get; set; }");
                Add();
            });

            if (navigationProperties.Any())
            {
                Container.Remove(Container.Length - 2, 2);

                if (collectionNavigationProperties.Any())
                    Add();
            }

            collectionNavigationProperties.Each(p =>
            {
                var type = Singularize(Pass.Code.Escape(p.ToEndMember.GetEntityType()));
                Add(Tab(2), "public ICollection<", type, Settings.ContractName, "> ", Pass.Code.Escape(p), " { get; set; }");
                Add();
            });

            if (collectionNavigationProperties.Any())
                Container.Remove(Container.Length - 2, 2);
        }
        #endregion
    }
}
