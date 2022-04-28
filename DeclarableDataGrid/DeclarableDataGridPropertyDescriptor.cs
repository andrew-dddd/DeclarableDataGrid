using System;
using System.ComponentModel;

namespace DeclarableDataGrid
{
    public abstract class DeclarableDataGridPropertyDescriptor : PropertyDescriptor
    {
        public DeclarableDataGridPropertyDescriptor(int displayIndex, string name, Attribute[] attributes) : base(name, attributes)
        {
            DisplayIndex = displayIndex;
        }

        public int DisplayIndex { get; private set; }

        public string GetDisplayName()
        {
            return string.IsNullOrEmpty(DisplayName)
                    ? Name
                    : DisplayName;
        }
    }
}