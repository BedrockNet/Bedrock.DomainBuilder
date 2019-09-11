using System;

using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections.Entity;

namespace Bedrock.DomainBuilder.Sections.Factory
{
    public static class AFSectionFactoryEntity
    {
        #region Methods
        public static ISection CreateSection<T>(T buildSection, BuildPass<T> pass)
            where T : struct, IConvertible
        {
            ISection returnValue = null;
            var buildSectionValue = (eBuildSectionEntity)(object)buildSection;

            switch (buildSectionValue)
            {
                case eBuildSectionEntity.UsingStatements:
                    {
                        returnValue = new SectionUsingStatements<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.Namespace:
                    {
                        returnValue = new SectionNamespace<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.Attributes:
                    {
                        returnValue = new SectionAttributes<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.Class:
                    {
                        returnValue = new SectionClass<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.Fields:
                    {
                        returnValue = new SectionFields<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.Constructors:
                    {
                        returnValue = new SectionConstructors<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.Properties:
                    {
                        returnValue = new SectionProperties<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.IValidatable:
                    {
                        returnValue = new SectionIValidatable<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.PublicMethods:
                    {
                        returnValue = new SectionPublicMethods<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.PrivateMethods:
                    {
                        returnValue = new SectionPrivateMethods<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.ClassClose:
                    {
                        returnValue = new SectionClassClose<T>(pass);
                        break;
                    }
                case eBuildSectionEntity.NamespaceClose:
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
