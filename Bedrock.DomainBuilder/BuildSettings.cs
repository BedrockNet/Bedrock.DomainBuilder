using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder
{
    public class BuildSettings
    {
        #region Fields
        private eDomain? _domain;
        private string _connectionString;
        private string _domainPath;
        private string _namespaceEntity;
        private string _namespaceShared;
        private eEntityType? _entityType;
        private ePrimaryKeyType? _primaryKeyType;
        private string _fileExtension;
        private bool? _isValidatable;
        private bool? _isIncludeSpatialType;
        private bool? _isIncludeCrud;
        private bool? _isIncludeCrudEntity;
        private bool? _isRecursiveCrud;
        private bool? _isIncludePrimaryKey;
        private bool? _isIncludeAuditFields;
        private bool? _isIncludeDeletableFields;
        private bool? _isIncludeProperties;
        private bool? _isCleanPath;
        private bool? _isNetCore;
        private List<eBuilder> _activeBuilders;
        private string _namespaceContext;
        private eContextType? _contextType;
        private string _namespaceMapping;
        private string _commonKey;
        private bool? _isUseCommonKey;
        private string _namespaceService;
        private eServiceType? _serviceType;
        private string _namespaceEnumeration;
        private bool? _isOpenFolder;
        private bool? _isCascadeOnDelete;
        private bool? _isIgnoreRootChildren;
        private string _namespaceDomain;
        private List<string> _entityExclusions;
        private List<string> _propertyInclusions;
        private string _namespaceContract;
        private string _contractName;
        private eContractType? _contractType;
        private string _namespaceAppService;
        private eAppServiceType? _appServiceType;
        private string _namespaceControllerApi;
        private eControllerApiType? _controllerApiType;
        private string _namespaceControllerMvc;
        private eControllerMvcType? _controllerMvcType;
        private string _storeSchemaNamespace;
        private bool? _isUsePropertyInclusions;
        private bool? _isTracked;
        private string _namespaceInfrastructure;
        #endregion

        #region Constructors
        private BuildSettings()
        {
            StoreSchemaNamespace = "dbo";
        }
        #endregion

        #region Properties
        public string Delimeter { get { return ","; } }

        public eDomain Domain
        {
            get { return _domain.Value; }
            set
            {
                _domain = value;
                Reset();
            }
        }

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    var key = string.Concat("Bedrock.ConnectionString.", Domain.ToString());
                    _connectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                }

                return _connectionString;
            }
            set { _connectionString = value; }
        }

        public string ProviderInvariantName
        {
            get { return "System.Data.SqlClient"; }
        }

        public string StoreSchemaNamespace
        {
            get { return _storeSchemaNamespace; }
            set { _storeSchemaNamespace = value; }
        }

        public string DomainPath
        {
            get
            {
                if (string.IsNullOrEmpty(_domainPath))
                {
                    var key = "Bedrock.Domain.Path";
                    _domainPath = string.Format(ConfigurationManager.AppSettings[key], Domain.ToString());
                }

                return _domainPath;
            }
            set { _domainPath = value; }
        }

        public string EntityPath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\Entity\\Model\\"); }
        }

        public string EntityPathPartial
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\Entity\\"); }
        }

        public string ContextPath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\Context\\"); }
        }

        public string MappingPath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\Mapping\\"); }
        }

        public string ServicePath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\Service\\"); }
        }

        public string ServiceInterfacePath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\Service\\Interface\\"); }
        }

        public string EnumerationPath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\Enumeration\\"); }
        }

        public string AppContextPath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\AppContext\\"); }
        }

        public string ContractPath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\Contract\\"); }
        }

        public string AppServicePath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\AppService\\"); }
        }

        public string AppServiceInterfacePath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\AppService\\Interface\\"); }
        }

        public string ControllerApiPath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\ControllerApi\\"); }
        }

        public string ControllerMvcPath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\ControllerMvc\\"); }
        }

        public string AutoMapperProfilePath
        {
            get { return string.Concat(DomainPath, StoreSchemaNamespace, "\\AutoMapperProfile\\"); }
        }

        public string NamespaceEntity
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceEntity))
                {
                    var key = "Bedrock.Entity.Namespace";
                    _namespaceEntity = string.Format(ConfigurationManager.AppSettings[key], Domain.ToString());
                }

                return _namespaceEntity;
            }
            set { _namespaceEntity = value; }
        }

        public string NamespaceShared
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceShared))
                {
                    var key = "Bedrock.Domain.Namespace.Shared";
                    _namespaceShared = ConfigurationManager.AppSettings[key];
                }

                return _namespaceShared;
            }
            set { _namespaceShared = value; }
        }

        public string NamespaceDomain
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceDomain))
                {
                    var key = "Bedrock.Domain.Namespace";
                    _namespaceDomain = string.Format(ConfigurationManager.AppSettings[key], Domain);
                }

                return _namespaceDomain;
            }
            set { _namespaceDomain = value; }
        }

        public eEntityType EntityType
        {
            get
            {
                if (!_entityType.HasValue)
                {
                    var key = "Bedrock.Entity.Type";
                    var value = ConfigurationManager.AppSettings[key];

                    _entityType = (eEntityType)Enum.Parse(typeof(eEntityType), value, true);
                }

                return _entityType.Value;
            }
            set { _entityType = value; }
        }

        public ePrimaryKeyType PrimaryKeyType
        {
            get
            {
                if (!_primaryKeyType.HasValue)
                {
                    var key = "Bedrock.Entity.PrimaryKeyType";
                    var value = ConfigurationManager.AppSettings[key];

                    _primaryKeyType = (ePrimaryKeyType)Enum.Parse(typeof(ePrimaryKeyType), value, true);
                }

                return _primaryKeyType.Value;
            }
            set { _primaryKeyType = value; }
        }

        public string FileExtension
        {
            get
            {
                if (string.IsNullOrEmpty(_fileExtension))
                {
                    var key = "Bedrock.Domain.File.Extension";
                    _fileExtension = ConfigurationManager.AppSettings[key];
                }

                return _fileExtension;
            }
            set { _fileExtension = value; }
        }

        public bool IsValidatable
        {
            get
            {
                if (!_isValidatable.HasValue)
                {
                    var key = "Bedrock.Entity.IsValidatable";
                    var value = ConfigurationManager.AppSettings[key];

                    _isValidatable = bool.Parse(value);
                }

                return _isValidatable.Value;
            }
            set { _isValidatable = value; }
        }

        public bool IsIncludeSpatialType
        {
            get
            {
                if (!_isIncludeSpatialType.HasValue)
                {
                    var key = "Bedrock.Entity.IsIncludeSpatialType";
                    var value = ConfigurationManager.AppSettings[key];

                    _isIncludeSpatialType = bool.Parse(value);
                }

                return _isIncludeSpatialType.Value;
            }
            set { _isIncludeSpatialType = value; }
        }

        public bool IsIncludeCrud
        {
            get
            {
                if (!_isIncludeCrud.HasValue)
                {
                    var key = "Bedrock.Domain.IsIncludeCrud";
                    var value = ConfigurationManager.AppSettings[key];

                    _isIncludeCrud = bool.Parse(value);
                }

                return _isIncludeCrud.Value;
            }
            set { _isIncludeCrud = value; }
        }

        public bool IsIncludeCrudEntity
        {
            get
            {
                if (!_isIncludeCrudEntity.HasValue)
                {
                    var key = "Bedrock.Entity.IsIncludeCrud";
                    var value = ConfigurationManager.AppSettings[key];

                    _isIncludeCrudEntity = bool.Parse(value);
                }

                return _isIncludeCrudEntity.Value;
            }
            set { _isIncludeCrudEntity = value; }
        }

        public bool IsRecursiveCrud
        {
            get
            {
                if (!_isRecursiveCrud.HasValue)
                {
                    var key = "Bedrock.Domain.IsRecursiveCrud";
                    var value = ConfigurationManager.AppSettings[key];

                    _isRecursiveCrud = bool.Parse(value);
                }

                return _isRecursiveCrud.Value;
            }
            set { _isRecursiveCrud = value; }
        }

        public bool IsIncludePrimaryKey
        {
            get
            {
                if (!_isIncludePrimaryKey.HasValue)
                {
                    var key = "Bedrock.Entity.IsIncludePrimaryKey";
                    var value = ConfigurationManager.AppSettings[key];

                    _isIncludePrimaryKey = bool.Parse(value);
                }

                return _isIncludePrimaryKey.Value;
            }
            set { _isIncludePrimaryKey = value; }
        }

        public bool IsIncludeAuditFields
        {
            get
            {
                if (!_isIncludeAuditFields.HasValue)
                {
                    var key = "Bedrock.Entity.IsIncludeAuditFields";
                    var value = ConfigurationManager.AppSettings[key];

                    _isIncludeAuditFields = bool.Parse(value);
                }

                return _isIncludeAuditFields.Value;
            }
            set { _isIncludeAuditFields = value; }
        }

        public bool IsIncludeDeletableFields
        {
            get
            {
                if (!_isIncludeDeletableFields.HasValue)
                {
                    var key = "Bedrock.Entity.IsIncludeDeletableFields";
                    var value = ConfigurationManager.AppSettings[key];

                    _isIncludeDeletableFields = bool.Parse(value);
                }

                return _isIncludeDeletableFields.Value;
            }
            set { _isIncludeDeletableFields = value; }
        }

        public bool IsIncludeProperties
        {
            get
            {
                if (!_isIncludeProperties.HasValue)
                {
                    var key = "Bedrock.Entity.IsIncludeProperties";
                    var value = ConfigurationManager.AppSettings[key];

                    _isIncludeProperties = bool.Parse(value);
                }

                return _isIncludeProperties.Value;
            }
            set { _isIncludeProperties = value; }
        }

        public bool IsCleanPath
        {
            get
            {
                if (!_isCleanPath.HasValue)
                {
                    var key = "Bedrock.Domain.IsCleanPath";
                    var value = ConfigurationManager.AppSettings[key];

                    _isCleanPath = bool.Parse(value);
                }

                return _isCleanPath.Value;
            }
            set { _isCleanPath = value; }
        }

        public bool IsNetCore
        {
            get
            {
                if (!_isNetCore.HasValue)
                {
                    var key = "Bedrock.Domain.IsNetCore";
                    var value = ConfigurationManager.AppSettings[key];

                    _isNetCore = bool.Parse(value);
                }

                return _isNetCore.Value;
            }
            set { _isNetCore = value; }
        }

        public List<eBuilder> ActiveBuilders
        {
            get
            {
                if (_activeBuilders == null)
                {
                    var configBuilders = ConfigurationManager
                                            .AppSettings["Bedrock.Domain.ActiveBuilders"]
                                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(b => (eBuilder)Enum.Parse(typeof(eBuilder), b, true));

                    _activeBuilders = new List<eBuilder>(configBuilders);
                }

                return _activeBuilders;
            }
        }

        public string NamespaceContext
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceContext))
                {
                    var key = "Bedrock.Context.Namespace";
                    _namespaceContext = string.Format(ConfigurationManager.AppSettings[key], Domain.ToString());
                }

                return _namespaceContext;
            }
            set { _namespaceContext = value; }
        }

        public eContextType ContextType
        {
            get
            {
                if (!_contextType.HasValue)
                {
                    var key = "Bedrock.Context.Type";
                    var value = ConfigurationManager.AppSettings[key];

                    _contextType = (eContextType)Enum.Parse(typeof(eContextType), value, true);
                }

                return _contextType.Value;
            }
            set { _contextType = value; }
        }

        public string NamespaceMapping
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceMapping))
                {
                    var key = "Bedrock.Mapping.Namespace";
                    _namespaceMapping = string.Format(ConfigurationManager.AppSettings[key], Domain.ToString());
                }

                return _namespaceMapping;
            }
            set { _namespaceMapping = value; }
        }

        public string CommonKey
        {
            get
            {
                if (string.IsNullOrEmpty(_commonKey))
                {
                    _commonKey = ConfigurationManager.AppSettings["Bedrock.Domain.CommonKey"];
                }

                return _commonKey;
            }
            set { _commonKey = value; }
        }

        public bool IsUseCommonKey
        {
            get
            {
                if (!_isUseCommonKey.HasValue)
                {
                    var key = "Bedrock.Domain.IsUseCommonKey";
                    var value = ConfigurationManager.AppSettings[key];

                    _isUseCommonKey = bool.Parse(value);
                }

                return _isUseCommonKey.Value;
            }
            set { _isUseCommonKey = value; }
        }

        public string NamespaceService
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceService))
                {
                    var key = "Bedrock.Service.Namespace";
                    _namespaceService = string.Format(ConfigurationManager.AppSettings[key], Domain.ToString());
                }

                return _namespaceService;
            }
            set { _namespaceService = value; }
        }

        public eServiceType ServiceType
        {
            get
            {
                if (!_serviceType.HasValue)
                {
                    var key = "Bedrock.Service.Type";
                    var value = ConfigurationManager.AppSettings[key];

                    _serviceType = (eServiceType)Enum.Parse(typeof(eServiceType), value, true);
                }

                return _serviceType.Value;
            }
            set { _serviceType = value; }
        }

        public string NamespaceEnumeration
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceEnumeration))
                {
                    var key = "Bedrock.Enumeration.Namespace";
                    _namespaceEnumeration = string.Format(ConfigurationManager.AppSettings[key], Domain.ToString());
                }

                return _namespaceEnumeration;
            }
            set { _namespaceEnumeration = value; }
        }

        public bool IsOpenFolder
        {
            get
            {
                if (!_isOpenFolder.HasValue)
                {
                    var key = "Bedrock.Domain.IsOpenFolder";
                    var value = ConfigurationManager.AppSettings[key];

                    _isOpenFolder = bool.Parse(value);
                }

                return _isOpenFolder.Value;
            }
            set { _isOpenFolder = value; }
        }

        public bool IsCascadeOnDelete
        {
            get
            {
                if (!_isCascadeOnDelete.HasValue)
                {
                    var key = "Bedrock.Domain.IsCascadeOnDelete";
                    var value = ConfigurationManager.AppSettings[key];

                    _isCascadeOnDelete = bool.Parse(value);
                }

                return _isCascadeOnDelete.Value;
            }
            set { _isCascadeOnDelete = value; }
        }

        public bool IsIgnoreRootChildren
        {
            get
            {
                if (!_isIgnoreRootChildren.HasValue)
                {
                    var key = "Bedrock.Domain.IsIgnoreRootChildren";
                    var value = ConfigurationManager.AppSettings[key];

                    _isIgnoreRootChildren = bool.Parse(value);
                }

                return _isIgnoreRootChildren.Value;
            }
            set { _isIgnoreRootChildren = value; }
        }

        public List<string> EntityExclusions
        {
            get
            {
                if (_entityExclusions == null)
                {
                    var key = string.Format("Bedrock.Domain.EntityExclusions.{0}", Domain.ToString());
                    var entityExclusions = ConfigurationManager
                                            .AppSettings[key]
                                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    _entityExclusions = new List<string>(entityExclusions);
                }

                return _entityExclusions;
            }
        }

        public List<string> PropertyInclusions
        {
            get
            {
                if (_propertyInclusions == null)
                {
                    var key = string.Format("Bedrock.Domain.PropertyInclusions.{0}", Domain.ToString());
                    var propertyInclusions = ConfigurationManager
                                            .AppSettings[key]
                                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    _propertyInclusions = new List<string>(propertyInclusions);
                }

                return _propertyInclusions;
            }
        }

        public string NamespaceContract
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceContract))
                {
                    var key = "Bedrock.Contract.Namespace";
                    _namespaceContract = string.Format(ConfigurationManager.AppSettings[key], Domain);
                }

                return _namespaceContract;
            }
            set { _namespaceContract = value; }
        }

        public string ContractName
        {
            get
            {
                if (string.IsNullOrEmpty(_contractName))
                    _contractName = ConfigurationManager.AppSettings["Bedrock.Contract.Name"];

                return _contractName;
            }
            set { _contractName = value; }
        }

        public eContractType ContractType
        {
            get
            {
                if (!_contractType.HasValue)
                {
                    var key = "Bedrock.Contract.Type";
                    var value = ConfigurationManager.AppSettings[key];

                    _contractType = (eContractType)Enum.Parse(typeof(eContractType), value, true);
                }

                return _contractType.Value;
            }
            set { _contractType = value; }
        }

        public string NamespaceAppService
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceAppService))
                {
                    var key = "Bedrock.AppService.Namespace";
                    _namespaceAppService = string.Format(ConfigurationManager.AppSettings[key], Domain);
                }

                return _namespaceAppService;
            }
            set { _namespaceAppService = value; }
        }

        public eAppServiceType AppServiceType
        {
            get
            {
                if (!_appServiceType.HasValue)
                {
                    var key = "Bedrock.AppService.Type";
                    var value = ConfigurationManager.AppSettings[key];

                    _appServiceType = (eAppServiceType)Enum.Parse(typeof(eAppServiceType), value, true);
                }

                return _appServiceType.Value;
            }
            set { _appServiceType = value; }
        }

        public string NamespaceControllerApi
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceControllerApi))
                {
                    var key = "Bedrock.ControllerApi.Namespace";
                    _namespaceControllerApi = string.Format(ConfigurationManager.AppSettings[key], Domain);
                }

                return _namespaceControllerApi;
            }
            set { _namespaceControllerApi = value; }
        }

        public eControllerApiType ControllerApiType
        {
            get
            {
                if (!_controllerApiType.HasValue)
                {
                    var key = "Bedrock.ControllerApi.Type";
                    var value = ConfigurationManager.AppSettings[key];

                    _controllerApiType = (eControllerApiType)Enum.Parse(typeof(eControllerApiType), value, true);
                }

                return _controllerApiType.Value;
            }
            set { _controllerApiType = value; }
        }

        public string NamespaceControllerMvc
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceControllerMvc))
                {
                    var key = "Bedrock.ControllerMvc.Namespace";
                    _namespaceControllerMvc = string.Format(ConfigurationManager.AppSettings[key], Domain);
                }

                return _namespaceControllerMvc;
            }
            set { _namespaceControllerMvc = value; }
        }

        public eControllerMvcType ControllerMvcType
        {
            get
            {
                if (!_controllerMvcType.HasValue)
                {
                    var key = "Bedrock.ControllerMvc.Type";
                    var value = ConfigurationManager.AppSettings[key];

                    _controllerMvcType = (eControllerMvcType)Enum.Parse(typeof(eControllerMvcType), value, true);
                }

                return _controllerMvcType.Value;
            }
            set { _controllerMvcType = value; }
        }

        public bool IsUsePropertyInclusions
        {
            get
            {
                if (!_isUsePropertyInclusions.HasValue)
                {
                    var key = "Bedrock.Domain.IsUsePropertyInclusions";
                    var value = ConfigurationManager.AppSettings[key];

                    _isUsePropertyInclusions = bool.Parse(value);
                }

                return _isUsePropertyInclusions.Value;
            }
            set { _isUsePropertyInclusions = value; }
        }

        public bool IsTracked
        {
            get
            {
                if (!_isTracked.HasValue)
                {
                    var key = "Bedrock.Domain.IsTracked";
                    var value = ConfigurationManager.AppSettings[key];

                    _isTracked = bool.Parse(value);
                }

                return _isTracked.Value;
            }
            set { _isTracked = value; }
        }

        public string NamespaceInfrastructure
        {
            get
            {
                if (string.IsNullOrEmpty(_namespaceInfrastructure))
                {
                    var key = "Bedrock.Infrastructure.Namespace";
                    _namespaceInfrastructure = string.Format(ConfigurationManager.AppSettings[key], Domain.ToString());
                }

                return _namespaceInfrastructure;
            }
            set { _namespaceInfrastructure = value; }
        }
        #endregion

        #region Public Methods
        public static BuildSettings Create()
        {
            return new BuildSettings();
        }

        public static BuildSettings Create(eDomain domain)
        {
            return new BuildSettings
            {
                Domain = domain
            };
        }

        public static BuildSettings Create(eDomain domain, eEntityType entityType)
        {
            return new BuildSettings
            {
                Domain = domain,
                EntityType = entityType
            };
        }

        public void Verify()
        {
            VerifyConnectionString();
            VerifyDomainPath();
            VerifyNamespaceEntity();
            VerifyNamespaceShared();
        }

        public string GetBuilderPath(eBuilder builder)
        {
            switch (builder)
            {
                case eBuilder.Entity:
                    return EntityPath;
                case eBuilder.EntityPartial:
                    return EntityPathPartial;
                case eBuilder.Mapping:
                    return MappingPath;
                case eBuilder.Context:
                    return ContextPath;
                case eBuilder.Service:
                    return ServicePath;
                case eBuilder.ServiceInterface:
                    return ServiceInterfacePath;
                case eBuilder.Enumeration:
                    return EnumerationPath;
                case eBuilder.AppContext:
                    return AppContextPath;
                case eBuilder.Contract:
                    return ContractPath;
                case eBuilder.AppService:
                    return AppServicePath;
                case eBuilder.AppServiceInterface:
                    return AppServiceInterfacePath;
                case eBuilder.ControllerApi:
                    return ControllerApiPath;
                case eBuilder.ControllerMvc:
                    return ControllerMvcPath;
                case eBuilder.AutoMapperProfile:
                    return AutoMapperProfilePath;
                default:
                    throw new NotSupportedException(string.Concat("Unsupported builder:  ", builder.ToString()));
            }
        }
        #endregion

        #region Public Methods (Verify)
        public void VerifyConnectionString()
        {
            try
            {
                new SqlConnectionStringBuilder(ConnectionString);
            }
            catch (Exception ex)
            {
                var newLine = Environment.NewLine;
                throw new ArgumentException(string.Concat("Connection string invalid.", newLine, newLine, ex.Message));
            }
        }

        public void VerifyDomainPath()
        {
            try
            {
                if (!DomainPath.EndsWith("\\"))
                    DomainPath += "\\";

                Path.GetFullPath(DomainPath);
            }
            catch (Exception ex)
            {
                var newLine = Environment.NewLine;
                throw new ArgumentException(string.Concat("Domain Path invalid.", newLine, newLine, ex.Message));
            }
        }

        public void VerifyNamespaceEntity()
        {
            if (!GetNamespaceExpression().IsMatch(NamespaceEntity))
                throw new ArgumentException("Entity Namespace invalid.");
        }

        public void VerifyNamespaceShared()
        {
            if (!GetNamespaceExpression().IsMatch(NamespaceShared))
                throw new ArgumentException("Shared Namespace invalid.");
        }
        #endregion

        #region Private Methods
        private void Reset()
        {
            _connectionString = null;
            _domainPath = null;
            _namespaceEntity = null;
            _namespaceContext = null;
            _namespaceMapping = null;
            _namespaceService = null;
            _namespaceEnumeration = null;
            _namespaceContract = null;
            _namespaceAppService = null;
            _namespaceControllerApi = null;
            _namespaceControllerMvc = null;
            _entityExclusions = null;
            _propertyInclusions = null;
        }

        private Regex GetNamespaceExpression()
        {
            var pattern = @"(@?[a-z_A-Z]\w+(?:\.@?[a-z_A-Z]\w+)*)$";
            return new Regex(pattern, RegexOptions.IgnoreCase);
        }
        #endregion
    }
}
