using System;
using System.ComponentModel;
using System.Reflection;

namespace DeclarableDataGrid
{
    public class DeclarableColumnDataPropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyInfo _propertyInfo;

        public DeclarableColumnDataPropertyDescriptor(PropertyInfo propertyInfo, int displayIndex, Attribute[] attributes) : base(propertyInfo.Name, attributes)
        {
            _propertyInfo = propertyInfo;
            DisplayIndex = displayIndex;
        }

        public override Type ComponentType => _propertyInfo.DeclaringType;

        public override bool IsReadOnly => true;

        public override Type PropertyType => _propertyInfo.PropertyType;

        public int DisplayIndex { get; private set; }

        public override bool CanResetValue(object component) => false;

        public override object GetValue(object component)
        {
            if (component is DeclarableColumnDataDescriptor item)
            {
                return _propertyInfo.GetValue(item);
            }

            return null;
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            if (component is DeclarableColumnDataDescriptor item)
            {
                _propertyInfo.SetValue(item, value);
            }
        }

        public override bool ShouldSerializeValue(object component) => false;

        public string GetDisplayName()
        {
            return string.IsNullOrEmpty(DisplayName)
                    ? Name
                    : DisplayName;
        }
    }
}