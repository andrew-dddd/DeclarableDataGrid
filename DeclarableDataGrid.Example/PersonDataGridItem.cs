using DeclarableDataGrid.PropertyDescriptors;
using System;

namespace DeclarableDataGrid.Example
{
    public class PersonDataGridItem : DeclarableColumnDataDescriptor
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public PersonData PersonData { get; set; }
        public string HiddenColumn { get; set; }
    }

    public class PersonData
    {
        public string ExampleData1 { get; set; }
        public string ExampleData2 { get; set; }
    }
}
