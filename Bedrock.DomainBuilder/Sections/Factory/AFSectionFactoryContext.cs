using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.Context;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryContext
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionContext)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionContext.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionContext.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionContext.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionContext.Fields:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionContext.Constructors:
                    {
                        returnValue = new SectionConstructors<T>(pass);
                        break;
                    }
                case eBuildSectionContext.Properties:
                    {
                        returnValue = new SectionProperties<T>(pass);
                        break;
                    }
                case eBuildSectionContext.ProtectedMethods:
                    {
                        returnValue = new SectionProtectedMethods<T>(pass);
                        break;
                    }
                case eBuildSectionContext.PublicMethods:
                    {
                        returnValue = new SectionEmpty<T>(pass);
                        break;
                    }
                case eBuildSectionContext.PrivateMethods:
                    {
                        returnValue = new SectionPrivateMethods<T>(pass);
                        break;
                    }
                case eBuildSectionContext.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionContext.NamespaceClose:
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
