namespace ForDirectumConsole.Data
{
    internal class ConfigurationConnect
    {
        public string Server { get; set; } = "localhost";
        public string Port { get; set; } = "5432";
        public string User_Id { get; set; } = "postgres";
        public string Password { get; set; } = "postgres";
        public string Database { get; set; } = "postgres";
    }
}
