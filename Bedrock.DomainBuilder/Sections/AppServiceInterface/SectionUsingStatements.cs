using System;

namespace Bedrock.DomainBuilder.Sections.AppServiceInterface
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
            Add(string.Format("using {0}.Session.Interface;", Settings.NamespaceShared));
        }

        protected override void BuildFramework()
        {
            Add("using System.Collections.Generic;");
            Add("using System.Threading.Tasks;", NewLine());
            Add(string.Format("using {0};", Settings.NamespaceEntity));
            Add(string.Format("using {0};", Settings.NamespaceContract), NewLine());
            Add(string.Format("using {0}.Service.Interface;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Session.Interface;", Settings.NamespaceShared));
        }
        #endregion
    }
}
