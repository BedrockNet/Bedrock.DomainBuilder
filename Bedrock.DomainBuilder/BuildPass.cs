using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Metadata.Edm;

using Bedrock.DomainBuilder.Builder;
using Bedrock.DomainBuilder.EntityFramework;
using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections;

namespace Bedrock.DomainBuilder
{
    public class BuildPass<T>
        where T : struct, IConvertible
    {
        #region Constructors
        private BuildPass() { }
        #endregion

        #region Properties
        public T BuildSection { get; set; }

        public ISection Section { get; set; }

        public EntityType EntityType { get; set; }

        public EntityHelper EntityHelper { get; set; }

        public BuildSettings Settings { get; set; }

        public List<string> Roots { get; set; }

        public List<string> Enumerations { get; set; }

        public IBuilder Builder { get; set; }

        public CodeGenerationTools Code { get; set; }

        public IPluralizationService PluralizationService { get; set; }

        public EntitySet TableSet
        {
            get { return EntityHelper.Mappings.EntityMappings[EntityType].Item1; }
        }

        public Dictionary<EdmProperty, EdmProperty> PropertyToColumnMappings
        {
            get { return EntityHelper.Mappings.EntityMappings[EntityType].Item2; }
        }
        #endregion

        #region Methods
        public static BuildPass<T> Create(T buildSection, EntityType entityType, EntityHelper entityHelper, BuildSettings settings, IBuilder builder,
                                            List<string> roots, List<string> enumerations, CodeGenerationTools code, IPluralizationService pluralizationService)
        {
            return new BuildPass<T>
            {
                BuildSection = buildSection,
                EntityType = entityType,
                EntityHelper = entityHelper,
                Settings = settings,
                Builder = builder,
                Roots = roots,
                Enumerations = enumerations,
                Code = code,
                PluralizationService = pluralizationService
            };
        }

        public void Validate()
        {
            var buildSectionType = typeof(T);

            if (Section == null)
                throw new Exception("BuildPass does not have Section set; cannot build");

            if ((buildSectionType == typeof(eBuildSectionEntity) ||
                    buildSectionType == typeof(eBuildSectionMapping) ||
                    buildSectionType == typeof(eBuildSectionService)) && EntityType == null)
                throw new Exception("BuildPass does not have EntityType set; cannot build");

            if (EntityHelper == null)
                throw new Exception("BuildPass does not have EntityHelper set; cannot build");

            if (Settings == null)
                throw new Exception("BuildPass does not have Settings set; cannot build");

            if (Builder == null)
                throw new Exception("BuildPass does not have Builder set; cannot build");

            if (Roots == null)
                throw new Exception("BuildPass does not have Roots set; cannot build");

            if (Enumerations == null)
                throw new Exception("BuildPass does not have Enumerations set; cannot build");

            if (Code == null)
                throw new Exception("BuildPass does not have Code set; cannot build");

            if (PluralizationService == null)
                throw new Exception("BuildPass does not have PluralizationService set; cannot build");
        }
        #endregion
    }
}
