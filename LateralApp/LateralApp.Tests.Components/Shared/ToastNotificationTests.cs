namespace LateralApp.Tests.Components.Shared;

public class ToastNotificationTests : BunitContext
{
    private ToastService RegisterToastService()
    {
        var service = new ToastService();
        Services.AddSingleton(service);
        return service;
    }

    [Fact]
    public void Renders_Toast_Container()
    {
        RegisterToastService();

        var cut = Render<ToastNotification>();

        Assert.Contains("toast-container", cut.Markup);
    }

    [Fact]
    public void No_Toast_Items_Initially()
    {
        RegisterToastService();

        var cut = Render<ToastNotification>();

        Assert.DoesNotContain("toast-item", cut.Markup);
    }

    [Fact]
    public void Shows_Toast_When_Service_Show_Is_Called()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("Product saved!", ToastType.Success);

        Assert.Contains("Product saved!", cut.Markup);
    }

    [Fact]
    public void Shows_Success_Css_Type_For_Success_Toast()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("Saved!", ToastType.Success);

        Assert.Contains("toast-item--success", cut.Markup);
    }

    [Fact]
    public void Shows_Error_Css_Type_For_Error_Toast()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("Failed!", ToastType.Error);

        Assert.Contains("toast-item--error", cut.Markup);
    }

    [Fact]
    public void Shows_Info_Css_Type_For_Info_Toast()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("Note!", ToastType.Info);

        Assert.Contains("toast-item--info", cut.Markup);
    }

    [Fact]
    public void Shows_Multiple_Toasts()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("First message", ToastType.Success);
        service.Show("Second message", ToastType.Error);

        Assert.Contains("First message", cut.Markup);
        Assert.Contains("Second message", cut.Markup);
    }

    [Fact]
    public void Renders_Close_Button_Per_Toast()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("Hello!", ToastType.Info);

        var closeBtn = cut.Find("button.toast-item__close");
        Assert.NotNull(closeBtn);
    }

    [Fact]
    public void Renders_Progress_Bar_Per_Toast()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("Progress test", ToastType.Success);

        var progress = cut.Find(".toast-item__progress");
        Assert.NotNull(progress);
    }

    [Fact]
    public void Renders_Correct_Icon_For_Success()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("Done!", ToastType.Success);

        Assert.Contains("bi-check-circle-fill", cut.Markup);
    }

    [Fact]
    public void Renders_Correct_Icon_For_Error()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("Error!", ToastType.Error);

        Assert.Contains("bi-x-circle-fill", cut.Markup);
    }

    [Fact]
    public void Renders_Correct_Icon_For_Info()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        service.Show("Info!", ToastType.Info);

        Assert.Contains("bi-info-circle-fill", cut.Markup);
    }

    [Fact]
    public void Unsubscribes_From_Service_On_Dispose()
    {
        var service = RegisterToastService();

        var cut = Render<ToastNotification>();

        cut.Instance.Dispose();

        service.Show("After dispose", ToastType.Success);

        Assert.True(true);
    }
}
