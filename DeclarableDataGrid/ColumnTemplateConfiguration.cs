using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DeclarableDataGrid
{
    public class ColumnTemplateConfiguration
    {
        private readonly ResourceDictionary _resourceDictionary;
        private readonly List<KeyValuePair<IColumnFilter, string>> _columnResourceMapping = new List<KeyValuePair<IColumnFilter, string>>();

        internal ColumnTemplateConfiguration(ResourceDictionary resourceDictionary)
        {
            _resourceDictionary = resourceDictionary;
        }

        /// <summary>
        /// Creates binding between resource key and column name.
        /// </summary>
        /// <param name="columnFilter">Column filter</param>
        /// <param name="resourceKey">Resource key to use for the column. Resource must be of type <see cref="ColumnTemplateContainer"/></param>
        public ColumnTemplateConfiguration ForColumnUseResource(IColumnFilter columnFilter, string resourceKey)
        {
            if (!_resourceDictionary.Contains(resourceKey) && _resourceDictionary[resourceKey] is ColumnTemplateContainer)
            {
                throw new InvalidOperationException($"Unable to find column template container with key: {resourceKey}");
            }

            if (_resourceDictionary[resourceKey] is ColumnTemplateContainer)
            {
                _columnResourceMapping.Add(new KeyValuePair<IColumnFilter, string>(columnFilter, resourceKey));
            }
            else
            {
                throw new InvalidOperationException($"Expected resource {resourceKey} to be of type {typeof(ColumnTemplateContainer)}");
            }

            return this;
        }

        internal bool TryGetTemplateContainerByColumnName(DeclarableDataGridColumn declarableDataGridColumn, out ColumnTemplateContainer columnTemplateContainer)
        {
            columnTemplateContainer = null;
            var resourceKey = _columnResourceMapping.FirstOrDefault(x => x.Key.MatchColumn(declarableDataGridColumn)).Value;

            if (resourceKey != null && _resourceDictionary.Contains(resourceKey))
            {
                columnTemplateContainer = _resourceDictionary[resourceKey] as ColumnTemplateContainer;
                return true;
            }

            return false;
        }
    }
}