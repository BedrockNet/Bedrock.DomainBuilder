using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.Enumeration;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryEnumeration
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionEnumeration)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionEnumeration.UsingStatements:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionEnumeration.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionEnumeration.Enum:
                    {
                        returnValue = new SectionEnum<T>(pass);
                        break;
                    }
                case eBuildSectionEnumeration.Members:
                    {
                        returnValue = new SectionMembers<T>(pass);
                        break;
                    }
                case eBuildSectionEnumeration.EnumClose:
                    {
                        returnValue = new SectionEnumClose<T>(pass);
                        break;
                    }
                case eBuildSectionEnumeration.NamespaceClose:
                    {
                        returnValue = new SectionNamespaceClose<T>(pass);
                        break;
                    }
            }

            return returnValue;
        }
        #endregion
    }
}
