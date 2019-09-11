using System;

namespace Bedrock.DomainBuilder.Sections.ControllerMvc
{
    public class SectionClass<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionClass(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(false, Tab(), string.Format("public class {0}Controller : {1}", EntityName, Pass.Settings.ControllerMvcType));
            Add(false, NewLine(), Tab(), "{");

            return base.Build();
        }
        #endregion
    }
}
