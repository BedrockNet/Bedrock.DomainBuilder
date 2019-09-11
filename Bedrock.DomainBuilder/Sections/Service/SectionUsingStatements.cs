using System;

namespace Bedrock.DomainBuilder.Sections.Service
{
    public class SectionUsingStatements<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionUsingStatements(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add("using System;");
            Add("using System.Collections.Generic;");
            Add("using System.Linq;");
            Add("using System.Threading.Tasks;", NewLine());

            Add($"using {Settings.NamespaceEntity};");
        }

        protected override void BuildFramework()
        {
            Add("using System;");
            Add("using System.Collections.Generic;");
            Add("using System.Linq;");
            Add("using System.Threading.Tasks;", NewLine());

            Add($"using {Settings.NamespaceEntity};", NewLine());

            Add($"using {Settings.NamespaceShared}.Data.Repository.Extension;");
            Add($"using {Settings.NamespaceShared}.Extension;");
            Add($"using {Settings.NamespaceShared}.Mapper.Extension;", NewLine());

            Add($"using {Settings.NamespaceShared}.Validation.Implementation;");
            Add($"using {Settings.NamespaceShared}.Validation.Implementation.Configuration;");
            Add($"using {Settings.NamespaceShared}.Validation.Interface;", NewLine());

            Add($"using {Settings.NamespaceShared}.Service.Interface;");
        }
        #endregion
    }
}