namespace LateralApp.Tests.Components.Shared;

public class AlertMessageTests : BunitContext
{
    [Fact]
    public void Does_Not_Render_When_Message_Is_Null()
    {
        var cut = Render<AlertMessage>(p => p
            .Add(c => c.Message, (string?)null)
            .Add(c => c.OnDismiss, EventCallback.Empty));

        Assert.Empty(cut.Markup.Trim());
    }

    [Fact]
    public void Renders_Alert_When_Message_Is_Set()
    {
        var cut = Render<AlertMessage>(p => p
            .Add(c => c.Message, "Something went wrong")
            .Add(c => c.OnDismiss, EventCallback.Empty));

        Assert.Contains("Something went wrong", cut.Markup);
    }

    [Fact]
    public void Renders_Alert_Danger_Class()
    {
        var cut = Render<AlertMessage>(p => p
            .Add(c => c.Message, "Error!")
            .Add(c => c.OnDismiss, EventCallback.Empty));

        var alert = cut.Find(".alert-danger");
        Assert.NotNull(alert);
    }

    [Fact]
    public void Renders_Dismiss_Button()
    {
        var cut = Render<AlertMessage>(p => p
            .Add(c => c.Message, "An error")
            .Add(c => c.OnDismiss, EventCallback.Empty));

        var button = cut.Find("button.btn-close");
        Assert.NotNull(button);
    }

    [Fact]
    public void Dismiss_Button_Triggers_OnDismiss_Callback()
    {
        var wasCalled = false;

        var cut = Render<AlertMessage>(p => p
            .Add(c => c.Message, "Error")
            .Add(c => c.OnDismiss, EventCallback.Factory.Create(this, () => wasCalled = true)));

        cut.Find("button.btn-close").Click();

        Assert.True(wasCalled);
    }

    [Fact]
    public void Renders_Html_Escaped_Message()
    {
        const string message = "Error: record <not> found";

        var cut = Render<AlertMessage>(p => p
            .Add(c => c.Message, message)
            .Add(c => c.OnDismiss, EventCallback.Empty));

        Assert.Contains("Error: record", cut.Markup);
    }

    [Fact]
    public void Re_Renders_When_Message_Changes_From_Null_To_Value()
    {
        var cut = Render<AlertMessage>(p => p
            .Add(c => c.Message, (string?)null)
            .Add(c => c.OnDismiss, EventCallback.Empty));

        Assert.Empty(cut.Markup.Trim());

        cut.Render(p => p
            .Add(c => c.Message, "Now visible"));

        Assert.Contains("Now visible", cut.Markup);
    }

    [Fact]
    public void Re_Renders_When_Message_Changes_To_Null()
    {
        var cut = Render<AlertMessage>(p => p
            .Add(c => c.Message, "Visible")
            .Add(c => c.OnDismiss, EventCallback.Empty));

        cut.Render(p => p
            .Add(c => c.Message, (string?)null));

        Assert.Empty(cut.Markup.Trim());
    }
}
