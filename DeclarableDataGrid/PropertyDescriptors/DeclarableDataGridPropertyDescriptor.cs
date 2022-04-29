using System;
using System.ComponentModel;

namespace DeclarableDataGrid.PropertyDescriptors
{
    public abstract class DeclarableDataGridPropertyDescriptor : PropertyDescriptor
    {
        public DeclarableDataGridPropertyDescriptor(int displayIndex, string name, Attribute[] attributes) : base(name, attributes)
        {
            DisplayIndex = displayIndex;
        }

        public int DisplayIndex { get; private set; }
    }
}