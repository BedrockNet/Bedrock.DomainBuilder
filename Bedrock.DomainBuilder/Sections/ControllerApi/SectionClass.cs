using System;

namespace Bedrock.DomainBuilder.Sections.ControllerApi
{
    public class SectionClass<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionClass(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(Tab(), string.Format("[Route(\"api/{0}\")]", EntityName));
            Add(false, Tab(), string.Format("public class {0}Controller : {1}", EntityName, Pass.Settings.ControllerApiType));
            Add(false, NewLine(), Tab(), "{");
        }

        protected override void BuildFramework()
        {
            Add(Tab(), string.Format("[RoutePrefix(\"api/{0}\")]", EntityName));
            Add(false, Tab(), string.Format("public class {0}Controller : {1}", EntityName, Pass.Settings.ControllerApiType));
            Add(false, NewLine(), Tab(), "{");
        }
        #endregion
    }
}
