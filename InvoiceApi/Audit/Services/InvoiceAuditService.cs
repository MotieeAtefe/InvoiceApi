using InvoiceApi.Audit.Model;
using InvoiceApi.Audit.Services;
using InvoiceApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace InvoiceApi.Audit
{
    public class InvoiceAuditService : IInvoiceAuditService
    {
        private readonly IMongoCollection<InvoiceAuditEvent> _collection;

        public InvoiceAuditService(IMongoClient mongoClient, IOptions<MongoSettings> mongoSettings)
        {
            var database = mongoClient.GetDatabase(
                mongoSettings.Value.DatabaseName);

            _collection = database.GetCollection<InvoiceAuditEvent>(
                mongoSettings.Value.InvoicesCollectionName);
        }

        public async Task CreateAsync(InvoiceAuditEvent auditEvent)
        {
            await _collection.InsertOneAsync(auditEvent);
        }
    }
}