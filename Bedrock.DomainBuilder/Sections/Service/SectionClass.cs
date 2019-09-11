using System;

using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Sections.Service
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
            var isInheritence = Settings.ServiceType != eServiceType.None;
            var inheritenceFormat = isInheritence ? " : {0}, I{1}Service" : string.Empty;

            Add(false, Tab(), string.Format("public partial class {0}Service", EntityName), string.Format(inheritenceFormat, Settings.ServiceType, EntityName));
            Add(false, NewLine(), Tab(), "{");

            return base.Build();
        }
        #endregion
    }
}
