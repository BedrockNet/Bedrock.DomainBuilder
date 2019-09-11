using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder
{
    public sealed class BuildStatistics
    {
        #region Fields
        private ConcurrentDictionary<eBuilder, int> _builderFilesCreated;
        private ConcurrentDictionary<eBuilder, int> _builderLoC;
        private ConcurrentBag<BuildWarning> _buildWarnings;

        private int? _builderCount;
        #endregion

        #region Constructors
        private BuildStatistics() { }
        #endregion

        #region Properties
        private BuildManager BuildManager { get; set; }

        private ConcurrentDictionary<eBuilder, int> BuilderFilesCreated
        {
            get
            {
                _builderFilesCreated = _builderFilesCreated ?? new ConcurrentDictionary<eBuilder, int>(BuilderCount, BuilderCount);
                return _builderFilesCreated;
            }
        }

        private ConcurrentDictionary<eBuilder, int> BuilderLoC
        {
            get
            {
                _builderLoC = _builderLoC ?? new ConcurrentDictionary<eBuilder, int>(BuilderCount, BuilderCount);
                return _builderLoC;
            }
        }

        private ConcurrentBag<BuildWarning> BuildWarnings
        {
            get
            {
                _buildWarnings = _buildWarnings ?? new ConcurrentBag<BuildWarning>();
                return _buildWarnings;
            }
        }

        public int FilesCreatedEntity
        {
            get { return GetFilesCreated(eBuilder.Entity); }
        }

        public int FilesCreatedEntityPartial
        {
            get { return GetFilesCreated(eBuilder.EntityPartial); }
        }

        public int FilesCreatedMapping
        {
            get { return GetFilesCreated(eBuilder.Mapping); }
        }

        public int FilesCreatedContext
        {
            get { return GetFilesCreated(eBuilder.Context); }
        }

        public int FilesCreatedService
        {
            get { return GetFilesCreated(eBuilder.Service); }
        }

        public int FilesCreatedServiceInterface
        {
            get { return GetFilesCreated(eBuilder.ServiceInterface); }
        }

        public int FilesCreatedEnumeration
        {
            get { return GetFilesCreated(eBuilder.Enumeration); }
        }

        public int FilesCreatedAppContext
        {
            get { return GetFilesCreated(eBuilder.AppContext); }
        }

        public int FilesCreatedContract
        {
            get { return GetFilesCreated(eBuilder.Contract); }
        }

        public int FilesCreatedAppService
        {
            get { return GetFilesCreated(eBuilder.AppService); }
        }

        public int FilesCreatedAppServiceInterface
        {
            get { return GetFilesCreated(eBuilder.AppServiceInterface); }
        }

        public int FilesCreatedControllerApi
        {
            get { return GetFilesCreated(eBuilder.ControllerApi); }
        }

        public int FilesCreatedControllerMvc
        {
            get { return GetFilesCreated(eBuilder.ControllerMvc); }
        }

        public int FilesCreatedTotal
        {
            get { return BuilderFilesCreated.Sum(l => l.Value); }
        }

        public int LoCEntity
        {
            get { return GetLoC(eBuilder.Entity); }
        }

        public int LoCEntityPartial
        {
            get { return GetLoC(eBuilder.EntityPartial); }
        }

        public int LoCMapping
        {
            get { return GetLoC(eBuilder.Mapping); }
        }

        public int LoCContext
        {
            get { return GetLoC(eBuilder.Context); }
        }

        public int LoCService
        {
            get { return GetLoC(eBuilder.Service); }
        }

        public int LoCServiceInterface
        {
            get { return GetLoC(eBuilder.ServiceInterface); }
        }

        public int LoCEnumeration
        {
            get { return GetLoC(eBuilder.Enumeration); }
        }

        public int LoCAppContext
        {
            get { return GetLoC(eBuilder.AppContext); }
        }

        public int LoCContract
        {
            get { return GetLoC(eBuilder.Contract); }
        }

        public int LoCAppService
        {
            get { return GetLoC(eBuilder.AppService); }
        }

        public int LoCAppServiceInterface
        {
            get { return GetLoC(eBuilder.AppServiceInterface); }
        }

        public int LoCControllerApi
        {
            get { return GetLoC(eBuilder.ControllerApi); }
        }

        public int LoCControllerMvc
        {
            get { return GetLoC(eBuilder.ControllerMvc); }
        }

        public int LoCTotal
        {
            get { return BuilderLoC.Sum(l => l.Value); }
        }

        public IEnumerable<BuildWarning> WarningsEntity
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.Entity); }
        }

        public IEnumerable<BuildWarning> WarningsEntityPartial
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.EntityPartial); }
        }

        public IEnumerable<BuildWarning> WarningsMapping
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.Mapping); }
        }

        public IEnumerable<BuildWarning> WarningsContext
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.Context); }
        }

        public IEnumerable<BuildWarning> WarningsService
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.Service); }
        }

        public IEnumerable<BuildWarning> WarningsServiceInterface
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.ServiceInterface); }
        }

        public IEnumerable<BuildWarning> WarningsEnumeration
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.Enumeration); }
        }

        public IEnumerable<BuildWarning> WarningsAppContext
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.AppContext); }
        }

        public IEnumerable<BuildWarning> WarningsContract
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.Contract); }
        }

        public IEnumerable<BuildWarning> WarningsAppService
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.AppService); }
        }

        public IEnumerable<BuildWarning> WarningsAppServiceInterface
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.AppServiceInterface); }
        }

        public IEnumerable<BuildWarning> WarningsControllerApi
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.ControllerApi); }
        }

        public IEnumerable<BuildWarning> WarningsControllerMvc
        {
            get { return _buildWarnings.Where(w => w.Builder == eBuilder.ControllerMvc); }
        }

        public IEnumerable<BuildWarning> WarningsAll
        {
            get { return _buildWarnings.AsEnumerable(); }
        }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public TimeSpan ElapsedTime
        {
            get { return EndTime.Value - StartTime.Value; }
        }

        public bool IsBuildComplete
        {
            get { return BuildersComplete == BuilderCountActive; }
        }

        public int TotalFilesToBuild { get; private set; }

        public bool IsAllBuildersReportedFileCount
        {
            get { return BuilderCountFiles == BuilderCountActive; }
        }

        private int BuilderCountFiles { get; set; }

        internal int BuildersComplete { get; set; }

        internal int BuilderCount
        {
            get
            {
                _builderCount = _builderCount ?? Enum.GetValues(typeof(eBuilder)).Length;
                return _builderCount.Value;
            }
        }

        internal int BuilderCountActive
        {
            get
            {
                return BuildManager
                            .Builders
                            .Where(b => b.IsActive)
                            .Count();
            }
        }
        #endregion

        #region Public Methods
        public static BuildStatistics Create(BuildManager buildManager)
        {
            return new BuildStatistics
            {
                BuildManager = buildManager
            };
        }

        public void AddFilesToCreate(eBuilder builder, int fileCount)
        {
            TotalFilesToBuild += fileCount;
            BuilderCountFiles++;
        }

        public void AddFilesCreated(eBuilder builder, int filesCreated)
        {
            BuilderFilesCreated.AddOrUpdate(builder, filesCreated, (key, oldValue) => oldValue + filesCreated);
        }

        public void AddLoC(eBuilder builder, int loc)
        {
            BuilderLoC.AddOrUpdate(builder, loc, (key, oldValue) => oldValue + loc);
        }

        public void AddWarning(eBuilder builder, string section, string message)
        {
            var warning = BuildWarning.Create(builder, section, message);
            AddWarning(warning);
        }

        public void AddWarning(BuildWarning warning)
        {
            BuildWarnings.Add(warning);
        }

        public void Reset()
        {
            BuilderFilesCreated.Clear();
            BuilderLoC.Clear();
            ClearWarnings();
            BuildersComplete = 0;
            StartTime = null;
            EndTime = null;
            TotalFilesToBuild = 0;
            BuilderCountFiles = 0;
        }
        #endregion

        #region Public Methods (Message)
        public string GetBuildCompleteMessage()
        {
            var returnValue = new StringBuilder();

            returnValue.AppendLine("--Build Complete--");
            returnValue.AppendLine(string.Format("Start Time: {0}", StartTime.Value.ToLongTimeString()));
            returnValue.AppendLine(string.Format("End Time: {0}", EndTime.Value.ToLongTimeString()));
            returnValue.AppendLine(string.Format("Elapsed Time: {0} seconds", ElapsedTime.TotalSeconds.ToString()));

            return returnValue.ToString();
        }

        public string GetFilesCreatedMessage()
        {
            var returnValue = new StringBuilder();

            returnValue.AppendLine("--Files Created--");
            returnValue.AppendLine(string.Format("Entity: {0}", FilesCreatedEntity.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Entity Partial: {0}", FilesCreatedEntityPartial.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Mapping: {0}", FilesCreatedMapping.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Context: {0}", FilesCreatedContext.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Service: {0}", FilesCreatedService.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("ServiceInterface: {0}", FilesCreatedServiceInterface.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Enumeration: {0}", FilesCreatedEnumeration.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("AppContext: {0}", FilesCreatedAppContext.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Contract: {0}", FilesCreatedContract.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("AppService: {0}", FilesCreatedAppService.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("AppServiceInterface: {0}", FilesCreatedAppServiceInterface.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("ControllerApi: {0}", FilesCreatedControllerApi.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("ControllerMvc: {0}", FilesCreatedControllerMvc.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Total: {0}", FilesCreatedTotal.AddThousandthsPlaceCommas()));

            return returnValue.ToString();
        }

        public string GetLoCMessage()
        {
            var returnValue = new StringBuilder();

            returnValue.AppendLine("--LoC--");
            returnValue.AppendLine(string.Format("Entity: {0}", LoCEntity.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Entity Partial: {0}", LoCEntityPartial.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Mapping: {0}", LoCMapping.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Context: {0}", LoCContext.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Service: {0}", LoCService.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("ServiceInterface: {0}", LoCServiceInterface.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Enumeration: {0}", LoCEnumeration.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("AppContext: {0}", LoCAppContext.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Contract: {0}", LoCContract.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("AppService: {0}", LoCAppService.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("AppServiceInterface: {0}", LoCAppServiceInterface.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("ControllerApi: {0}", LoCControllerApi.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("ControllerMvc: {0}", LoCControllerMvc.AddThousandthsPlaceCommas()));
            returnValue.AppendLine(string.Format("Total: {0}", LoCTotal.AddThousandthsPlaceCommas()));

            return returnValue.ToString();
        }

        public static string GetWarningMessage(BuildWarning warning)
        {
            var returnValue = new StringBuilder();

            returnValue.AppendLine("--Warning--");
            returnValue.AppendLine(string.Format("Builder: {0}", warning.Builder));
            returnValue.AppendLine(string.Format("Section: {0}", warning.Section));
            returnValue.AppendLine(string.Format("Message: {0}", warning.Message));

            return returnValue.ToString();
        }

        public string GetWarningsMessage()
        {
            var returnValue = new StringBuilder();

            returnValue.AppendLine("--Warnings Count--");
            returnValue.AppendLine(string.Format("Entity: {0}", WarningsEntity.Count()));
            returnValue.AppendLine(string.Format("Entity Partial: {0}", WarningsEntityPartial.Count()));
            returnValue.AppendLine(string.Format("Mapping: {0}", WarningsMapping.Count()));
            returnValue.AppendLine(string.Format("Context: {0}", WarningsContext.Count()));
            returnValue.AppendLine(string.Format("Service: {0}", WarningsService.Count()));
            returnValue.AppendLine(string.Format("ServiceInterface: {0}", WarningsServiceInterface.Count()));
            returnValue.AppendLine(string.Format("Enumeration: {0}", WarningsEnumeration.Count()));
            returnValue.AppendLine(string.Format("AppContext: {0}", WarningsAppContext.Count()));
            returnValue.AppendLine(string.Format("Contract: {0}", WarningsContract.Count()));
            returnValue.AppendLine(string.Format("AppService: {0}", WarningsAppService.Count()));
            returnValue.AppendLine(string.Format("AppServiceInterface: {0}", WarningsAppServiceInterface.Count()));
            returnValue.AppendLine(string.Format("ControllerApi: {0}", WarningsControllerApi.Count()));
            returnValue.AppendLine(string.Format("ControllerMvc: {0}", WarningsControllerMvc.Count()));
            returnValue.AppendLine(string.Format("Total: {0}", WarningsAll.Count()));

            return returnValue.ToString();
        }
        #endregion

        #region Private Methods
        private int GetFilesCreated(eBuilder builder)
        {
            int returnValue;
            BuilderFilesCreated.TryGetValue(builder, out returnValue);
            return returnValue;
        }

        private int GetLoC(eBuilder builder)
        {
            int returnValue;
            BuilderLoC.TryGetValue(builder, out returnValue);
            return returnValue;
        }

        private void ClearWarnings()
        {
            BuildWarning warning;

            while (!BuildWarnings.IsEmpty)
                BuildWarnings.TryTake(out warning);
        }
        #endregion
    }
}
