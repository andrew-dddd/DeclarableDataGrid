using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DeclarableDataGrid.PropertyDescriptors
{
    /// <summary>
    /// Data grid column descriptor
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
            // When property has been set before setting property descriptors, types of the set values must be checked.

            _propertyDescriptorCollection = propertyDescriptorCollection;
            foreach (var propertyDescriptor in _propertyDescriptorCollection.OfType<DynamicColumnPropertyDescriptor>())
            {
                if (_dynamicPropertiesValues.TryGetValue(propertyDescriptor.Name, out var value) && propertyDescriptor.PropertyType.IsAssignableFrom(value.GetType())) continue;

                if (value == null) continue;

                // If the type of set value does not match the configured type, or the type of value does not inherit from the configured type, throw exception.                
                throw new InvalidOperationException($"Expected property {propertyDescriptor.Name} to be of type {propertyDescriptor.PropertyType} but instead property is of type {value.GetType()}");
            }
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
            get
            {
                return GetDynamicPropertyValue(name);
            }
            set
            {
                SetDynamicPropertyValue(name, value);
            }
        }
    }
}