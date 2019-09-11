using System;

namespace Bedrock.DomainBuilder.Sections.ServiceInterface
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
            Add("using System.Threading.Tasks;", NewLine());

            Add($"using {Settings.NamespaceEntity};", NewLine());

            Add($"using {Settings.NamespaceShared}.Service.Interface;");
            Add($"using {Settings.NamespaceShared}.Session.Interface;");
        }

        protected override void BuildFramework()
        {
            Add("using System.Collections.Generic;");
            Add("using System.Threading.Tasks;", NewLine());
            Add($"using {Settings.NamespaceEntity};", NewLine());
            Add($"using {Settings.NamespaceShared}.Service.Interface;");
            Add($"using {Settings.NamespaceShared}.Session.Interface;");
        }
        #endregion
    }
}
