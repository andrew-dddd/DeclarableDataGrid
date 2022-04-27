using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DeclarableDataGrid
{
    public sealed class ColumnHeaderBuilder
    {
        private PropertyInfo _propertyInfo;

        public ColumnHeaderBuilder(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public string ColumnName { get; private set; }

        public Type ColumnType { get; private set; }

        public DisplayNameAttribute DisplayNameAttribute { get; private set; }

        public int DisplayIndex { get; private set; }

        public ColumnHeaderBuilder WithDisplayName(string displayName)
        {
            DisplayNameAttribute = new DisplayNameAttribute(displayName);
            return this;
        }

        public ColumnHeaderBuilder WithDisplayIndex(int displayIndex)
        {
            DisplayIndex = displayIndex;
            return this;
        }

        public DeclarableColumnDataPropertyDescriptor BuildColumnDescriptor()
        {
            var attributes = new List<Attribute>();
            attributes.Add(DisplayNameAttribute);
            var attributesArray = attributes.Where(x => x != null).ToArray();

            return new DeclarableColumnDataPropertyDescriptor(_propertyInfo, DisplayIndex, attributesArray);
        }
    }
}