using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using Bedrock.DomainBuilder.EntityFramework;
using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Builder
{
    public abstract class BuilderBase : IBuilder
    {
        #region Fields
        private event BuilderStarted _onBuilderStarted;
        private event BuilderInitialized _onBuilderInitialized;
        private event BuilderBuilding _onBuilderBuilding;
        private event BuilderFileCountUpdate _onBuilderFileCountUpdate;
        private event BuilderPassComplete _onBuilderPassComplete;
        private event BuilderComplete _onBuilderComplete;
        private event BuilderWarning _onBuilderWarning;
        private event FileBuilding _onFileBuilding;
        private event FileBuilt _onFileBuilt;
        #endregion

        #region Events
        public event BuilderStarted OnBuilderStarted
        {
            add { _onBuilderStarted += value; }
            remove { _onBuilderStarted -= value; }
        }

        public event BuilderInitialized OnBuilderInitialized
        {
            add { _onBuilderInitialized += value; }
            remove { _onBuilderInitialized -= value; }
        }

        public event BuilderBuilding OnBuilderBuilding
        {
            add { _onBuilderBuilding += value; }
            remove { _onBuilderBuilding -= value; }
        }

        public event BuilderFileCountUpdate OnBuilderFileCountUpdate
        {
            add { _onBuilderFileCountUpdate += value; }
            remove { _onBuilderFileCountUpdate -= value; }
        }

        public event BuilderPassComplete OnBuilderPassComplete
        {
            add { _onBuilderPassComplete += value; }
            remove { _onBuilderPassComplete -= value; }
        }

        public event BuilderComplete OnBuilderComplete
        {
            add { _onBuilderComplete += value; }
            remove { _onBuilderComplete -= value; }
        }

        public event BuilderWarning OnBuilderWarning
        {
            add { _onBuilderWarning += value; }
            remove { _onBuilderWarning -= value; }
        }

        public event FileBuilding OnFileBuilding
        {
            add { _onFileBuilding += value; }
            remove { _onFileBuilding -= value; }
        }

        public event FileBuilt OnFileBuilt
        {
            add { _onFileBuilt += value; }
            remove { _onFileBuilt -= value; }
        }
        #endregion

        #region Protected Properties
        protected int LoC { get; set; }

        protected string SchemaCamelCased { get; set; }
        #endregion

        #region IBuilder Properties
        public eBuilder BuilderType { get; set; }

        public bool IsActive { get; set; }

        public EntityHelper EntityHelper { get; set; }

        public List<string> Roots { get; set; }

        public List<string> Enums { get; set; }

        public PauseToken Pause { get; set; }

        public CancellationToken Cancellation { get; set; }
        #endregion

        #region IBuilder Methods
        public virtual async Task Build(BuildSettings settings)
        {
            LoC = 0;

            await Task.Run(async () =>
            {
                Initialize(settings);
                BuilderBuilding();
                Complete(await BuildInternal(settings));
            });
        }

        public virtual void BuilderWarning(string section, string message)
        {
            if (_onBuilderWarning != null)
                _onBuilderWarning(BuildWarning.Create(BuilderType, section, message));
        }
        #endregion

        #region Public Methods
        public static List<string> GetRoots(eDomain domain)
        {
            return GetConfigItems(domain, "Root")
                    .Select(e => StringBuilder<int>.PluralizationService.Singularize(e))
                    .ToList();
        }

        public static List<string> GetEnums(eDomain domain)
        {
            return GetConfigItems(domain, "Enum")
                    .Select(e => StringBuilder<int>.PluralizationService.Singularize(e))
                    .ToList();
        }
        #endregion

        #region Event Handlers
        protected void BuilderPassComplete(eBuilder builder, string buildSection)
        {
            if (_onBuilderPassComplete != null)
                _onBuilderPassComplete(builder, buildSection);
        }
        #endregion

        #region Protected Methods
        protected virtual void Initialize(BuildSettings settings)
        {
            BuilderStarted();

            var path = settings.GetBuilderPath(BuilderType);
            SchemaCamelCased = settings.StoreSchemaNamespace.ToLower() == "dbo" ? string.Empty : settings.StoreSchemaNamespace.ToCamelCase();

            IOHelper.EnsurePath(path);
            IOHelper.CleanPath(path, settings);

            BuilderInitialized();
        }

        protected virtual async Task<int> BuildInternal(BuildSettings settings)
        {
            return await Task.FromResult(0);
        }

        protected virtual async Task BuildFile<T>(string filePath, EntityType entityType, BuildSettings settings)
            where T : struct, IConvertible
        {
            FileBuilding(filePath);

            var stringBuilder = CreateStringBuilder<T>(entityType, EntityHelper, settings, this, Roots, Enums, Pause, Cancellation);
            var fileText = await stringBuilder.Build();

            IOHelper.WriteFile(filePath, fileText);
            FileBuilt(filePath, GetUpdateLoC(fileText));
        }

        protected virtual void Complete(int filesCreated)
        {
            BuilderComplete(filesCreated, LoC);
        }

        protected StringBuilder<T> CreateStringBuilder<T>(EntityHelper entityHelper, BuildSettings settings, IBuilder builder,
                                                            List<string> roots, List<string> enumerations, PauseToken pause, CancellationToken cancellation)
            where T : struct, IConvertible
        {
            return CreateStringBuilder<T>(null, entityHelper, settings, builder, roots, enumerations, pause, cancellation);
        }

        protected StringBuilder<T> CreateStringBuilder<T>(EntityType entityType, EntityHelper entityHelper, BuildSettings settings, IBuilder builder,
                                                            List<string> roots, List<string> enumerations, PauseToken pause, CancellationToken cancellation)
            where T : struct, IConvertible
        {
            var returnValue = new StringBuilder<T>(entityType, entityHelper, settings, builder, roots, enumerations, pause, cancellation);
            returnValue.OnBuilderPassComplete += BuilderPassComplete;
            return returnValue;
        }
        #endregion

        #region Protected Methods (Events)
        protected void BuilderStarted()
        {
            if (_onBuilderStarted != null)
                _onBuilderStarted(BuilderType);
        }

        protected void BuilderInitialized()
        {
            if (_onBuilderInitialized != null)
                _onBuilderInitialized(BuilderType);
        }

        protected void BuilderBuilding()
        {
            if (_onBuilderBuilding != null)
                _onBuilderBuilding(BuilderType);
        }

        protected void BuilderFileCountUpdate(int fileCount)
        {
            if (_onBuilderFileCountUpdate != null)
                _onBuilderFileCountUpdate(BuilderType, fileCount);
        }

        protected void BuilderComplete(int filesCreated, int loc)
        {
            if (_onBuilderComplete != null)
                _onBuilderComplete(BuilderType, filesCreated, loc);
        }

        protected void FileBuilding(string file)
        {
            if (_onFileBuilding != null)
                _onFileBuilding(BuilderType, file);
        }

        protected void FileBuilt(string file, int loc)
        {
            if (_onFileBuilt != null)
                _onFileBuilt(BuilderType, file, loc);
        }
        #endregion

        #region Private Methods
        private int GetUpdateLoC(string text)
        {
            var returnValue = text
                                .Select(e => Environment.NewLine)
                                .Count();

            LoC += returnValue;

            return returnValue;
        }

        private static string[] GetConfigItems(eDomain domain, string tagName)
        {
            var doc = new XmlDocument();
            var absolutePath = IOHelper.GetExecutionPathLocation("\\Config\\", string.Format("{0}s{1}.xml", tagName, domain));

            doc.Load(absolutePath);

            var nodes = new List<XmlNode>(doc.DocumentElement.GetElementsByTagName(tagName).OfType<XmlNode>());
            var items = nodes.Select(n => n.FirstChild.Value).ToArray();

            return items;
        }
        #endregion
    }
}
