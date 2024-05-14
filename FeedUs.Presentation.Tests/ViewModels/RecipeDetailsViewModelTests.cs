using FakeItEasy;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Models;
using FeedUs.Presentation.ViewModels;
using FeedUs.Presentation.Wrappers;
using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.Tests.ViewModels;

[ExcludeFromCodeCoverage]
[TestFixture]
public class RecipeDetailsViewModelTests
{
    private IDataAccess _dataAccess;
    private INavigationWrapper _navigationWrapper;
    private RecipeDetailsViewModel _viewModel;

    [SetUp]
    public void SetUp()
    {
        _dataAccess = A.Fake<IDataAccess>();
        _navigationWrapper = A.Fake<INavigationWrapper>();
        _viewModel = new RecipeDetailsViewModel(_dataAccess, _navigationWrapper)
        {
            Recipe = new Recipe { Id = 1, Title = "Test" }
        };
    }

    [Test]
    public async Task DeleteRecipeAsync_WhenAnswerIsTrue_ShouldDeleteRecipeAndNavigateBack()
    {
        // Arrange
        A.CallTo(() => _navigationWrapper.DisplayAlert("Confirmation",
            "Are you sure you want to delete the recipe?", "Yes", "No"))
            .Returns(Task.FromResult(true));

        // Act
        await _viewModel.DeleteRecipeAsync();

        // Assert
        A.CallTo(() => _dataAccess.DeleteRecipeAsync(1)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _navigationWrapper.GoToAsync("..")).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task DeleteRecipeAsync_WhenAnswerIsFalse_ShouldNotDeleteRecipeAndNavigateBack()
    {
        // Arrange
        A.CallTo(() => _navigationWrapper.DisplayAlert("Confirmation",
            "Are you sure you want to delete the recipe?", "Yes", "No"))
            .Returns(Task.FromResult(false));

        // Act
        await _viewModel.DeleteRecipeAsync();

        // Assert
        A.CallTo(() => _dataAccess.DeleteRecipeAsync(An<int>.Ignored)).MustNotHaveHappened();
        A.CallTo(() => _navigationWrapper.GoToAsync(A<string>.Ignored)).MustNotHaveHappened();
    }
}
