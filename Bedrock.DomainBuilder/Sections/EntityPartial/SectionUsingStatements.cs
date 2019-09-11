using System;

namespace Bedrock.DomainBuilder.Sections.EntityPartial
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
            Add("using System.ComponentModel.DataAnnotations;");
        }

        protected override void BuildFramework()
        {
            Add("using System.ComponentModel.DataAnnotations;");
        }
        #endregion
    }
}