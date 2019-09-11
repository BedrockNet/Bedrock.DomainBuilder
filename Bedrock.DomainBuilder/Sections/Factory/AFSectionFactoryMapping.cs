using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.Mapping;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryMapping
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionMapping)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionMapping.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionMapping.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionMapping.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionMapping.Fields:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionMapping.Constructors:
                    {
                        returnValue = new SectionConstructors<T>(pass);
                        break;
                    }
                case eBuildSectionMapping.Properties:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionMapping.PublicMethods:
                    {
                        returnValue = new SectionPublicMethods<T>(pass);
                        break;
                    }
                case eBuildSectionMapping.PrivateMethods:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionMapping.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionMapping.NamespaceClose:
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
