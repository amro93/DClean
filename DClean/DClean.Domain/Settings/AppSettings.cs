namespace DClean.Domain.Settings
{
    public class AppSettings
    {
        public bool Seed { get; set; }
        public string ServerRootAddress { get; set; }
        public string ClientRootAddress { get; set; }
        public string CorsOrigins { get; set; }
    }
}
