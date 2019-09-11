using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bedrock.DomainBuilder.Builder;
using Bedrock.DomainBuilder.EntityFramework;
using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder
{
    public class BuildManager
    {
        #region Fields
        private PauseTokenSource _pauseTokenSource;
        #endregion

        #region Constructors
        public BuildManager(eDomain defaultDomain) : this(defaultDomain, null) { }

        public BuildManager(eDomain defaultDomain, Action<eBuilder, IBuilder> addEventsCallack)
        {
            Initialize(defaultDomain, addEventsCallack);
        }
        #endregion

        #region Properties
        public List<IBuilder> Builders { get; private set; }

        public BuildSettings Settings { get; private set; }

        public BuildStatistics Statistics { get; private set; }

        public IEnumerable<string> StoreSchemas
        {
            get { return EntityHelper.GetStoreSchemas(Settings); }
        }

        public bool IsPaused { get { return PauseTokenSource.IsPaused; } }

        public bool IsCancelled { get { return CancellationTokenSource != null && CancellationTokenSource.IsCancellationRequested; } }

        protected PauseTokenSource PauseTokenSource
        {
            get
            {
                _pauseTokenSource = _pauseTokenSource ?? new PauseTokenSource();
                return _pauseTokenSource;
            }
        }

        protected CancellationTokenSource CancellationTokenSource { get; set; }
        #endregion

        #region Public Methods
        public IEnumerable<Task> Build()
        {
            InitializeBuild();

            return Builders
                    .Where(b => b.IsActive)
                    .Select(b => b.Build(Settings));
        }

        public void UpdateActiveBuilders()
        {
            Builders.Each(b =>
            {
                b.IsActive = Settings
                                .ActiveBuilders
                                .Contains(b.BuilderType);
            });
        }

        public bool EnsureActiveBuilders()
        {
            if (!Builders.Any(b => b.IsActive))
                return false;

            return true;
        }

        public void PauseBuild(bool isPaused)
        {
            PauseTokenSource.PauseToken(isPaused);
        }

        public void CancelBuild()
        {
            if (CancellationTokenSource != null)
                CancellationTokenSource.Cancel(false);
        }
        #endregion

        #region Private Methods
        private void Initialize(eDomain defaultDomain, Action<eBuilder, IBuilder> addEventsCallack)
        {
            Settings = BuildSettings.Create(defaultDomain);
            InitializeBuilders(addEventsCallack);
            Statistics = BuildStatistics.Create(this);
        }

        private void InitializeBuild()
        {
            Settings.Verify();
            Statistics.Reset();

            Statistics.StartTime = DateTime.Now;
            PauseTokenSource.PauseToken(true);
            CancellationTokenSource = new CancellationTokenSource();

            UpdateBuilders();
        }

        private void InitializeBuilders(Action<eBuilder, IBuilder> addEventsCallack)
        {
            Builders = new List<IBuilder>();

            Enum.GetValues(typeof(eBuilder)).Cast<eBuilder>().Each(b =>
            {
                var isActive = Settings.ActiveBuilders.Contains(b);
                var builder = AFBuilderFactory.Create(b, isActive, PauseTokenSource.Token);

                AddEventHandlers(builder);

                if (addEventsCallack != null)
                    addEventsCallack(b, builder);

                Builders.Add(builder);
            });
        }

        private void UpdateBuilders()
        {
            var entityHelper = EntityHelper.Create(Settings);
            var roots = BuilderBase.GetRoots(Settings.Domain);
            var enums = BuilderBase.GetEnums(Settings.Domain);

            Builders.Each(b =>
            {
                b.EntityHelper = entityHelper;
                b.Roots = roots;
                b.Enums = enums;
                b.Cancellation = CancellationTokenSource.Token;
            });
        }

        private void AddEventHandlers(IBuilder builder)
        {
            builder.OnBuilderInitialized += Builder_OnBuilderInitialized;
            builder.OnBuilderStarted += Builder_OnBuilderStarted;
            builder.OnBuilderBuilding += Builder_OnBuilderBuilding;
            builder.OnBuilderFileCountUpdate += Builder_OnBuilderFileCountUpdate;
            builder.OnBuilderPassComplete += Builder_OnBuilderPassComplete;
            builder.OnFileBuilding += Builder_OnFileBuilding;
            builder.OnFileBuilt += Builder_OnFileBuilt;
            builder.OnBuilderComplete += Builder_OnBuilderComplete;
            builder.OnBuilderWarning += Builder_OnBuilderWarning;
        }
        #endregion

        #region Event Handlers
        private void Builder_OnBuilderInitialized(eBuilder builder) { }

        private void Builder_OnBuilderStarted(eBuilder builder) { }

        private void Builder_OnBuilderBuilding(eBuilder builder) { }

        private void Builder_OnBuilderFileCountUpdate(eBuilder builder, int fileCount)
        {
            Statistics.AddFilesToCreate(builder, fileCount);
        }

        private void Builder_OnBuilderPassComplete(eBuilder builder, string buildSection) { }

        private void Builder_OnFileBuilding(eBuilder builder, string file) { }

        private void Builder_OnFileBuilt(eBuilder builder, string file, int loc)
        {
            Statistics.AddLoC(builder, loc);
        }

        private void Builder_OnBuilderComplete(eBuilder builder, int filesCreated, int loc)
        {
            Statistics.AddFilesCreated(builder, filesCreated);
            Statistics.BuildersComplete++;

            if (Statistics.IsBuildComplete)
                Statistics.EndTime = DateTime.Now;
        }

        private void Builder_OnBuilderWarning(BuildWarning warning)
        {
            Statistics.AddWarning(warning);
        }
        #endregion
    }
}
