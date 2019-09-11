using System;

namespace Bedrock.DomainBuilder.Sections.ControllerMvc
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
            Add("using System.Threading.Tasks;", NewLine());
            Add(string.Format("using {0}.Interface.Infrastructure.Session;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Web.Controller;", Settings.NamespaceShared), NewLine());
            Add(string.Format("using {0};", Settings.NamespaceAppService));
            Add(string.Format("using {0};", Settings.NamespaceContract), NewLine());
            Add("using Microsoft.AspNetCore.Mvc;");
        }

        protected override void BuildFramework()
        {
            Add("using System;");
            Add("using System.Collections;");
            Add("using System.Collections.Generic;");
            Add("using System.Linq;");
            Add("using System.Threading.Tasks;");
            Add("using System.Web;");
            Add("using System.Web.Mvc;", NewLine());
            Add(string.Format("using {0}.Interface.Infrastructure;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Web.Controller.Mvc;", Settings.NamespaceShared));
        }
        #endregion
    }
}

