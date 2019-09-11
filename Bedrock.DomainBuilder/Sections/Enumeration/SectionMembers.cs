using System;
using System.Data;
using System.Linq;

using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Sections.Enumeration
{
    public class SectionMembers<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionMembers(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            BuildMembers();
            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildMembers()
        {
            var tableName = GetTableName();

            if (tableName != null)
            {
                var enumTable = Pass.EntityHelper.GetDataTable(tableName);
                var nameField = GetNameField(enumTable);
                var valueField = GetValueField(enumTable);

                if (!string.IsNullOrEmpty(nameField) && !string.IsNullOrEmpty(valueField))
                {
                    foreach (DataRow row in enumTable.Rows)
                    {
                        var name = row[nameField];
                        var value = row[valueField];

                        Add(Tab(2), string.Format("{0} = {1},", name, value));
                    }

                    if (enumTable.Rows.Count > 0)
                        Container.Remove(Container.Length - 3, 3);
                }
            }
        }

        private string GetTableName()
        {
            var returnValue = EntityName;

            if (!Pass.EntityHelper.IsTableExist(returnValue))
                returnValue = Pluralize(returnValue);

            if (!Pass.EntityHelper.IsTableExist(returnValue))
                returnValue = null;

            return returnValue;
        }

        private string GetNameField(DataTable enumTable)
        {
            var returnValue = string.Empty;

            var names = Enum
                            .GetValues(typeof(eEnumName))
                            .Cast<eEnumName>();

            names.Each(n =>
            {
                var name = n.ToString();

                if (enumTable.Columns.Contains(name))
                {
                    returnValue = name;
                    return false;
                }

                return true;
            });

            return returnValue;
        }

        private string GetValueField(DataTable enumTable)
        {
            var returnValue = string.Empty;
            var properties = GetPropertiesForComparisonNoCommonKey(EntityName);
            var keyColumn = string.Empty;

            if (properties.Any())
                keyColumn = properties.First();

            if (keyColumn != null)
            {
                var hasKeyColumn = enumTable.Columns.Contains(keyColumn);

                if (hasKeyColumn)
                    returnValue = keyColumn;
            }
            else
            {
                var values = Enum
                                .GetValues(typeof(eEnumValue))
                                .Cast<eEnumValue>();

                values.Each(v =>
                {
                    var value = v.ToString();

                    if (enumTable.Columns.Contains(value))
                    {
                        returnValue = value;
                        return false;
                    }

                    return true;
                });
            }

            return returnValue;
        }
        #endregion
    }
}
