namespace LateralApp.Tests.Components.Shared;

public class ProductFormModalTests : BunitContext
{
    private static ProductDto ValidProduct() => new()
    {
        Id = Guid.Empty,
        Name = "Test Product",
        Price = 9.99m,
        Quantity = 5,
        IsActive = true
    };

    [Fact]
    public void Does_Not_Render_When_Visible_Is_False()
    {
        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, false)
            .Add(c => c.Model, ValidProduct())
            .Add(c => c.OnClose, EventCallback.Empty)
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        Assert.Empty(cut.Markup.Trim());
    }

    [Fact]
    public void Renders_When_Visible_Is_True()
    {
        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Model, ValidProduct())
            .Add(c => c.OnClose, EventCallback.Empty)
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        Assert.Contains("modal", cut.Markup);
    }

    [Fact]
    public void Shows_New_Product_Title_When_Id_Is_Empty()
    {
        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Model, new ProductDto { Id = Guid.Empty })
            .Add(c => c.OnClose, EventCallback.Empty)
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        Assert.Contains("New Product", cut.Markup);
    }

    [Fact]
    public void Shows_Edit_Product_Title_When_Id_Is_Not_Empty()
    {
        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Model, new ProductDto { Id = Guid.NewGuid(), Name = "Laptop" })
            .Add(c => c.OnClose, EventCallback.Empty)
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        Assert.Contains("Edit Product", cut.Markup);
    }

    [Fact]
    public void Close_Button_In_Header_Triggers_OnClose()
    {
        var closed = false;

        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Model, ValidProduct())
            .Add(c => c.OnClose, EventCallback.Factory.Create(this, () => closed = true))
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        cut.Find("button.btn-close").Click();

        Assert.True(closed);
    }

    [Fact]
    public void Cancel_Button_Triggers_OnClose()
    {
        var closed = false;

        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Model, ValidProduct())
            .Add(c => c.OnClose, EventCallback.Factory.Create(this, () => closed = true))
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        cut.Find("button.btn-secondary").Click();

        Assert.True(closed);
    }

    [Fact]
    public void Save_Button_Disabled_When_IsSaving_Is_True()
    {
        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Model, ValidProduct())
            .Add(c => c.IsSaving, true)
            .Add(c => c.OnClose, EventCallback.Empty)
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        var saveBtn = cut.Find("button[type='submit']");
        Assert.True(saveBtn.HasAttribute("disabled"));
    }

    [Fact]
    public void Shows_Spinner_On_Save_Button_When_IsSaving()
    {
        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Model, ValidProduct())
            .Add(c => c.IsSaving, true)
            .Add(c => c.OnClose, EventCallback.Empty)
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        Assert.Contains("spinner-border", cut.Markup);
    }

    [Fact]
    public void Renders_Name_Input_Field()
    {
        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Model, ValidProduct())
            .Add(c => c.OnClose, EventCallback.Empty)
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        var nameInput = cut.Find("input[value='Test Product']");
        Assert.NotNull(nameInput);
    }

    [Fact]
    public void Renders_Active_Checkbox()
    {
        var cut = Render<ProductFormModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Model, ValidProduct())
            .Add(c => c.OnClose, EventCallback.Empty)
            .Add(c => c.OnValidSubmit, EventCallback.Empty));

        var checkbox = cut.Find("input[type='checkbox']");
        Assert.NotNull(checkbox);
    }
}
