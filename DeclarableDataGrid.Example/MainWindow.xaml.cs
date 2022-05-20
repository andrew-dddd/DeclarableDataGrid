using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DeclarableDataGrid.DeclarableDataGridTemplateSelector;

namespace DeclarableDataGrid.Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TemplateSelector = new ExampleDataTemplateSelector
            {
                Template1 = this.Resources["Template1"] as DataTemplate,
                Template2 = this.Resources["Template2"] as DataTemplate,
            };

            DataContext = new MainWindowViewModel();
        }

        public ExampleDataTemplateSelector TemplateSelector { get; }

        private void ExampleDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DeclarableDataGridHelper.CreateDeclarableDataGrid(e, column =>
            {
                //if (column.ColumnName == "DynamicColumn1")
                //{
                //    column.UseTemplateContainer(this.Resources["ColumnTemplate"] as ColumnTemplateContainer);
                //}
            });
        }
    }
}
