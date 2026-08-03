namespace LateralApp.Tests.Components.Shared;

public class ConfirmModalTests : BunitContext
{
    [Fact]
    public void Does_Not_Render_When_Visible_Is_False()
    {
        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, false)
            .Add(c => c.Title, "Confirm")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Sure?")))
            .Add(c => c.OnConfirm, EventCallback.Empty)
            .Add(c => c.OnCancel, EventCallback.Empty));

        Assert.Empty(cut.Markup.Trim());
    }

    [Fact]
    public void Renders_When_Visible_Is_True()
    {
        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Title, "Delete Product")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Are you sure?")))
            .Add(c => c.OnConfirm, EventCallback.Empty)
            .Add(c => c.OnCancel, EventCallback.Empty));

        Assert.Contains("Delete Product", cut.Markup);
        Assert.Contains("Are you sure?", cut.Markup);
    }

    [Fact]
    public void Renders_Custom_Confirm_Label()
    {
        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Title, "Delete")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Confirm?")))
            .Add(c => c.ConfirmLabel, "Yes, Delete")
            .Add(c => c.OnConfirm, EventCallback.Empty)
            .Add(c => c.OnCancel, EventCallback.Empty));

        Assert.Contains("Yes, Delete", cut.Markup);
    }

    [Fact]
    public void Renders_Default_Confirm_Label_When_Not_Provided()
    {
        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Title, "Confirm")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Sure?")))
            .Add(c => c.OnConfirm, EventCallback.Empty)
            .Add(c => c.OnCancel, EventCallback.Empty));

        Assert.Contains("Confirm", cut.Markup);
    }

    [Fact]
    public void Confirm_Button_Triggers_OnConfirm_Callback()
    {
        var confirmed = false;

        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Title, "Delete")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Sure?")))
            .Add(c => c.ConfirmLabel, "Delete")
            .Add(c => c.OnConfirm, EventCallback.Factory.Create(this, () => confirmed = true))
            .Add(c => c.OnCancel, EventCallback.Empty));

        cut.Find("button.btn-danger").Click();

        Assert.True(confirmed);
    }

    [Fact]
    public void Cancel_Button_Triggers_OnCancel_Callback()
    {
        var cancelled = false;

        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Title, "Delete")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Sure?")))
            .Add(c => c.OnConfirm, EventCallback.Empty)
            .Add(c => c.OnCancel, EventCallback.Factory.Create(this, () => cancelled = true)));

        cut.Find("button.btn-secondary").Click();

        Assert.True(cancelled);
    }

    [Fact]
    public void Close_Button_In_Header_Triggers_OnCancel()
    {
        var cancelled = false;

        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Title, "Delete")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Sure?")))
            .Add(c => c.OnConfirm, EventCallback.Empty)
            .Add(c => c.OnCancel, EventCallback.Factory.Create(this, () => cancelled = true)));

        cut.Find("button.btn-close").Click();

        Assert.True(cancelled);
    }

    [Fact]
    public void Confirm_Button_Disabled_When_IsBusy_Is_True()
    {
        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Title, "Delete")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Sure?")))
            .Add(c => c.IsBusy, true)
            .Add(c => c.OnConfirm, EventCallback.Empty)
            .Add(c => c.OnCancel, EventCallback.Empty));

        var confirmBtn = cut.Find("button.btn-danger");
        Assert.True(confirmBtn.HasAttribute("disabled"));
    }

    [Fact]
    public void Shows_Spinner_When_IsBusy_Is_True()
    {
        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Title, "Delete")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Sure?")))
            .Add(c => c.IsBusy, true)
            .Add(c => c.OnConfirm, EventCallback.Empty)
            .Add(c => c.OnCancel, EventCallback.Empty));

        Assert.Contains("spinner-border", cut.Markup);
    }

    [Fact]
    public void Applies_Custom_ConfirmClass()
    {
        var cut = Render<ConfirmModal>(p => p
            .Add(c => c.Visible, true)
            .Add(c => c.Title, "Deactivate")
            .Add(c => c.Message, (RenderFragment)(b => b.AddContent(0, "Sure?")))
            .Add(c => c.ConfirmClass, "btn-warning")
            .Add(c => c.ConfirmLabel, "Deactivate")
            .Add(c => c.OnConfirm, EventCallback.Empty)
            .Add(c => c.OnCancel, EventCallback.Empty));

        var btn = cut.Find("button.btn-warning");
        Assert.NotNull(btn);
    }
}
