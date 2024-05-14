namespace FeedUs.Presentation.Enums;

public enum UnitOfMeasure
{
    None,
    [DisplayString("Item")]
    Item,
    [DisplayString("g")]
    Gram,
    [DisplayString("kg")]
    Kilogram,
    [DisplayString("ml")]
    Milliliter,
    [DisplayString("l")]
    Liter,
    [DisplayString("tsp")]
    Teaspoon,
    [DisplayString("tbsp")]
    Tablespoon,
    [DisplayString("cup")]
    Cup,
    [DisplayString("oz")]
    Ounce,
    [DisplayString("lb")]
    Pound,
    [DisplayString("fl oz")]
    FluidOunce,
    [DisplayString("pt")]
    Pint,
    [DisplayString("qt")]
    Quart,
    [DisplayString("gal")]
    Gallon,
    [DisplayString("can")]
    Can,
    [DisplayString("slice")]
    Slice
}
