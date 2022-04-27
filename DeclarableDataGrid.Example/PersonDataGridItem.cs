using System;

namespace DeclarableDataGrid.Example
{
    public class PersonDataGridItem : DeclarableColumnDataDescriptor
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HiddenColumn { get; set; }
    }
}
