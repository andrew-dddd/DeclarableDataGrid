using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DeclarableDataGrid
{
    /// <summary>
    /// Describes columns of the row of the DataGrid
    /// </summary>
    public abstract class DeclarableColumnDataDescriptor : ICustomTypeDescriptor
    {
        private PropertyDescriptorCollection _propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
        private Dictionary<string, object> _dynamicPropertiesValues = new Dictionary<string, object>();

        public AttributeCollection GetAttributes() => null;

        public string GetClassName() => GetType().Name;

        public string GetComponentName() => "";

        public TypeConverter GetConverter() => null;

        public EventDescriptor GetDefaultEvent() => null;

        public PropertyDescriptor GetDefaultProperty() => null;

        public object GetEditor(Type editorBaseType) => null;

        public EventDescriptorCollection GetEvents() => null;

        public EventDescriptorCollection GetEvents(Attribute[] attributes) => null;

        /// <summary>
        /// Gets the DataGrid headers.
        /// </summary>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties() => _propertyDescriptorCollection;

        /// <summary>
        /// Sets headers available for the DataGrid
        /// </summary>
        /// <param name="propertyDescriptorCollection"></param>
        public void SetProperties(PropertyDescriptorCollection propertyDescriptorCollection)
        {
            _propertyDescriptorCollection = propertyDescriptorCollection;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => _propertyDescriptorCollection;

        public object GetPropertyOwner(PropertyDescriptor pd) => null;

        public object GetDynamicPropertyValue(string name)
        {
            return _dynamicPropertiesValues.TryGetValue(name, out var value)
                ? value
                : null;
        }

        public void SetDynamicPropertyValue(string name, object value)
        {
            if (_dynamicPropertiesValues.ContainsKey(name))
            {
                _dynamicPropertiesValues[name] = value;
            }
            else
            {
                _dynamicPropertiesValues.Add(name, value);
            }
        }

        public object this[string name]
        {
            get { return GetDynamicPropertyValue(name); }
            set 
            {
                SetDynamicPropertyValue(name, value);
            }
        }
    }
}