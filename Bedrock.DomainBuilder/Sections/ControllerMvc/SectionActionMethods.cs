using System;

namespace Bedrock.DomainBuilder.Sections.ControllerMvc
{
    public class SectionActionMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionActionMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), "#region Action Methods");
            BuildMethods();
            Add(false, Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildMethods() { }
        #endregion
    }
}
