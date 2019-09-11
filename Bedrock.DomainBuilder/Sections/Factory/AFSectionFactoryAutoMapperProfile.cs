using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.AutoMapperProfile;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryAutoMapperProfile
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionAutoMapperProfile)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionAutoMapperProfile.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionAutoMapperProfile.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionAutoMapperProfile.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionAutoMapperProfile.Constructors:
                    {
                        returnValue = new SectionConstructors<T>(pass);
                        break;
                    }
                case eBuildSectionAutoMapperProfile.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionAutoMapperProfile.NamespaceClose:
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
