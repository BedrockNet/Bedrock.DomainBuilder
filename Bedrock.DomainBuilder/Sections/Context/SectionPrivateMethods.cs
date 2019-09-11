using System;

namespace Bedrock.DomainBuilder.Sections.Context
{
    public class SectionPrivateMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPrivateMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(Tab(2), "#region Private Methods");
            AddIgnorePropertiesCore();
            Add(false, Tab(2), "#endregion");
        }

        protected override void BuildFramework()
        {
            Add(Tab(2), "#region Private Methods");
            AddIgnorePropertiesFramework();
            Add(false, Tab(2), "#endregion");
        }
        #endregion

        #region ISection Methods

        #endregion

        #region Private Methods
        private void AddIgnorePropertiesCore()
        {
            Add(Tab(2), "private void IgnoreProperties(ModelBuilder modelBuilder){ }");
        }

        private void AddIgnorePropertiesFramework()
        {
            Add(Tab(2), "private void IgnoreProperties(DbModelBuilder modelBuilder){ }");
        }
        #endregion
    }
}
