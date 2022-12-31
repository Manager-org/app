namespace Manager.Utils
{
    class AppSettings
    {
        public IConfiguration Configuration;

        // LOCAL, DEV, STAGE, PROD
        public AppSettings() => Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + "/Properties")
            .AddJsonFile($"settings.{new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build().GetValue<string>("Mode")}.json").Build();
    }
}
