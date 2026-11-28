using InvoiceApi.Audit.Model;
using System.Threading.Tasks;

namespace InvoiceApi.Audit.Services
{
    public interface IInvoiceAuditService
    {
        public Task CreateAsync(InvoiceAuditEvent auditEvent);
    }
}
