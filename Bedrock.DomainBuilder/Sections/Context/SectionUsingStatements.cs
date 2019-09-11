using System;

namespace Bedrock.DomainBuilder.Sections.Context
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
            Add(string.Format("using {0};", Settings.NamespaceEntity));
            Add(string.Format("using {0}.{1};", Settings.NamespaceMapping, Settings.Domain), NewLine());

            Add(string.Format("using {0}.Configuration;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Data.Repository.EntityFramework;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Data.Repository.EntityFramework.Helper;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Domain.Interface;", Settings.NamespaceShared), NewLine());
            Add("using Microsoft.EntityFrameworkCore;");
        }

        protected override void BuildFramework()
        {
            Add("using System.Data.Entity;", NewLine());
            Add(string.Format("using {0};", Settings.NamespaceEntity));
            Add(string.Format("using {0};", Settings.NamespaceMapping), NewLine());
            Add(string.Format("using {0}.Data.Repository.EntityFramework;", Settings.NamespaceShared));
        }
        #endregion
    }
}
