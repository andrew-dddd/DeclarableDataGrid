using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace DeclarableDataGrid
{
    public class DeclarableDataGridObservableCollection<T> : ObservableCollection<T>, ITypedList
        where T : DeclarableColumnDataDescriptor
    {
        private PropertyDescriptorCollection _propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
        private HashSet<string> _columnHeaderNames = new HashSet<string>();

        public void UsePropertyAsColumn<TProperty>(Expression<Func<T, TProperty>> expression, Action<ColumnHeaderBuilder> builder = null)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                var propertyInfo = (PropertyInfo)memberExpression.Member;

                if (_columnHeaderNames.Contains(propertyInfo.Name))
                {
                    throw new InvalidOperationException($"Column {propertyInfo.Name} has already been registered.");
                }

                var columnHeaderBuilder = new ColumnHeaderBuilder(propertyInfo)
                    .WithDisplayIndex(_propertyDescriptorCollection.Count);

                builder?.Invoke(columnHeaderBuilder);
                AddColumnDescriptor(columnHeaderBuilder.BuildColumnDescriptor());
                return;
            }

            throw new InvalidOperationException("Expected property but got: ");
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

        private void AddColumnDescriptor(DeclarableColumnDataPropertyDescriptor columnDescriptor)
        {
            _columnHeaderNames.Add(columnDescriptor.Name);
            _propertyDescriptorCollection.Add(columnDescriptor);
        }
    }
}