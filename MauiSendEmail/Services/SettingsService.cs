namespace MauiSendEmail.Services
{
    public class SettingsService
    {
        private const string FromKey = "from";
        private const string HostKey = "host";
        private const string PortKey = "port";
        private const string UseSslKey = "use-ssl";
        private const string UseAuthKey = "use-auth";
        private const string UserNameKey = "user-name";
        private const string PasswordKey = "password";

        public string From
        {
            get => Preferences.Get(FromKey, null);
            set => Preferences.Set(FromKey, value);
        }

        public string Host
        {
            get => Preferences.Get(HostKey, null);
            set => Preferences.Set(HostKey, value);
        }

        public string Port
        {
            get => Preferences.Get(PortKey, null);
            set => Preferences.Set(PortKey, value);
        }

        public bool UseSsl
        {
            get => Preferences.Get(UseSslKey, false);
            set => Preferences.Set(UseSslKey, value);
        }

        public bool UseAuth
        {
            get => Preferences.Get(UseAuthKey, false);
            set => Preferences.Set(UseAuthKey, value);
        }

        public string UserName
        {
            get => Preferences.Get(UserNameKey, null);
            set => Preferences.Set(UserNameKey, value);
        }

        public async Task<string> GetPasswordAsync()
        {
            return await SecureStorage.Default.GetAsync(PasswordKey);
        }

        public async Task SetPasswordAsync(string value)
        {
            await SecureStorage.Default.SetAsync(PasswordKey, value);
        }

        public async Task<bool> IsDataCompleteAsync()
        {
            return !(string.IsNullOrWhiteSpace(From) ||
                     string.IsNullOrWhiteSpace(Host) ||
                     string.IsNullOrWhiteSpace(Port) ||
                     string.IsNullOrWhiteSpace(UserName) ||
                     string.IsNullOrWhiteSpace(await GetPasswordAsync()));
        }
    }
}
