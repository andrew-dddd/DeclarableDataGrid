using System;
using System.Windows.Controls;

namespace DeclarableDataGrid
{
    public static class DeclarableDataGridHelper
    {
        /// <summary>
        /// Default logic for declarable column creation.
        /// </summary>
        /// <param name="templateSelector">Column template selector</param>
        /// <param name="e">Column creation event arg</param>
        public static void DefaultColumnCreatedAction(DataTemplateSelector templateSelector, DataGridAutoGeneratingColumnEventArgs e)
        {
            ColumnGeneratingAction(e, dgdtc =>
            {
                dgdtc.CellTemplateSelector = templateSelector;
            });
        }

        /// <summary>
        /// Default logic using DeclarableDataGridTemplateSelector selector.
        /// </summary>
        /// <param name="templateSelector"></param>
        /// <param name="e"></param>
        public static void DefaultColumnGeneratingAction(DeclarableDataGridTemplateSelector templateSelector, DataGridAutoGeneratingColumnEventArgs e)
        {
            ColumnGeneratingAction(e, dgdtc =>
            {
                dgdtc.CellTemplateSelector = templateSelector;
            });
        }

        private static void ColumnGeneratingAction(DataGridAutoGeneratingColumnEventArgs e, Action<DeclarableDataGridColumn> columnConfigurationAction = null)
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
            dgdtc.IsReadOnly = true;
            dgdtc.Header = e.Column.Header;

            columnConfigurationAction?.Invoke(dgdtc);

            if (e.PropertyDescriptor is DeclarableDataGridPropertyDescriptor descriptor)
            {
                dgdtc.ColumnName = descriptor.Name;
                dgdtc.Header = descriptor.GetDisplayName();
                dgdtc.DisplayIndex = descriptor.DisplayIndex;
            }

            // Replacing automatically created column with the column created here
            // No cancelling needed - DataGrid will just swallow whatever is in the event arg.
            e.Column = dgdtc;
        }
    }
}