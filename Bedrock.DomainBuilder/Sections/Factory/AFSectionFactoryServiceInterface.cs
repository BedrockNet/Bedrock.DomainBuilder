using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.ServiceInterface;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryServiceInterface
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionServiceInterface)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionServiceInterface.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionServiceInterface.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionServiceInterface.Interface:
                    {
                        returnValue = new SectionInterface<T>(pass);
                        break;
                    }
                case eBuildSectionServiceInterface.Properties:
                    {
                        returnValue = new SectionProperties<T>(pass);
                        break;
                    }
                case eBuildSectionServiceInterface.Methods:
                    {
                        returnValue = new SectionMethods<T>(pass);
                        break;
                    }
                case eBuildSectionServiceInterface.InterfaceClose:
                    {
                        returnValue = new SectionInterfaceClose<T>(pass);
                        break;
                    }
                case eBuildSectionServiceInterface.NamespaceClose:
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
