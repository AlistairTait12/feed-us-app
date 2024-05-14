using FakeItEasy;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Models;
using FeedUs.Presentation.ViewModels;
using FeedUs.Presentation.Wrappers;

namespace FeedUs.Presentation.Tests.ViewModels;

public class UpdateRecipeViewModelTests
{
    [Test]
    public async Task UpdateRecipe_Should_UpdateRecipeAndNavigateBack()
    {
        // Arrange
        var dataAccess = A.Fake<IDataAccess>();
        var navigationWrapper = A.Fake<INavigationWrapper>();
        var viewModel = new UpdateRecipeViewModel(dataAccess, navigationWrapper);
        var recipe = new Recipe
        {
            Id = 12,
        };
        viewModel.Recipe = recipe;

        // Act
        await viewModel.UpdateRecipe();

        // Assert
        A.CallTo(() => dataAccess.UpdateRecipeAsync(A<Recipe>.That.Matches(r => r.Id == recipe.Id)))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => navigationWrapper.GoToAsync("..", A<Dictionary<string, object>>.Ignored))
            .MustHaveHappenedOnceExactly();
    }
}
