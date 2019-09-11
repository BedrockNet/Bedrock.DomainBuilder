using System;

namespace Bedrock.DomainBuilder.Sections.Mapping
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
            Add(string.Format("using {0};", Settings.NamespaceEntity), NewLine());

            Add("using Microsoft.EntityFrameworkCore;");
            Add("using Microsoft.EntityFrameworkCore.Metadata.Builders;");
        }

        protected override void BuildFramework()
        {
            Add("using System.ComponentModel.DataAnnotations.Schema;");
            Add("using System.Data.Entity.ModelConfiguration;", NewLine());
            Add(string.Format("using {0};", Settings.NamespaceEntity));
        }
        #endregion
    }
}
