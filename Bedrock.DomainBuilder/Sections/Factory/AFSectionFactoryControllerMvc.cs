using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.ControllerMvc;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryControllerMvc
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionControllerMvc)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionControllerMvc.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionControllerMvc.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionControllerMvc.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionControllerMvc.Constructors:
                    {
                        returnValue = new SectionConstructors<T>(pass);
                        break;
                    }
                case eBuildSectionControllerMvc.Properties:
                    {
                        returnValue = new SectionProperties<T>(pass);
                        break;
                    }
                case eBuildSectionControllerMvc.ActionMethods:
                    {
                        returnValue = new SectionActionMethods<T>(pass);
                        break;
                    }
                case eBuildSectionControllerMvc.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionControllerMvc.NamespaceClose:
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
