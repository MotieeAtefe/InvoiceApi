namespace InvoiceApi.Settings
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string InvoicesCollectionName { get; set; } = null!;
    }
}
