namespace Api.Models
{
    public class AppSettings
    {
        public string DatabaseFile { get; set; }
        public string ConnectionString { get { return $"Data Source={Environment.ExpandEnvironmentVariables(DatabaseFile)}"; } }

        public AppSettings()
        {
            DatabaseFile = string.Empty;
        }
    }
}
