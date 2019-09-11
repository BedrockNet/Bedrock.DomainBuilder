using System;
using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Sections.Contract
{
    public class SectionUsingStatements<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionUsingStatements(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
			Add("using System;");
			Add("using System.Collections.Generic;");
            Add("using System.ComponentModel.DataAnnotations;", NewLine());
            Add(string.Format("using {0};", Settings.NamespaceEntity));

            if (Settings.ContractType != eContractType.None)
                Add(string.Format("using {0}.Model;", Settings.NamespaceShared));

            return base.Build();
        }
        #endregion
    }
}