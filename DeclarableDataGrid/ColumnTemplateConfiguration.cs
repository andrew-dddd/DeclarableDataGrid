using System;
using System.Collections.Generic;
using System.Windows;

namespace DeclarableDataGrid
{
    public class ColumnTemplateConfiguration
    {
        private readonly ResourceDictionary _resourceDictionary;
        private readonly Dictionary<string, string> _columnResourceMapping = new Dictionary<string, string>();

        internal ColumnTemplateConfiguration(ResourceDictionary resourceDictionary)
        {
            _resourceDictionary = resourceDictionary;
        }

        /// <summary>
        /// Creates binding between resource key and column name.
        /// </summary>
        /// <param name="columnName">Name of the column</param>
        /// <param name="resourceKey">Resource key to use for the column. Resource must be of type <see cref="ColumnTemplateContainer"/></param>
        public ColumnTemplateConfiguration ForColumnUseTemplate(string columnName, string resourceKey)
        {
            if (!_resourceDictionary.Contains(resourceKey) && _resourceDictionary[resourceKey] is ColumnTemplateContainer)
            {
                throw new InvalidOperationException($"Unable to find column template container with key: {resourceKey}");
            }

            if (_resourceDictionary[resourceKey] is ColumnTemplateContainer)
            {
                _columnResourceMapping.Add(columnName, resourceKey);
            }
            else
            {
                throw new InvalidOperationException($"Expected resource {resourceKey} to be of type {typeof(ColumnTemplateContainer)}");
            }

            return this;
        }

        internal bool TryGetTemplateContainerByColumnName(string columnName, out ColumnTemplateContainer columnTemplateContainer)
        {
            columnTemplateContainer = null;
            if (_columnResourceMapping.TryGetValue(columnName, out var resourceKey))
            {
                columnTemplateContainer = _resourceDictionary[resourceKey] as ColumnTemplateContainer;
                return true;
            }

            return false;
        }
    }
}