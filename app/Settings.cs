namespace Pgcode
{
    public class Settings
    {
        public int Port { get; set; } = 5000;
        public string Host { get; set; } = "localhost";
        public string RunAsUser { get; set; } = null;
        public string MinimalPgVersion { get; set; } = "9.5";
        public string PgCodeSchema { get; set; } = "pgcode";
        public string DefaultSchema { get; set; } = "public";
        public string SkipSchemaPattern { get; set; } = "(pg_temp|pg_toast)%";
        public bool LogRequests { get; set; } = true;
        public bool LogInternalConnectionNotice { get; set; } = true;
        public string WindowsOpenCommand { get; set; } = null;
        public string OsxOpenCommand { get; set; } = "open";
        public string LinuxOpenCommand { get; set; } = "xdg-open";
        public string FreeBsdOpenCommand { get; set; } = "xdg-open";
    }
}