using DeclarableDataGrid.PropertyDescriptors;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DeclarableDataGrid
{
    public class DeclarableDataGridBuilder
    {
        private ColumnTemplateConfiguration _columnTemplateConfiguration;

        public DeclarableDataGridBuilder()
        {
        }

        public ColumnTemplateConfiguration ConfigureColumnTemplates(ResourceDictionary resourceDictionary)
        {
            _columnTemplateConfiguration = new ColumnTemplateConfiguration(resourceDictionary);
            return _columnTemplateConfiguration;
        }

        /// <summary>
        /// Default logic for 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="columnConfigurationAction"></param>
        public void CreateDeclarableDataGrid(DataGridAutoGeneratingColumnEventArgs e, Action<DeclarableDataGridColumn> columnConfigurationAction = null)
        {
            // By default, DataGrid creates text column for complex types to display ToString() value of the object.
            // Fot other simple types, there are other templates, like checkbox for the bool
            // Common part for these automatic columns is that they are derived from DataGridBoundColumn, and they contain direct binding for the property.
            // By default, DataGridTemplateColumn does not have anything related to the binding, so that is why custom type is needed.
            // In the custom DeclarableDataGridColumn type, binding is stored for later use.
            DeclarableDataGridColumn dgdtc = new DeclarableDataGridColumn();

            if (e.Column is DataGridBoundColumn dataGridBoundColumn)
            {
                dgdtc.ColumnDataBinding = dataGridBoundColumn.Binding;
            }

            // Storing type of the column for the later use in the cell template selector.
            dgdtc.ColumnDataType = e.PropertyType;
            dgdtc.CellTemplateSelector = new DataTemplateSelector();

            if (e.PropertyDescriptor is DeclarableDataGridPropertyDescriptor descriptor)
            {
                dgdtc.ColumnName = descriptor.Name;
                dgdtc.Header = descriptor.DisplayName;
                dgdtc.DisplayIndex = descriptor.DisplayIndex;
                dgdtc.IsReadOnly = descriptor.IsReadOnly;
            }
            else
            {
                dgdtc.Header = e.Column.Header;
            }

            if (_columnTemplateConfiguration != null && _columnTemplateConfiguration.TryGetTemplateContainerByColumnName(dgdtc, out var columnTemplateContainer))
            {
                dgdtc.UseTemplateContainer(columnTemplateContainer);
            }

            columnConfigurationAction?.Invoke(dgdtc);

            // Replacing automatically created column with the column created here
            // No cancelling needed - DataGrid will just swallow whatever is in the event arg.
            e.Column = dgdtc;
        }

        public static void DefaultCreateDeclarableDataGrid(DataGridAutoGeneratingColumnEventArgs e, Action<DeclarableDataGridColumn> columnConfigurationAction = null)
        {
            DeclarableDataGridBuilder builder = new DeclarableDataGridBuilder();
            builder.CreateDeclarableDataGrid(e, columnConfigurationAction);
        }
    }
}