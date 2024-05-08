using FeedUs.Presentation.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FeedUs.Presentation.DataAccess;

public class SimpleJsonDataAccess : IDataAccess
{
    private readonly string _filePath;

    public SimpleJsonDataAccess(string filePath) => _filePath = filePath;

    public async Task<Recipe> GetRecipe(int id)
    {
        var recipes = await GetRecipesAsync();
        return recipes.FirstOrDefault(r => r.Id == id);
    }

    public async Task<IEnumerable<Recipe>> GetRecipesAsync()
    {
        using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var recipes = await JsonSerializer.DeserializeAsync<IEnumerable<Recipe>>(stream, options);
        return recipes;
    }
}
