using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeclarableDataGrid.Example
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DeclarableDataGridObservableCollection<PersonDataGridItem> _exampleCollection;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public MainWindowViewModel()
        {
            ExampleCollection = new DeclarableDataGridObservableCollection<PersonDataGridItem>();

            ExampleCollection.UsePropertyAsColumn(x => x.PersonId, x => x.WithDisplayName("Id"));
            ExampleCollection.UsePropertyAsColumn(x => x.Name, x => x.WithDisplayName("Name"));
            ExampleCollection.UsePropertyAsColumn(x => x.LastName, x => x.WithDisplayName("Last Name"));
            ExampleCollection.UsePropertyAsColumn(x => x.BirthDate, x => x.WithDisplayName("Birth Date"));

            ExampleCollection.UseDynamicColumn<int>("DynamicColumn1", x => x.WithDisplayName("Dynamic column 1"));
            ExampleCollection.UseDynamicColumn<double>("DynamicColumn2", x => x.WithDisplayName("Dynamic column 2"));
            ExampleCollection.UseDynamicColumn<string>("DynamicColumn3", x => x.WithDisplayName("Dynamic column 3"));

            ExampleCollection.Add(new PersonDataGridItem
            {
                PersonId = 1,
                BirthDate = new DateTime(1990, 1, 1),
                Name = "John",
                LastName = "Smith",
                HiddenColumn = "SSN",
                ["DynamicColumn1"] = 1,
            });

            ExampleCollection.Add(new PersonDataGridItem
            {
                PersonId = 2,
                BirthDate = new DateTime(1989, 1, 1),
                Name = "Jane",
                LastName = "Smith",
                HiddenColumn = "SSN",
                ["DynamicColumn2"] = 25.0
            });

            ExampleCollection.Add(new PersonDataGridItem
            {
                PersonId = 3,
                BirthDate = new DateTime(1988, 1, 1),
                Name = "Joe",
                LastName = "Smith",
                HiddenColumn = "SSN",
                ["DynamicColumn3"] = "value"
            });
        }

        public DeclarableDataGridObservableCollection<PersonDataGridItem> ExampleCollection
        {
            get => _exampleCollection;
            set
            {
                _exampleCollection = value;
                RaisePropertyChanged();
            }
        }
    }
}
