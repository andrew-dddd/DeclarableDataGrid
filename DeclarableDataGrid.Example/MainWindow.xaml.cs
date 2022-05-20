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
        private readonly DeclarableDataGridBuilder _declarableDataGridBuilder;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            _declarableDataGridBuilder = new DeclarableDataGridBuilder();
            _declarableDataGridBuilder.ConfigureColumnTemplates(Resources)
                .ForColumnUseTemplate("DynamicColumn1", "ColumnTemplate")
                .ForColumnUseTemplate("PersonData", "PersonDataColumnTemplate");
        }

        private void ExampleDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            _declarableDataGridBuilder.CreateDeclarableDataGrid(e);
        }
    }
}
