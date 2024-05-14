using FakeItEasy;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Enums;
using FeedUs.Presentation.Models;
using FeedUs.Presentation.ViewModels;
using FeedUs.Presentation.Wrappers;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.Tests.ViewModels;

[ExcludeFromCodeCoverage]
[TestFixture]
public class CreateRecipeViewModelTests
{
    private IDataAccess _dataAccess;
    private INavigationWrapper _navigationWrapper;
    private CreateRecipeViewModel _viewModel;

    [SetUp]
    public void SetUp()
    {
        _dataAccess = A.Fake<IDataAccess>();
        _navigationWrapper = A.Fake<INavigationWrapper>();
        _viewModel = new (_dataAccess, _navigationWrapper);
    }

    [Test]
    public void AddIngredient_AddsIngredientToIngredients()
    {
        // Arrange
        var expected = new List<Ingredient>
        {
            new() { Name = "Ingredient 1", Quantity = 100, Unit = UnitOfMeasure.Gram },
            new() { Name = "Ingredient 2", Quantity = 2, Unit = UnitOfMeasure.Can }
        };

        // Act
        _viewModel.CurrentIngredientName = "Ingredient 1";
        _viewModel.CurrentIngredientAmount = "100";
        _viewModel.CurrentIngredientUnit = "g";
        _viewModel.AddIngredient();
        _viewModel.CurrentIngredientName = "Ingredient 2";
        _viewModel.CurrentIngredientAmount = "2";
        _viewModel.CurrentIngredientUnit = "can";
        _viewModel.AddIngredient();

        // Assert
        _viewModel.Ingredients.Should().BeEquivalentTo(expected,
            assertionOptions => assertionOptions.WithStrictOrdering());
        _viewModel.CurrentIngredientName.Should().BeNullOrEmpty();
        _viewModel.CurrentIngredientAmount.Should().BeNullOrEmpty();
        _viewModel.CurrentIngredientUnit.Should().BeNullOrEmpty();
    }

    [Test]
    public void AddStep_AddsStepToSteps()
    {
        // Arrange
        var expected = new List<string>
        {
            "Step 1",
            "Step 2"
        };

        // Act
        _viewModel.CurrentStep = "Step 1";
        _viewModel.AddStep();
        _viewModel.CurrentStep = "Step 2";
        _viewModel.AddStep();

        // Assert
        _viewModel.Steps.Should().BeEquivalentTo(expected);
        _viewModel.CurrentStep.Should().BeNullOrEmpty();
    }

    [Test]
    public async Task CreateRecipe_CreatesRecipe()
    {
        // Act
        _viewModel.Title = "Recipe";
        _viewModel.Description = "Description";
        _viewModel.CurrentIngredientName = "Ingredient 1";
        _viewModel.CurrentIngredientAmount = "100";
        _viewModel.CurrentIngredientUnit = "g";
        _viewModel.AddIngredient();
        _viewModel.CurrentStep = "Step 1";
        _viewModel.AddStep();
        await _viewModel.CreateRecipeAsync();

        // Assert
        A.CallTo(() => _dataAccess
            .AddRecipeAsync(A<Recipe>.That.Matches(r => r.Title == "Recipe")))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => _navigationWrapper.GoToAsync(".."))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void RemoveIngredient_RemovesIngredientFromIngredients()
    {
        // Arrange
        var expected = new List<Ingredient>
        {
            new() { Name = "Ingredient 2", Quantity = 2, Unit = UnitOfMeasure.Can }
        };

        _viewModel.CurrentIngredientName = "Ingredient 1";
        _viewModel.CurrentIngredientAmount = "100";
        _viewModel.CurrentIngredientUnit = "g";
        _viewModel.AddIngredient();
        _viewModel.CurrentIngredientName = "Ingredient 2";
        _viewModel.CurrentIngredientAmount = "2";
        _viewModel.CurrentIngredientUnit = "can";
        _viewModel.AddIngredient();

        // Act
        _viewModel.RemoveIngredient(_viewModel.Ingredients[0]);

        // Assert
        _viewModel.Ingredients.Should().BeEquivalentTo(expected);
    }

    // TODO: RemoveStep just removes first matching string, not necessarily the one that was clicked
    [Test]
    public void RemoveStep_RemovesStepFromSteps()
    {
        // Arrange
        var expected = new List<string>
        {
            "Step 2"
        };

        _viewModel.CurrentStep = "Step 1";
        _viewModel.AddStep();
        _viewModel.CurrentStep = "Step 2";
        _viewModel.AddStep();

        // Act
        _viewModel.RemoveStep(_viewModel.Steps[0]);

        // Assert
        _viewModel.Steps.Should().BeEquivalentTo(expected);
    }
}
