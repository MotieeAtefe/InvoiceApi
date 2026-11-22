using InvoiceApi.Audit.Enums;

namespace InvoiceApi.Audit.Model
{
    public class InvoiceAuditChange
    {
        public AuditEntityType EntityType { get; set; }
        public int EntityId { get; set; }

        public string Field { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
