using System;

namespace DeclarableDataGrid.PropertyDescriptors
{
    public sealed class DynamicColumnPropertyDescriptor : DeclarableDataGridPropertyDescriptor
    {
        private readonly Type _componentType;
        private readonly Type _columnType;

        public DynamicColumnPropertyDescriptor(Type componentType, Type columnType, int displayIndex, string name, Attribute[] attributes) : base(displayIndex, name, attributes)
        {
            _componentType = componentType;
            _columnType = columnType;
        }

        public override Type ComponentType => _componentType;

        public override bool IsReadOnly => true;

        public override Type PropertyType => _columnType;

        public override bool CanResetValue(object component) => false;

        public override object GetValue(object component)
        {
            if (component is DeclarableColumnDataDescriptor item)
            {
                return item.GetDynamicPropertyValue(Name);
            }

            return null;
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            if (value.GetType() != PropertyType)
            {
                throw new InvalidOperationException($"Value property type {value.GetType()} must match dynamic property type: {PropertyType}");
            }

            if (component is DeclarableColumnDataDescriptor item)
            {
                item.SetDynamicPropertyValue(Name, value);
            }
        }

        public override bool ShouldSerializeValue(object component) => false;
    }
}