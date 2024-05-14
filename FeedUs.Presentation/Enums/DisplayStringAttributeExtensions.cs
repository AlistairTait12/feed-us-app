using System.Reflection;

namespace FeedUs.Presentation.Enums;

public static class DisplayStringAttributeExtensions
{
    public static string GetDisplayString(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        var field = type.GetField(name);
        var attribute = field.GetCustomAttribute<DisplayStringAttribute>();
        return attribute?.DisplayString ?? name;
    }
}
