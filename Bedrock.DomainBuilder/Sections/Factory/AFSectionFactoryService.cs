using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.Service;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryService
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionService)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionService.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionService.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionService.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionService.Fields:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionService.Constructors:
                    {
                        returnValue = new SectionConstructors<T>(pass);
                        break;
                    }
                case eBuildSectionService.Properties:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionService.InterfaceMethods:
                    {
                        returnValue = new SectionInterfaceMethods<T>(pass);
                        break;
                    }
                case eBuildSectionService.PublicMethods:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionService.PrivateMethods:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionService.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionService.NamespaceClose:
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
