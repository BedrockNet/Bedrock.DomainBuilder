using System;

namespace Bedrock.DomainBuilder.Sections.Entity
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
            Add("using System.ComponentModel.DataAnnotations;", NewLine());

            Add(string.Format("using {0}.Entity.Implementation;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Entity.Interface;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Utility;", Settings.NamespaceShared));
        }

        protected override void BuildFramework()
        {
            Add("using System;");
            Add("using System.Collections.Generic;");
            Add(Settings.IsIncludeSpatialType, "using System.ComponentModel.DataAnnotations;");

            if (Settings.IsIncludeSpatialType)
                Add(false, "using System.Data.Entity.Spatial;");

            Add(NewLine());
            Add(string.Format("using {0}.Entity.Implementation;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Entity.Interface;", Settings.NamespaceShared), NewLine());
			Add("using SharedUtility = Bedrock.Shared.Utility;");

            if (Settings.IsTracked)
                Add(string.Format("using {0}.Tracking;", Settings.NamespaceShared));
        }
        #endregion
    }
}