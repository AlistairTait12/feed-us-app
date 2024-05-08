using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.Models;

[ExcludeFromCodeCoverage]
public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<Ingredient> Ingredients { get; set; }
}
