using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Design;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.Metadata.Edm;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Bedrock.DomainBuilder.EntityFramework
{
    public class EntityHelper
    {
        #region Fields
        private static Dictionary<string, EntityStoreSchemaFilterEntry> _storeMetadataFilters = new Dictionary<string, EntityStoreSchemaFilterEntry>
        {
            { "EdmMetadata", new EntityStoreSchemaFilterEntry(null, null, "EdmMetadata", EntityStoreSchemaFilterObjectTypes.Table, EntityStoreSchemaFilterEffect.Exclude) },
            { "__MigrationHistory", new EntityStoreSchemaFilterEntry(null, null, "__MigrationHistory", EntityStoreSchemaFilterObjectTypes.Table, EntityStoreSchemaFilterEffect.Exclude) }
        };

        public static readonly string STORE_GENERATED_PATTERN = "StoreGeneratedPattern";
        public static readonly string IDENTITY = "Identity";
        #endregion

        #region Constructors
        private EntityHelper(BuildSettings settings)
        {
            Settings = settings;
            Initialize();
        }
        #endregion

        #region Properties
        public IEnumerable<EntityType> EntityTypes { get; set; }

        public EdmMapping Mappings { get; set; }

        private BuildSettings Settings { get; set; }
        #endregion

        #region Public Methods
        public static EntityHelper Create(BuildSettings settings)
        {
            var returnValue = new EntityHelper(settings);
            var schema = returnValue.LoadStoreSchema();
            var mappings = returnValue.GenerateDefaultMappings(schema);

            returnValue.HydrateTypeInfo(schema, mappings);

            return returnValue;
        }

        public DataTable GetDataTable(string tableName)
        {
            var returnValue = new DataTable(tableName);
            var commandText = string.Format("SELECT * FROM {0}", tableName);

            using (var connection = new SqlConnection(Settings.ConnectionString))
            using (var adaptor = new SqlDataAdapter(commandText, connection))
            {
                adaptor.Fill(returnValue);
            }

            return returnValue;
        }

        public bool IsTableExist(string tableName)
        {
            var returnValue = false;
            var commandText = string.Format("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}'", Settings.StoreSchemaNamespace, tableName);

            using (var connection = new SqlConnection(Settings.ConnectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                connection.Open();
                returnValue = command.ExecuteReader().HasRows;
            }

            return returnValue;
        }

        public static List<string> GetStoreSchemas(BuildSettings settings)
        {
            var returnValue = new List<string>();
            var commandText = string.Format("SELECT DISTINCT(TABLE_SCHEMA) FROM INFORMATION_SCHEMA.TABLES;");

            using (var connection = new SqlConnection(settings.ConnectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        returnValue.Add(reader.GetString(0));
                }
            }

            return returnValue;
        }
        #endregion

        #region Private Methods
        private EntityStoreSchemaGenerator LoadStoreSchema()
        {
            var returnValue = new EntityStoreSchemaGenerator(Settings.ProviderInvariantName, Settings.ConnectionString, Settings.StoreSchemaNamespace);

            returnValue.GenerateForeignKeyProperties = true;
            returnValue.GenerateStoreMetadata(_storeMetadataFilters.Values);

            return returnValue;
        }

        private EntityModelSchemaGenerator GenerateDefaultMappings(EntityStoreSchemaGenerator storeGenerator)
        {
            var contextName = string.Concat(Settings.Domain, "Context");
            var returnValue = new EntityModelSchemaGenerator(storeGenerator.EntityContainer, "DefaultNamespace", contextName);

            returnValue.PluralizationService = PluralizationService.CreateService(new CultureInfo("en"));
            returnValue.GenerateForeignKeyProperties = true;
            returnValue.GenerateMetadata();

            return returnValue;
        }

        private void HydrateTypeInfo(EntityStoreSchemaGenerator schema, EntityModelSchemaGenerator mappings)
        {
            EntityTypes = mappings.EdmItemCollection.OfType<EntityType>().ToArray();
            Mappings = new EdmMapping(mappings, schema.StoreItemCollection);
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            SetStoreMetadataFilters();
        }

        private void SetStoreMetadataFilters()
        {
            GetStoreSchemas(Settings).Each(s =>
            {
                var effect = s == Settings.StoreSchemaNamespace ? EntityStoreSchemaFilterEffect.Allow : EntityStoreSchemaFilterEffect.Exclude;
                _storeMetadataFilters[s] = new EntityStoreSchemaFilterEntry(null, s, null, EntityStoreSchemaFilterObjectTypes.Table, effect);
            });
        }
        #endregion
    }
}
