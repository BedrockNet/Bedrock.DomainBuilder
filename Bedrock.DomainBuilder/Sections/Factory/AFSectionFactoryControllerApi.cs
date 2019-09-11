using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.ControllerApi;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryControllerApi
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionControllerApi)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionControllerApi.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionControllerApi.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionControllerApi.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionControllerApi.Constructors:
                    {
                        returnValue = new SectionConstructors<T>(pass);
                        break;
                    }
                case eBuildSectionControllerApi.Properties:
                    {
                        returnValue = new SectionProperties<T>(pass);
                        break;
                    }
                case eBuildSectionControllerApi.ApiMethods:
                    {
                        returnValue = new SectionApiMethods<T>(pass);
                        break;
                    }
                case eBuildSectionControllerApi.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionControllerApi.NamespaceClose:
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
