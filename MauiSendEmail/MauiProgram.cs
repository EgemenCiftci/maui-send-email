using CommunityToolkit.Maui;
using MauiSendEmail.Services;
using MauiSendEmail.ViewModels;
using MauiSendEmail.Views;

namespace MauiSendEmail;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        return MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                _ = fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                _ = fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews()
            .Build();
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        _ = mauiAppBuilder.Services.AddSingleton<AppShell>();
        _ = mauiAppBuilder.Services.AddSingleton<SettingsService>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        _ = mauiAppBuilder.Services.AddTransient<SendEmailPageViewModel>();
        _ = mauiAppBuilder.Services.AddTransient<SettingsPageViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        _ = mauiAppBuilder.Services.AddTransient<SendEmailPage>();
        _ = mauiAppBuilder.Services.AddTransient<SettingsPage>();

        return mauiAppBuilder;
    }
}
