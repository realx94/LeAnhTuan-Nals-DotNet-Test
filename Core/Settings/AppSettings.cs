namespace Core.Settings
{
    public class AppSettings
    {
        public string AllowedHosts { get; set; }

        public virtual string[] AllowedHostsValues
        {
            get
            {
                if (string.IsNullOrEmpty(AllowedHosts))
                    return new string[] { };

                return this.AllowedHosts.Split(new char[] { ',', ';' });
            }
        }

        public AuthSetting Auth { get; set; }

        public class AuthSetting
        {
            public string Audience { get; set; }

            public string Authority { get; set; }

            public string SecretKey { get; set; }
        }
    }
}
