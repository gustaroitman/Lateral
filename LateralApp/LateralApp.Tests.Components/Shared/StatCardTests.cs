namespace LateralApp.Tests.Components.Shared;

public class StatCardTests : BunitContext
{
    [Fact]
    public void Renders_Title_Correctly()
    {
        var cut = Render<StatCard>(p => p
            .Add(c => c.Title, "Total Products")
            .Add(c => c.Value, "42"));

        Assert.Contains("Total Products", cut.Markup);
    }

    [Fact]
    public void Renders_Value_Correctly()
    {
        var cut = Render<StatCard>(p => p
            .Add(c => c.Title, "Active Products")
            .Add(c => c.Value, "100"));

        Assert.Contains("100", cut.Markup);
    }

    [Fact]
    public void Applies_Custom_Color_To_Style()
    {
        const string customColor = "#ff5733";

        var cut = Render<StatCard>(p => p
            .Add(c => c.Title, "Revenue")
            .Add(c => c.Value, "$500")
            .Add(c => c.Color, customColor));

        Assert.Contains(customColor, cut.Markup);
    }

    [Fact]
    public void Uses_Default_Color_When_Not_Specified()
    {
        var cut = Render<StatCard>(p => p
            .Add(c => c.Title, "Products")
            .Add(c => c.Value, "10"));

        // Default color is #0d6efd
        Assert.Contains("#0d6efd", cut.Markup);
    }

    [Fact]
    public void Renders_Both_Title_And_Value_Together()
    {
        var cut = Render<StatCard>(p => p
            .Add(c => c.Title, "Inventory Value")
            .Add(c => c.Value, "$1,234.56"));

        Assert.Contains("Inventory Value", cut.Markup);
        Assert.Contains("$1,234.56", cut.Markup);
    }

    [Fact]
    public void Renders_With_Long_Title()
    {
        const string longTitle = "A Very Long Product Category Title";

        var cut = Render<StatCard>(p => p
            .Add(c => c.Title, longTitle)
            .Add(c => c.Value, "0"));

        Assert.Contains(longTitle, cut.Markup);
    }
}
