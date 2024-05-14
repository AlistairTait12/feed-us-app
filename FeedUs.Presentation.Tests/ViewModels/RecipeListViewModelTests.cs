using FakeItEasy;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Models;
using FeedUs.Presentation.ViewModels;
using FeedUs.Presentation.Wrappers;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.Tests.ViewModels;

[ExcludeFromCodeCoverage]
[TestFixture]
public class RecipeListViewModelTests
{
    private IDataAccess _dataAccess;
    private INavigationWrapper _navigationWrapper;

    [SetUp]
    public void SetUp()
    {
        _dataAccess = A.Fake<IDataAccess>();
        _navigationWrapper = A.Fake<INavigationWrapper>();
        var recipes = new List<Recipe>
        {
            new () { Title = "Avocado Smash" },
            new () { Title = "Pasta" }
        };
        A.CallTo(() => _dataAccess.GetRecipesAsync()).Returns(recipes);
    }

    [Test]
    public async Task LoadRecipesAsync_PopulatesRecipesInViewModel()
    {
        // Arrange
        var expected = new List<Recipe>
        {
            new () { Title = "Avocado Smash" },
            new () { Title = "Pasta" }
        };

        var viewModel = new RecipeListViewModel(_dataAccess, _navigationWrapper);

        // Act
        await viewModel.LoadRecipesAsync();

        // Assert
        viewModel.Recipes.Should().BeEquivalentTo(expected,
            assertionOptions => assertionOptions.WithStrictOrdering());
    }
}
