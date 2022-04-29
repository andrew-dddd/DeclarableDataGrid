using DeclarableDataGrid.PropertyDescriptors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DeclarableDataGrid.ColumnBuilders
{
    public sealed class DynamicColumnBuilder
    {
        public DynamicColumnBuilder(Type componentType, Type propertyType, string name)
        {
            ColumnName = name;
            ColumnType = propertyType;
            ColumnDeclaringType = componentType;
        }

        public string ColumnName { get; private set; }

        public Type ColumnType { get; private set; }

        public Type ColumnDeclaringType { get; private set; }

        public DisplayNameAttribute DisplayNameAttribute { get; private set; }

        public int DisplayIndex { get; private set; }

        public DynamicColumnBuilder WithDisplayName(string displayName)
        {
            DisplayNameAttribute = new DisplayNameAttribute(displayName);
            return this;
        }

        public DynamicColumnBuilder WithDisplayIndex(int displayIndex)
        {
            DisplayIndex = displayIndex;
            return this;
        }

        public DynamicColumnPropertyDescriptor BuildColumnDescriptor()
        {
            var attributes = new List<Attribute>();
            attributes.Add(DisplayNameAttribute);
            var attributesArray = attributes.Where(x => x != null).ToArray();

            return new DynamicColumnPropertyDescriptor(ColumnDeclaringType, ColumnType, DisplayIndex, ColumnName, attributesArray);
        }
    }
}