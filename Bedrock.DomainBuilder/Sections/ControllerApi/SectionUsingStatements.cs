using System;

namespace Bedrock.DomainBuilder.Sections.ControllerApi
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
            Add(string.Format("using {0};", Settings.NamespaceAppService), NewLine());
            Add(string.Format("using {0}.Session.Interface;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Web.Api.Controller;", Settings.NamespaceShared), NewLine());
            Add("using Microsoft.AspNetCore.Mvc;");
        }

        protected override void BuildFramework()
        {   
            Add("using System.Threading.Tasks;");
            Add("using System.Web.Http;");
            Add("using System.Web.Http.Description;", NewLine());

            Add(string.Format("using {0};", Settings.NamespaceAppService));
            Add(string.Format("using {0};", Settings.NamespaceContract), NewLine());
            Add(string.Format("using {0}.Session.Interface;", Settings.NamespaceShared), NewLine());
            Add(string.Format("using {0}.Web.Api.Controller;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Web.Api.Filter;", Settings.NamespaceShared));
        }
        #endregion
    }
}

