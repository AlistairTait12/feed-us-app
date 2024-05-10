using FakeItEasy;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Enums;
using FeedUs.Presentation.Models;
using FeedUs.Presentation.ViewModels;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.Tests.ViewModels;

[ExcludeFromCodeCoverage]
[TestFixture]
public class CreateRecipeViewModelTests
{
    private IDataAccess _dataAccess;
    private CreateRecipeViewModel _viewModel;

    [SetUp]
    public void SetUp()
    {
        _dataAccess = A.Fake<IDataAccess>();
        _viewModel = new CreateRecipeViewModel(_dataAccess);
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
        _viewModel.CurrentIngredientAmount = 100;
        _viewModel.CurrentIngredientUnit = "Gram";
        _viewModel.AddIngredient();
        _viewModel.CurrentIngredientName = "Ingredient 2";
        _viewModel.CurrentIngredientAmount = 2;
        _viewModel.CurrentIngredientUnit = "Can";
        _viewModel.AddIngredient();

        // Assert
        _viewModel.Ingredients.Should().BeEquivalentTo(expected,
            assertionOptions => assertionOptions.WithStrictOrdering());
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
    }

    [Test]
    public async Task CreateRecipe_CreatesRecipe()
    {
        // Act
        _viewModel.Title = "Recipe";
        _viewModel.Description = "Description";
        _viewModel.CurrentIngredientName = "Ingredient 1";
        _viewModel.CurrentIngredientAmount = 100;
        _viewModel.CurrentIngredientUnit = "Gram";
        _viewModel.AddIngredient();
        _viewModel.CurrentStep = "Step 1";
        _viewModel.AddStep();
        await _viewModel.CreateRecipeAsync();

        // Assert
        A.CallTo(() => _dataAccess.AddRecipeAsync(A<Recipe>.That.Matches(r => r.Title == "Recipe"))).MustHaveHappened();
    }
}
