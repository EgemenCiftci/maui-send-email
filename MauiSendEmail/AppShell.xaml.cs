using MauiSendEmail.Services;

namespace MauiSendEmail;

public partial class AppShell : Shell
{
    public AppShell(SettingsService settingsService)
    {
        InitializeComponent();

        _ = settingsService.IsDataCompleteAsync().Result ? GoToAsync("//SendEmail") : GoToAsync("//Settings");
    }
}
