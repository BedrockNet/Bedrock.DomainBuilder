using System;

namespace Bedrock.DomainBuilder.Sections.ServiceInterface
{
    public class SectionInterface<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionInterface(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(false, Tab(), string.Format("public partial interface I{0}Service : {1}", EntityName, "ISessionAware"));
            Add(false, NewLine(), Tab(), "{");

            return base.Build();
        }
        #endregion
    }
}
