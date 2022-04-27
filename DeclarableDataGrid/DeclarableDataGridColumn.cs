using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DeclarableDataGrid
{
    /// <summary>
    /// Column for the declarable DataGrid
    /// </summary>
    public class DeclarableDataGridColumn : DataGridTemplateColumn
    {
        /// <summary>
        /// Property binding for the DataGrid column
        /// </summary>
        public BindingBase ColumnDataBinding { get; set; }

        /// <summary>
        /// Type of the property bound to the column
        /// </summary>
        public Type ColumnDataType { get; set; }

        /// <summary>
        /// Name of the column given at declaration time.
        /// </summary>
        public string ColumnName { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            // Binding stored while creating column is used here. 
            // When the cell has the datacontext set and the cell template content does not, cell template derives the datacontext from the cell. 
            // By setting binding to the cell, cell template can access the cell datacontext and the template bindings can work as usual. 
            // After that, the cell resolves its value via GetColumnValue method of the DeclarableColumnDataDescriptor
            cell.SetBinding(FrameworkElement.DataContextProperty, ColumnDataBinding);
            return base.GenerateElement(cell, dataItem);
        }
    }
}