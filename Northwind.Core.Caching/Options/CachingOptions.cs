namespace Northwind.Core.Caching.Options
{
    public class CachingOptions
    {
        public string DirectoryPath { get; set; }

        public int MaxCount { get; set; }

        public int ExpirationTimeMs { get; set; }
    }
}
