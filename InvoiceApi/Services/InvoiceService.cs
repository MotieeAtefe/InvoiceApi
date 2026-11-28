#nullable enable

using InvoiceApi.Audit.Enums;
using InvoiceApi.Audit.Model;
using InvoiceApi.Audit.Services;
using InvoiceApi.Data;
using InvoiceApi.Dtos;
using InvoiceApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUser _currentUser;
        private readonly IInvoiceAuditService _auditChange;

        public InvoiceService(
            AppDbContext appDbContext,
            ICurrentUser currentUser,
            IInvoiceAuditService auditChange)
        {
            _appDbContext = appDbContext;
            _currentUser = currentUser;
            _auditChange = auditChange;
        }

        public async Task<int> CreateAsync(CreateInvoiceDto dto)
        {
            var userId = _currentUser.GetUserId();

            if (dto.Items == null || !dto.Items.Any())
            {
                throw new Exception("Invoice must have at least one item");
            }

            var invoice = new Invoice
            {
                CustomerName = dto.CustomerName,
                Date = DateTime.UtcNow,
                Items = new List<InvoiceItem>(),
                UserId = userId
            };

            foreach (var item in dto.Items)
            {
                var product = await _appDbContext.Products
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (product == null)
                {
                    throw new Exception($"Product {item.ProductId} is not found!");
                }

                invoice.Items.Add(new InvoiceItem
                {
                    ProductId = item.ProductId,
                    UnitPrice = product.Price,
                    Quantity = item.Quantity
                });
            }

            invoice.TotalAmount = invoice.Items.Sum(
                i => i.Quantity * i.UnitPrice);

            _appDbContext.Invoices.Add(invoice);
            await _appDbContext.SaveChangesAsync();

            var changes = new List<InvoiceAuditChange>
            {
                new InvoiceAuditChange
                {
                    EntityType = AuditEntityType.Invoice,
                    EntityId = invoice.Id,
                    Field = "CustomerName",
                    OldValue = null,
                    NewValue = invoice.CustomerName
                },
                new InvoiceAuditChange
                {
                    EntityType = AuditEntityType.Invoice,
                    EntityId = invoice.Id,
                    Field = "TotalAmount",
                    OldValue = null,
                    NewValue = invoice.TotalAmount
                }
            };

            foreach (var item in invoice.Items)
            {
                changes.Add(new InvoiceAuditChange
                {
                    EntityType = AuditEntityType.InvoiceItem,
                    EntityId = item.Id,
                    Field = "ProductId",
                    OldValue = null,
                    NewValue = item.ProductId
                });

                changes.Add(new InvoiceAuditChange
                {
                    EntityType = AuditEntityType.InvoiceItem,
                    EntityId = item.Id,
                    Field = "Quantity",
                    OldValue = null,
                    NewValue = item.Quantity
                });

                changes.Add(new InvoiceAuditChange
                {
                    EntityType = AuditEntityType.InvoiceItem,
                    EntityId = item.Id,
                    Field = "UnitPrice",
                    OldValue = null,
                    NewValue = item.UnitPrice
                });
            }

            var auditEvent = new InvoiceAuditEvent
            {
                InvoiceId = invoice.Id,
                UserId = userId,
                OccurredAt = DateTime.UtcNow,
                Changes = changes
            };

            await _auditChange.CreateAsync(auditEvent);

            return invoice.Id;
        }

        public async Task<InvoiceDto?> GetById(int id)
        {
            var userId = _currentUser.GetUserId();

            return await _appDbContext.Invoices
                .Where(i => i.Id == id && i.UserId == userId)
                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    CustomerName = i.CustomerName,
                    Date = i.Date,
                    TotalAmount = i.TotalAmount,
                    Items = i.Items.Select(ii => new InvoiceItemDto
                    {
                        InvoiceId = ii.InvoiceId,
                        ProductId = ii.ProductId,
                        ProductName = ii.Product.Name,
                        Quantity = ii.Quantity,
                        UnitPrice = ii.UnitPrice
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<InvoiceDto>> GetAll()
        {
            var userId = _currentUser.GetUserId();

            return await _appDbContext.Invoices
                .Where(i => i.UserId == userId)
                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    CustomerName = i.CustomerName,
                    Date = i.Date,
                    TotalAmount = i.TotalAmount,
                    Items = i.Items.Select(ii => new InvoiceItemDto
                    {
                        InvoiceId = ii.InvoiceId,
                        ProductId = ii.ProductId,
                        ProductName = ii.Product.Name,
                        Quantity = ii.Quantity,
                        UnitPrice = ii.UnitPrice
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}