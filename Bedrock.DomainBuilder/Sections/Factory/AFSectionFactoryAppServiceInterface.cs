using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.AppServiceInterface;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryAppServiceInterface
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionAppServiceInterface)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionAppServiceInterface.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionAppServiceInterface.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionAppServiceInterface.Interface:
                    {
                        returnValue = new SectionInterface<T>(pass);
                        break;
                    }
                case eBuildSectionAppServiceInterface.Properties:
                    {
                        returnValue = new SectionProperties<T>(pass);
                        break;
                    }
                case eBuildSectionAppServiceInterface.Methods:
                    {
                        returnValue = new SectionMethods<T>(pass);
                        break;
                    }
                case eBuildSectionAppServiceInterface.InterfaceClose:
                    {
                        returnValue = new SectionInterfaceClose<T>(pass);
                        break;
                    }
                case eBuildSectionAppServiceInterface.NamespaceClose:
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
