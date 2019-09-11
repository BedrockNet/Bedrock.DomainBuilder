using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.AppService;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryAppService
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionAppService)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionAppService.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionAppService.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionAppService.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionAppService.Constructors:
                    {
                        returnValue = new SectionConstructors<T>(pass);
                        break;
                    }
                case eBuildSectionAppService.Properties:
                    {
                        returnValue = new SectionProperties<T>(pass);
                        break;
                    }
                case eBuildSectionAppService.InterfaceMethods:
                    {
                        returnValue = new SectionInterfaceMethods<T>(pass);
                        break;
                    }
                case eBuildSectionAppService.PrivateMethods:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionAppService.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionAppService.NamespaceClose:
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
