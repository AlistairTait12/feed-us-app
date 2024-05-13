using FeedUs.Presentation.Models;

namespace FeedUs.Presentation.DataAccess;

public interface IDataAccess
{
    Task<IEnumerable<Recipe>> GetRecipesAsync();
    Task<Recipe> GetRecipe(int id);
    Task AddRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(int id);
    // TODO: Task UpdateRecipeAsync(Recipe recipe);
}
