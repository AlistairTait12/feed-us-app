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
        return recipes.FirstOrDefault(recipe => recipe.Id == id);
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
        stream.Dispose();
        return recipes;
    }

    public async Task AddRecipeAsync(Recipe recipe)
    {
        var allRecipes = await GetRecipesAsync();
        recipe.Id = allRecipes.Any()
            ? allRecipes.Last().Id + 1
            : 1;
        var newList = allRecipes.Append(recipe);
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        using var stream = new FileStream(_filePath, FileMode.Open,
            FileAccess.ReadWrite, FileShare.Read);
        await JsonSerializer.SerializeAsync(stream, newList, options);
        stream.Dispose();
    }

    public async Task DeleteRecipeAsync(int id)
    {
        var recipes = (await GetRecipesAsync()).ToList();
        recipes.Remove(recipes.First(recipe => recipe.Id == id));
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        using var stream = new FileStream(_filePath, FileMode.Truncate,
            FileAccess.ReadWrite, FileShare.Read);
        await JsonSerializer.SerializeAsync(stream, recipes, options);
        stream.Dispose();
    }

    public async Task UpdateRecipeAsync(Recipe recipe)
    {
        var recipes = (await GetRecipesAsync()).ToList();
        var updatedRecipeIndex = recipes.FindIndex(r => r.Id == recipe.Id);
        if (updatedRecipeIndex >= 0)
        {
            recipes[updatedRecipeIndex] = recipe;
        }
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        using var stream = new FileStream(_filePath, FileMode.Truncate,
            FileAccess.ReadWrite, FileShare.Read);
        await JsonSerializer.SerializeAsync(stream, recipes, options);
        stream.Dispose();
    }
}
