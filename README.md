[![Build Status](https://app.travis-ci.com/andrew-dddd/DeclarableDataGrid.svg?branch=main)](https://app.travis-ci.com/andrew-dddd/DeclarableDataGrid)

# DeclarableDataGrid

DeclarableDataGrid will help you contol Windows Presentation Foundation DataGrid columns. It allows you to declare columns statically, or add them while running WPF application. 

# Features

- Declare which properties of the object need to be added as DataGrid column
- Add other columns during runtime
- Bindings are supported

# Planned features
- Allow to edit cells of the DataGrid added via DeclarableData grid. 

# Usage
- Set DataGrid to automatically generate columns and bind the data source. Data source must be of type `DeclarableDataGridObservableCollection<T>` where T is the type you want as a row in DataGrid. `T` must inherit from the `DeclarableDataGrid.PropertyDescriptors.DeclarableColumnDataDescriptor`
```
<DataGrid Name="ExampleDataGrid" ItemsSource="{Binding ExampleCollection}" AutoGenerateColumns="True" AutoGeneratingColumn="ExampleDataGrid_AutoGeneratingColumn"></DataGrid>
```
- In the event handler of the `AutoGeneratingColumn` event include the helper function from the DeclarableDataGrid
```
    private void ExampleDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        DeclarableColumnGenerationHelper.OnDeclarableColumnGenerated(e);
    }
```

- Select properties which should be used by DataGrid in a fluent manner:

```
    ExampleCollection.UsePropertyAsColumn(x => x.PersonId, x => x.WithDisplayName("Id"));
    ExampleCollection.UsePropertyAsColumn(x => x.Name, x => x.WithDisplayName("Name"));
    ExampleCollection.UsePropertyAsColumn(x => x.LastName, x => x.WithDisplayName("Last Name"));
    ExampleCollection.UsePropertyAsColumn(x => x.BirthDate, x => x.WithDisplayName("Birth Date"));
````

- Or add DataGrid properties later in a loop for example: 
```
    ExampleCollection.UseDynamicColumn<int>("DynamicColumn1", x => x.WithDisplayName("Dynamic column 1"));
    ExampleCollection.UseDynamicColumn<double>("DynamicColumn2", x => x.WithDisplayName("Dynamic column 2"));
    ExampleCollection.UseDynamicColumn<string>("DynamicColumn3", x => x.WithDisplayName("Dynamic column 3"));
```

- Dynamic columns can be set using indexer operator on the object (type of the set property must match the declared type):
```
    ExampleCollection.Add(new PersonDataGridItem
    {
        PersonId = 1,
        BirthDate = new DateTime(1990, 1, 1),
        Name = "John",
        LastName = "Smith",
        HiddenColumn = "SSN",
        ["DynamicColumn1"] = 1,
    });
```