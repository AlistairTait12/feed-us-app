using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Enums;
using FeedUs.Presentation.Models;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.Tests.DataAccess;

[ExcludeFromCodeCoverage]
[TestFixture]
public class SimpleJsonDataAccessTests
{
    private SimpleJsonDataAccess _dataAccess;
    
    private readonly string _testfilePath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        @"..\..\..\SampleFiles",
        "sampleRecipes.json");

    [SetUp]
    public void SetUp() => _dataAccess = new(_testfilePath);

    [Test]
    public async Task GetRecipes_WhenCalled_ReturnsRecipes()
    {
        // Arrange
        var expected = new List<Recipe>
        {
            new()
            {
                Id = 1,
                Title = "Beans on Toast",
                Description = "Simple classic",
                Ingredients =
                [
                    new() { Name = "Baked Beans", Quantity = 1, Unit = UnitOfMeasure.Can },
                    new() { Name = "Bread", Quantity = 2, Unit = UnitOfMeasure.Slice }
                ]
            },
            new()
            {
                Id = 2,
                Title = "Pasta",
                Description = "Easy and quick meal",
                Ingredients =
                [
                    new() { Name = "Pasta", Quantity = 100, Unit = UnitOfMeasure.Gram },
                    new() { Name = "Chopped Tomatoes", Quantity = 1, Unit = UnitOfMeasure.Can }
                ]
            }
        };

        // Act
        var actual = await _dataAccess.GetRecipesAsync();

        // Assert
        actual.Should().BeEquivalentTo(expected,
            assertionOptions => assertionOptions.WithStrictOrdering());
    }

    [Test]
    public async Task GetRecipe_WhenCalled_ReturnsRecipe()
    {
        // Arrange
        var expected = new Recipe
        {
            Id = 1,
            Title = "Beans on Toast",
            Description = "Simple classic",
            Ingredients =
            [
                new Ingredient { Name = "Baked Beans", Quantity = 1, Unit = UnitOfMeasure.Can },
                new Ingredient { Name = "Bread", Quantity = 2, Unit = UnitOfMeasure.Slice }
            ]
        };

        // Act
        var actual = await _dataAccess.GetRecipe(1);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
