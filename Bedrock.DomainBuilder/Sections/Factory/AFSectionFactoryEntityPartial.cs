using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.EntityPartial;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryEntityPartial
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionEntityPartial)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionEntityPartial.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionEntityPartial.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionEntityPartial.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionEntityPartial.Properties:
                    {
                        returnValue = new SectionProperties<T>(pass);
                        break;
                    }
                case eBuildSectionEntityPartial.PrivateMethods:
                    {
                        returnValue = new SectionPrivateMethods<T>(pass);
                        break;
                    }
                case eBuildSectionEntityPartial.InternalClasses:
                    {
                        returnValue = new SectionInternalClasses<T>(pass);
                        break;
                    }
                case eBuildSectionEntityPartial.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionEntityPartial.NamespaceClose:
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
