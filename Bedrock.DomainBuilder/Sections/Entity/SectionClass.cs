using System;
using System.Linq;

using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionClass<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionClass(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            var isInheritence = Settings.EntityType != eEntityType.None;
            var inheritenceFormatEnumeration = isInheritence ? !Settings.IsIncludePrimaryKey ? " : BedrockIdEntity<{0}, {1}>, IBedrockEntity, IBedrockIdEntity<{1}>" : " : BedrockEntity<{0}>, IBedrockEntity" : string.Empty;
            var inheritenceFormat = (isInheritence && !IsEnumeration) ? !Settings.IsIncludePrimaryKey ? " : {3}<{0}, {1}>, I{4}" : " : {3}<{0}>, I{4}" : inheritenceFormatEnumeration;

            Add(false, Tab(), string.Format("public partial class {0}", EntityName), string.Format(inheritenceFormat, EntityName, GetKeyType(), Settings.Domain, Settings.EntityType, Settings.EntityType.ToString().Replace("Id", "")));

            if (isInheritence && Settings.IsValidatable && !IsEnumeration)
                Container.Append(string.Concat(", ", "IValidatableObject"));

            Add(false, NewLine(), Tab(), "{");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private string GetKeyType()
        {
            var keyType = Settings.PrimaryKeyType.ToString();

            if (Pass.EntityType.KeyMembers.Count() == 1)
            {
                var property = Pass.EntityType.KeyMembers.Single();
                keyType = Pass.Code.Escape(property.TypeUsage);
            }

            return keyType;
        }
        #endregion
    }
}
