using System.Windows;

namespace DeclarableDataGrid
{
    public sealed class ColumnTemplateContainer
    {
        public Style CellStyle { get; set; }
        public Style HeaderStyle { get; set; }
        public DataTemplate CellTemplate { get; set; }
        public DataTemplate HeaderTemplate { get; set; }
    }
}