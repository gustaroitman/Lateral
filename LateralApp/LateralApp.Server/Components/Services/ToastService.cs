namespace LateralApp.Components.Services;

public enum ToastType { Success, Error, Info }

public class ToastService
{
    public event Action<string, ToastType>? OnShow;

    public void Show(string message, ToastType type = ToastType.Success)
        => OnShow?.Invoke(message, type);
}
