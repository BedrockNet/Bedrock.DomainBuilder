using System;
using System.CodeDom;
using System.Data.Metadata.Edm;
using System.Globalization;

using Microsoft.CSharp;

namespace Bedrock.DomainBuilder.EntityFramework
{
    public class CodeGenerationTools
    {
        #region Constructors
        public CodeGenerationTools()
        {
            Code = new CSharpCodeProvider();
            Tools = new MetadataTools();

            FullyQualifySystemTypes = false;
            CamelCaseFields = true;
        }
        #endregion

        #region Properties
        protected CSharpCodeProvider Code { get; set; }
        public MetadataTools Tools { get; set; }
        public bool FullyQualifySystemTypes { get; set; }
        public bool CamelCaseFields { get; set; }
        #endregion

        #region Public Methods
        public string Escape(string name)
        {
            if (name == null)
                return null;

            return Code.CreateEscapedIdentifier(name);
        }

        public string Escape(TypeUsage typeUsage)
        {
            if (typeUsage == null)
                return null;

            if (typeUsage.EdmType is ComplexType || typeUsage.EdmType is EntityType)
            {
                return Escape(typeUsage.EdmType.Name);
            }
            else if (typeUsage.EdmType is SimpleType)
            {
                var clrType = Tools.UnderlyingClrType(typeUsage.EdmType);
                var typeName = typeUsage.EdmType is EnumType ? Escape(typeUsage.EdmType.Name) : Escape(clrType);

                if (clrType.IsValueType && Tools.IsNullable(typeUsage))
                    return String.Format(CultureInfo.InvariantCulture, "Nullable<{0}>", typeName);

                return typeName;
            }
            else if (typeUsage.EdmType is CollectionType)
                return String.Format(CultureInfo.InvariantCulture, "ICollection<{0}>", Escape(((CollectionType)typeUsage.EdmType).TypeUsage));

            throw new ArgumentException("typeUsage");
        }

        public string Escape(EdmMember member)
        {
            if (member == null)
                return null;

            return Escape(member.Name);
        }

        public string Escape(EdmType type)
        {
            if (type == null)
                return null;

            return Escape(type.Name);
        }

        public string Escape(EdmFunction function)
        {
            if (function == null)
                return null;

            return Escape(function.Name);
        }

        public string Escape(EnumMember member)
        {
            if (member == null)
                return null;

            return Escape(member.Name);
        }

        public string Escape(EntityContainer container)
        {
            if (container == null)
                return null;

            return Escape(container.Name);
        }

        public string Escape(EntitySet set)
        {
            if (set == null)
                return null;

            return Escape(set.Name);
        }

        public string Escape(StructuralType type)
        {
            if (type == null)
                return null;

            return Escape(type.Name);
        }

        public string EscapeNamespace(string namespaceName)
        {
            if (string.IsNullOrEmpty(namespaceName))
                return namespaceName;

            var parts = namespaceName.Split('.');
            namespaceName = String.Empty;

            foreach (string part in parts)
            {
                if (namespaceName != String.Empty)
                    namespaceName += ".";

                namespaceName += Escape(part);
            }

            return namespaceName;
        }

        public string Escape(Type clrType)
        {
            return Escape(clrType, FullyQualifySystemTypes);
        }

        public string Escape(Type clrType, bool fullyQualifySystemTypes)
        {
            if (clrType == null)
                return null;

            string typeName;

            if (fullyQualifySystemTypes)
                typeName = "global::" + clrType.FullName;
            else
                typeName = Code.GetTypeOutput(new CodeTypeReference(clrType));

            return typeName;
        }

        public string AbstractOption(EntityType entity)
        {
            if (entity.Abstract)
                return "abstract";

            return string.Empty;
        }

        public string FieldName(EdmMember member)
        {
            if (member == null)
                return null;

            return FieldName(member.Name);
        }

        public string FieldName(EntitySet set)
        {
            if (set == null)
                return null;

            return FieldName(set.Name);
        }

        public string FieldName(string name)
        {
            if (CamelCaseFields)
                return "_" + CamelCase(name);
            else
                return "_" + name;
        }

        public string CamelCase(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                return identifier;

            if (identifier.Length == 1)
                return identifier[0].ToString(CultureInfo.InvariantCulture).ToLowerInvariant();

            return identifier[0].ToString(CultureInfo.InvariantCulture).ToLowerInvariant() + identifier.Substring(1);
        }
        #endregion
    }
}
