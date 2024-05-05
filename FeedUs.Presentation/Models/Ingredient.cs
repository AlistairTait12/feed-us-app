﻿using FeedUs.Presentation.Enums;
using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.Models;

[ExcludeFromCodeCoverage]
public class Ingredient
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public UnitOfMeasure Unit { get; set; }
}
