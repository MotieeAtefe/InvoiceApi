using System;
using System.Collections.Generic;
using InvoiceApi.Models;

namespace InvoiceApi.Audit.Model
{
    public class InvoiceAuditEvent
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public DateTime OccurredAt { get; set; }

        public string UserId { get; set; }

        public List<InvoiceAuditChange> Changes { get; set; }
            = new List<InvoiceAuditChange>();
    }
}