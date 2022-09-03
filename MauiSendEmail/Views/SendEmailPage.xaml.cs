using MauiSendEmail.ViewModels;

namespace MauiSendEmail.Views;

public partial class SendEmailPage : ContentPage
{
    public SendEmailPage(SendEmailPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}

