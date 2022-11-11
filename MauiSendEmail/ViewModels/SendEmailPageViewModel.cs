using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MailKit.Net.Smtp;
using MauiSendEmail.Services;
using MimeKit;

namespace MauiSendEmail.ViewModels;

public class SendEmailPageViewModel : ObservableObject
{
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private bool _isCcVisible;
    public bool IsCcVisible
    {
        get => _isCcVisible;
        set => SetProperty(ref _isCcVisible, value);
    }

    private bool _isBccVisible;
    public bool IsBccVisible
    {
        get => _isBccVisible;
        set => SetProperty(ref _isBccVisible, value);
    }

    private string? _to;
    public string? To
    {
        get => _to;
        set => SetProperty(ref _to, value);
    }

    private string? _cc;
    public string? Cc
    {
        get => _cc;
        set => SetProperty(ref _cc, value);
    }

    private string? _bcc;
    public string? Bcc
    {
        get => _bcc;
        set => SetProperty(ref _bcc, value);
    }

    private string? _subject;
    public string? Subject
    {
        get => _subject;
        set => SetProperty(ref _subject, value);
    }

    private string? _body;
    public string? Body
    {
        get => _body;
        set => SetProperty(ref _body, value);
    }

    public IAsyncRelayCommand SendCommand { get; }
    public IRelayCommand ShowCcCommand { get; }
    public IRelayCommand ShowBccCommand { get; }

    private readonly SettingsService _settingsService;

    public SendEmailPageViewModel(SettingsService settingsService)
    {
        _settingsService = settingsService;
        SendCommand = new AsyncRelayCommand(SendAsync, CanSend);
        ShowCcCommand = new RelayCommand(() => IsCcVisible = true);
        ShowBccCommand = new RelayCommand(() => IsBccVisible = true);
    }

    private bool CanSend()
    {
        return !IsBusy;
    }

    private async Task SendAsync()
    {
        IsBusy = true;

        try
        {
            MimeMessage message = new();
            message.From.Add(new MailboxAddress(_settingsService.From, _settingsService.From));
            message.To.Add(new MailboxAddress(To, To));
            message.Subject = Subject;
            message.Body = new TextPart("plain") { Text = Body };

            using SmtpClient client = new();
            await client.ConnectAsync(_settingsService.Host, Convert.ToInt32(_settingsService.Port), _settingsService.UseSsl);

            if (_settingsService.UseAuth)
            {
                await client.AuthenticateAsync(_settingsService.UserName, await _settingsService.GetPasswordAsync());
            }

            _ = await client.SendAsync(message);
            await client.DisconnectAsync(true);

            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Email sent.", "OK");
            }
        }
        catch (Exception ex)
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}
