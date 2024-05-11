using FeedUs.Presentation.Models;

namespace FeedUs.Presentation.DataAccess;

public interface IDataAccess
{
    Task<IEnumerable<Recipe>> GetRecipesAsync();
    Task<Recipe> GetRecipe(int id);
    Task AddRecipeAsync(Recipe recipe);
    // TODO: Task UpdateRecipeAsync(Recipe recipe);
    // TODO: Task DeleteRecipeAsync(int id);
}
