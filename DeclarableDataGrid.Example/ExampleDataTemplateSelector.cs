using System;
using System.Windows;

namespace DeclarableDataGrid.Example
{
    public class ExampleDataTemplateSelector : DeclarableDataGridTemplateSelector
    {
        public DataTemplate Template1 { get; set; }
        public DataTemplate Template2 { get; set; }

        protected override DataTemplate SelectTemplate(object item, Type columnDataType)
        {
            return base.SelectTemplate(item, columnDataType);
        }
    }
}
