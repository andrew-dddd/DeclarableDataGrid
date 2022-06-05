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
            DataContext = new MainWindowViewModel();
            
            ViewModel.ExampleCollection.ConfigureColumnTemplates(Resources)                
                .ForColumnNameUseResource("DynamicColumn1", "ColumnTemplate")
                .ForColumnNameUseResource("PersonData", "PersonDataColumnTemplate");
        }

        private MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;

        private void ExampleDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            ViewModel.ExampleCollection.CreateDeclarableDataGrid(e);

            // OR
            // DeclarableDataGridBuilder.DefaultCreateDeclarableDataGrid(e);
        }
    }
}
