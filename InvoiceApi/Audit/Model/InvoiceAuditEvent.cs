using InvoiceApi.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace InvoiceApi.Audit.Model
{
    public class InvoiceAuditEvent
    {
        public ObjectId Id { get; set; }

        public int InvoiceId { get; set; }
        public DateTime OccurredAt { get; set; }

        public int UserId { get; set; }

        public List<InvoiceAuditChange> Changes { get; set; }
            = new List<InvoiceAuditChange>();
    }
}