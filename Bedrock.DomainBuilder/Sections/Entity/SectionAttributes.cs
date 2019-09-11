using System;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionAttributes<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionAttributes(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(Settings.IsValidatable && !IsEnumeration, Tab(), "[Serializable]");

            if (Settings.IsValidatable && !IsEnumeration)
                Add(false, Tab(), "[MetadataType(typeof(Metadata))]");
        }

        protected override void BuildFramework()
        {
            Add(Settings.IsValidatable && !IsEnumeration, Tab(), "[Serializable]");

            if (Settings.IsValidatable && !IsEnumeration)
                Add(false, Tab(), "[SharedUtility.MetadataType(typeof(Metadata))]");
        }
        #endregion
    }
}
