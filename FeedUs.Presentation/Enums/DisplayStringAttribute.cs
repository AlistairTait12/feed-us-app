using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.Enums;

[ExcludeFromCodeCoverage]
public class DisplayStringAttribute : Attribute
{
    public string DisplayString { get; private set; }

    public DisplayStringAttribute(string displayString) => DisplayString = displayString;
}
