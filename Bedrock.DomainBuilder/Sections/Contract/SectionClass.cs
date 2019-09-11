using System;
using System.Linq;

using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Sections.Contract
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
            var contractType = Settings.ContractType;

            if (IsEnumeration)
                contractType = eContractType.BedrockIdModel;

            var isInheritence = (contractType != eContractType.None);// && !IsEnumeration;
            var inheritenceFormat = isInheritence ? !Settings.IsIncludePrimaryKey ? " : {0}<{1}, {2}>" : " : {0}<{1}" : string.Empty;
            var inheritenceFormatMapper = isInheritence ? !Settings.IsIncludePrimaryKey ? " : {0}<{1}, {2}, {3}>" : " : {0}<{1}, {2}>" : string.Empty;
            var format = contractType != eContractType.ModelBase ? inheritenceFormatMapper : inheritenceFormat;
            var contractName = string.Concat(EntityName, Settings.ContractName);

            Add(false, Tab(), string.Format("public partial class {0}", contractName), string.Format(format, contractType, EntityName, contractName, GetPrimaryKeyType(Pass.EntityType)));
            Add(false, NewLine(), Tab(), "{");

            return base.Build();
        }
        #endregion
    }
}
