using MauiSendEmail.ViewModels;

namespace MauiSendEmail.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}