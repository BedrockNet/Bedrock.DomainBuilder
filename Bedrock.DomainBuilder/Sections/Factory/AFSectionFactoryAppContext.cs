using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.AppContext;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryAppContext
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionAppContext)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionAppContext.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.Interface:
                    {
                        returnValue = new SectionInterface<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.Constructors:
                    {
                        returnValue = new SectionConstructors<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.Properties:
                    {
                        returnValue = new SectionProperties<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.InterfaceProperties:
                    {
                        returnValue = new SectionInterfaceProperties<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.PublicMethods:
                    {
                        returnValue = new SectionPublicMethods<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.PrivateMethods:
                    {
                        returnValue = new SectionPrivateMethods<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionAppContext.NamespaceClose:
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
