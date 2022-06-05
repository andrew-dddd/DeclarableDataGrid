using System;

namespace DeclarableDataGrid
{
    public static class ColumnTemplateConfigurationExtensions
    {
        public static ColumnTemplateConfiguration ForColumnNameUseResource(this ColumnTemplateConfiguration config, string columnName, string resourceKey)
        {
            config.ForColumnUseResource(new CustomColumnFilter(x => x.ColumnName == columnName), resourceKey);
            return config;
        }

        public static ColumnTemplateConfiguration ForColumnType(this ColumnTemplateConfiguration config, Type columnDataName, string resourceKey)
        {
            config.ForColumnUseResource(new CustomColumnFilter(x => x.ColumnDataType == columnDataName), resourceKey);
            return config;
        }

        public static ColumnTemplateConfiguration ForColumnNameAndType(this ColumnTemplateConfiguration config, string columName, Type columnDataName, string resourceKey)
        {
            config.ForColumnUseResource(new CustomColumnFilter(x => x.ColumnName == columName && x.ColumnDataType == columnDataName), resourceKey);
            return config;
        }

        public static ColumnTemplateConfiguration ForColumnNameOrType(this ColumnTemplateConfiguration config, string columName, Type columnDataName, string resourceKey)
        {
            config.ForColumnUseResource(new CustomColumnFilter(x => x.ColumnName == columName || x.ColumnDataType == columnDataName), resourceKey);
            return config;
        }
    }
}