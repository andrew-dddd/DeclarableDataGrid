using DeclarableDataGrid.PropertyDescriptors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DeclarableDataGrid.ColumnBuilders
{
    public sealed class PropertyColumnBuilder
    {
        private PropertyInfo _propertyInfo;

        public PropertyColumnBuilder(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public string ColumnName { get; private set; }

        public Type ColumnType { get; private set; }

        public DisplayNameAttribute DisplayNameAttribute { get; private set; }

        public int DisplayIndex { get; private set; }

        public PropertyColumnBuilder WithDisplayName(string displayName)
        {
            DisplayNameAttribute = new DisplayNameAttribute(displayName);
            return this;
        }

        public PropertyColumnBuilder WithDisplayIndex(int displayIndex)
        {
            DisplayIndex = displayIndex;
            return this;
        }

        public DeclarableDataGridPropertyDescriptor BuildColumnDescriptor()
        {
            var attributes = new List<Attribute>();
            attributes.Add(DisplayNameAttribute);
            var attributesArray = attributes.Where(x => x != null).ToArray();

            return new PropertyColumnDataDescriptor(_propertyInfo, DisplayIndex, attributesArray);
        }

        internal static PropertyColumnBuilder CreateFromExpression<TType, TProperty>(Expression<Func<TType, TProperty>> expression)
            where TType : DeclarableColumnDataDescriptor
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                var propertyInfo = (PropertyInfo)memberExpression.Member;

                var columnHeaderBuilder = new PropertyColumnBuilder(propertyInfo);
                return columnHeaderBuilder;
            }

            throw new InvalidOperationException("Expected property");
        }
    }
}