using System;
using System.Data.Metadata.Edm;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.EntityPartial
{
    public class SectionInternalClasses<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionInternalClasses(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            if (Settings.IsValidatable && !IsEnumeration)
            {
                BuildInternalClassesRegionStart();
                BuildInternalClassesMetaDataClassDeclaration();
                BuildInternalClassesMetaDataClassConstructor();
                BuildInternalClassesMetaDataClassProperties();
                BuildInternalClassesMetaDataClassClose();
                BuildInternalClassesRegionEnd();

                return base.Build();
            }

            return string.Empty;
        }
        #endregion

        #region Private Methods
        private void BuildInternalClassesRegionStart()
        {
            Add(NewLine(), Tab(2), "#region Internal Classes");
        }

        private void BuildInternalClassesMetaDataClassDeclaration()
        {
            Add(Tab(2), "internal sealed class Metadata");
            Add(Tab(2), "{");
        }

        private void BuildInternalClassesMetaDataClassConstructor()
        {
            Add(Tab(3), "private Metadata() { }", NewLine());
        }

        private void BuildInternalClassesMetaDataClassProperties()
        {
            var keyName = GetCommonKeyPropertyName();

            foreach (var property in Pass.EntityType.Properties)
                BuildInternalClassesMetaDataTableColumn(property, keyName);

            if (Pass.EntityType.Properties.Count() > 0)
                Container.Remove(Container.Length - 2, 2);
        }

        private void BuildInternalClassesMetaDataTableColumn(EdmProperty property, string keyName)
        {
            var maxLength = Pass.Code.Tools.GetPropertyMaxLength(property);
            var requiresRequiredAttribute = !Pass.Code.Tools.IsNullable(property) && !Pass.Code.Tools.IsValueType(property.TypeUsage.EdmType);
            var requiresMaxLengthAttribute = maxLength > 0;

            if (requiresRequiredAttribute)
                Add(Tab(3), "[Required]");

            if (requiresMaxLengthAttribute)
                Add(Tab(3), string.Format("[MaxLength({0})]", maxLength));

            if (requiresRequiredAttribute || requiresMaxLengthAttribute)
            {
                var propertyName = Pass.Code.Escape(property);

                if (propertyName == keyName)
                    propertyName = Settings.CommonKey;

                Add(Tab(3), string.Format("public {0} {1} {{ get; set; }}", Pass.Code.Escape(property.TypeUsage), propertyName), NewLine());
            }
        }

        private void BuildInternalClassesMetaDataClassClose()
        {
            Add(Tab(2), "}");
        }

        private void BuildInternalClassesRegionEnd()
        {
            Add(false, Tab(2), "#endregion");
        }
        #endregion
    }
}
