using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.Models;

[ExcludeFromCodeCoverage]
public class Recipe
{
    public string Name { get; set; }
    public IEnumerable<Ingredient> Ingredients { get; set; }
}
