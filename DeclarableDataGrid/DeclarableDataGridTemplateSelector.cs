using System;
using System.Windows;
using System.Windows.Controls;

namespace DeclarableDataGrid
{
    /// <summary>
    /// Template selector for the declarable DataGridColumn
    /// </summary>
    public class DeclarableDataGridTemplateSelector : DataTemplateSelector
    {
        public override sealed DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            // By default, DataGridTemplateColumn does not carry any information about the type of the column.
            // Using custom DeclarableDataGridColumn allows to precisely determine the type of the column.
            // Precise determination is needed to select appropriate template for the DataGrid column.
            // That selector is not really mandatory, ordinary DataTemplateSelecor can be used but logic in the helper would need to be updated/declared.
            if (container is ContentPresenter p && p.Parent is DataGridCell cell && cell.Column is DeclarableDataGridColumn dgtc)
            {
                return SelectTemplate(item, dgtc.ColumnDataType);
            }

            return base.SelectTemplate(item, container);
        }

        protected virtual DataTemplate SelectTemplate(object item, Type columnDataType)
        {
            return null;
        }
    }
}