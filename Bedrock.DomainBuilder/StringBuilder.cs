using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Bedrock.DomainBuilder.Builder;
using Bedrock.DomainBuilder.EntityFramework;
using Bedrock.DomainBuilder.Enumerations;
using Bedrock.DomainBuilder.Sections;
using Bedrock.DomainBuilder.Sections.Factory;

namespace Bedrock.DomainBuilder
{
    public class StringBuilder<T>
        where T : struct, IConvertible
    {
        #region Fields
        private event BuilderPassComplete _onBuilderPassComplete;
        #endregion

        #region Constructors
        public StringBuilder(EntityType entityType, EntityHelper entityHelper, BuildSettings settings, IBuilder builder,
                                List<string> roots, List<string> enumerations, PauseToken pause, CancellationToken cancellation)
        {
            EntityType = entityType;
            EntityHelper = entityHelper;
            Settings = settings;
            Builder = builder;
            Roots = roots;
            Enumerations = enumerations;
            Pause = pause;
            Cancellation = cancellation;
            Container = new StringBuilder();
        }

        static StringBuilder()
        {
            Code = new CodeGenerationTools();
            PluralizationService = new EnglishPluralizationService();
        }
        #endregion

        #region Events
        public event BuilderPassComplete OnBuilderPassComplete
        {
            add { _onBuilderPassComplete += value; }
            remove { _onBuilderPassComplete -= value; }
        }
        #endregion

        #region Properties
        protected EntityType EntityType { get; set; }
        protected EntityHelper EntityHelper { get; set; }
        protected BuildSettings Settings { get; set; }
        protected StringBuilder Container { get; set; }
        protected IBuilder Builder { get; set; }
        protected List<string> Roots { get; set; }
        protected List<string> Enumerations { get; set; }
        protected PauseToken Pause { get; set; }
        protected CancellationToken Cancellation { get; set; }
        public static CodeGenerationTools Code { get; set; }
        public static IPluralizationService PluralizationService { get; set; }
        #endregion

        #region Public Methods
        public async Task<string> Build()
        {
            foreach (var pass in GetBuildPasses())
            {
                pass.Validate();

                var result = pass.Section.Build();

                Container.Append(result);
                BuildPassComplete(pass.BuildSection);

                await Pause.WaitWhilePausedAsync();

                if (Cancellation.IsCancellationRequested)
                    break;
            }

            return await Task.FromResult(Container.ToString());
        }
        #endregion

        #region Private Methods
        private IEnumerable<BuildPass<T>> GetBuildPasses()
        {
            return Enum
                    .GetValues(typeof(T))
                    .Cast<T>()
                    .Select(s =>
                    {
                        var pass = BuildPass<T>.Create(s, EntityType, EntityHelper, Settings, Builder, Roots, Enumerations, Code, PluralizationService);
                        var section = GetSection(s, pass);

                        pass.Section = section;

                        return pass;
                    });
        }

        private ISection GetSection(T buildSection, BuildPass<T> pass)
        {
            ISection returnValue = null;

            TypeSwitch.On<T>
            (
                TypeSwitch.Case<eBuildSectionEntity>(() =>
                {
                    returnValue = AFSectionFactoryEntity.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionEntityPartial>(() =>
                {
                    returnValue = AFSectionFactoryEntityPartial.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionContext>(() =>
                {
                    returnValue = AFSectionFactoryContext.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionMapping>(() =>
                {
                    returnValue = AFSectionFactoryMapping.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionService>(() =>
                {
                    returnValue = AFSectionFactoryService.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionServiceInterface>(() =>
                {
                    returnValue = AFSectionFactoryServiceInterface.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionEnumeration>(() =>
                {
                    returnValue = AFSectionFactoryEnumeration.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionAppContext>(() =>
                {
                    returnValue = AFSectionFactoryAppContext.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionContract>(() =>
                {
                    returnValue = AFSectionFactoryContract.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionAppService>(() =>
                {
                    returnValue = AFSectionFactoryAppService.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionAppServiceInterface>(() =>
                {
                    returnValue = AFSectionFactoryAppServiceInterface.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionControllerApi>(() =>
                {
                    returnValue = AFSectionFactoryControllerApi.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionControllerMvc>(() =>
                {
                    returnValue = AFSectionFactoryControllerMvc.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Case<eBuildSectionAutoMapperProfile>(() =>
                {
                    returnValue = AFSectionFactoryAutoMapperProfile.CreateSection(buildSection, pass);
                }),
                TypeSwitch.Default(() =>
                {
                    throw new NotSupportedException(string.Concat("Unsupported section type:  ", typeof(T).Name));
                })
            );

            return returnValue;
        }

        private void BuildPassComplete(T buildSection)
        {
            var buildSectionValue = ((object)buildSection).ToString();

            if (_onBuilderPassComplete != null)
                _onBuilderPassComplete(Builder.BuilderType, buildSectionValue);
        }
        #endregion
    }
}
