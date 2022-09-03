using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiSendEmail.Services;

namespace MauiSendEmail.ViewModels;

public class SettingsPageViewModel : ObservableObject
{
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private string _from;
    public string From
    {
        get => _from;
        set => SetProperty(ref _from, value);
    }

    private string _host;
    public string Host
    {
        get => _host;
        set => SetProperty(ref _host, value);
    }

    private string _port;
    public string Port
    {
        get => _port;
        set => SetProperty(ref _port, value);
    }

    private bool _useSsl;
    public bool UseSsl
    {
        get => _useSsl;
        set => SetProperty(ref _useSsl, value);
    }

    private bool _useAuth;
    public bool UseAuth
    {
        get => _useAuth;
        set => SetProperty(ref _useAuth, value);
    }

    private string _userName;
    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    private string _password;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public AsyncRelayCommand SaveCommand { get; }

    private readonly SettingsService _settingsService;

    public SettingsPageViewModel(SettingsService settingsService)
    {
        _settingsService = settingsService;
        From = settingsService.From;
        Host = settingsService.Host;
        Port = settingsService.Port;
        UseSsl = settingsService.UseSsl;
        UseAuth = settingsService.UseAuth;
        UserName = settingsService.UserName;
        Password = settingsService.GetPasswordAsync().Result;

        SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
    }

    private bool CanSave()
    {
        return !IsBusy;
    }

    private async Task SaveAsync()
    {
        IsBusy = true;

        try
        {
            _settingsService.From = From;
            _settingsService.Host = Host;
            _settingsService.Port = Port;
            _settingsService.UseSsl = UseSsl;
            _settingsService.UseAuth = UseAuth;
            _settingsService.UserName = UserName;
            await _settingsService.SetPasswordAsync(Password);

            await Application.Current.MainPage.DisplayAlert("Success", "Saved.", "OK");
            await Shell.Current.GoToAsync("//SendEmail");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
