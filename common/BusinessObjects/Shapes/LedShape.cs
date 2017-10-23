using System;
using System.ComponentModel;
using System.Reflection;

namespace BusinessObjects.Shapes
{
    public enum LedShape
    {
        [Description("Circle Large")]
        CircleLarge,

        [Description("Circle Med")]
        CircleMed,

        [Description("Circle Small")]
        CircleSmall,

        [Description("Rectangle")]
        Rectangle,

        [Description("Square 1-inch")]
        Square,

        [Description("Arrow")]
        Arrow,

        [Description("Triangle")]
        Triangle,

        [Description("Thin Triangle Large")]
        ThinTriangleLarge,

        [Description("Thin Triangle Small")]
        ThinTriangleSmall
    };

    public static class LedShapeExtensions
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                        Attribute.GetCustomAttribute(field,
                            typeof (DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }

}