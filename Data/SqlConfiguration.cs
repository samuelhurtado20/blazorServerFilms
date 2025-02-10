namespace blazorServerFilms.Data
{
    public class SqlConfiguration(string conString)
    {
        public string? ConString { get; } = conString;
    }
}