using DeclarableDataGrid.ColumnBuilders;
using DeclarableDataGrid.PropertyDescriptors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DeclarableDataGrid
{
    public class DeclarableDataGridObservableCollection<T> : ObservableCollection<T>, ITypedList
        where T : DeclarableColumnDataDescriptor
    {
        private PropertyDescriptorCollection _propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
        private HashSet<string> _columnHeaderNames = new HashSet<string>();

        public void UsePropertyAsColumn<TProperty>(Expression<Func<T, TProperty>> expression, Action<PropertyColumnBuilder> builder = null)
        {
            var columnHeaderBuilder = PropertyColumnBuilder.CreateFromExpression(expression)
                .WithDisplayIndex(_propertyDescriptorCollection.Count);

            builder?.Invoke(columnHeaderBuilder);
            AddColumnDescriptor(columnHeaderBuilder.BuildColumnDescriptor());
        }

        public void UseDynamicColumn<TColumn>(string columnName, Action<DynamicColumnBuilder> builder = null)
        {
            var columnHeaderBuilder = new DynamicColumnBuilder(typeof(T), typeof(TColumn), columnName)
                .WithDisplayIndex(_propertyDescriptorCollection.Count);

            builder?.Invoke(columnHeaderBuilder);
            AddColumnDescriptor(columnHeaderBuilder.BuildColumnDescriptor());
        }

        public string GetListName(PropertyDescriptor[] listAccessors) => string.Empty;

        /// <summary>
        /// Gets the headers for data grid.
        /// </summary>
        /// <param name="listAccessors"></param>
        /// <returns>Collection of grid headers</returns>
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) => _propertyDescriptorCollection;

        protected override void ClearItems()
        {
            _propertyDescriptorCollection.Clear();
            _columnHeaderNames.Clear();
            base.ClearItems();
        }

        protected override void InsertItem(int index, T item)
        {
            item.SetProperties(_propertyDescriptorCollection);
            base.InsertItem(index, item);
        }

        private void AddColumnDescriptor(DeclarableDataGridPropertyDescriptor columnDescriptor)
        {
            if (_columnHeaderNames.Contains(columnDescriptor.Name))
            {
                throw new InvalidOperationException($"Column {columnDescriptor.Name} has already been registered.");
            }

            _columnHeaderNames.Add(columnDescriptor.Name);
            _propertyDescriptorCollection.Add(columnDescriptor);
        }
    }
}