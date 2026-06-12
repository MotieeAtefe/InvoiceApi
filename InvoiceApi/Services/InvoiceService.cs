using InvoiceApi.Data;
using InvoiceApi.Dtos;
using InvoiceApi.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _contextAccessor;
        public InvoiceService(AppDbContext appDbContext, IHttpContextAccessor contextAccessor)
        {
            _appDbContext = appDbContext;
            _contextAccessor = contextAccessor;
        }

        public async Task<int> CreateAsync(CreateInvoiceDto dto)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
            if (dto.Items == null || !dto.Items.Any())
            {
                throw new Exception("Invoice must have at least one item");
            }

            var invoice = new Invoice
            {
                CustomerName = dto.CustomerName,
                Date = DateTime.UtcNow,
                Items = new List<InvoiceItem>(),
                UserId= userId
            };

            foreach (var item in dto.Items)
            {

                var product = await _appDbContext.Products
                    .FirstOrDefaultAsync(P => P.Id == item.ProductId);

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

            invoice.TotalAmount = invoice.Items.Sum(i => i.Quantity * i.UnitPrice);
            _appDbContext.Invoices.Add(invoice);
            await _appDbContext.SaveChangesAsync();
            return invoice.Id;
        }


        public async Task<InvoiceDto> GetById(int id)
        
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
            return await _appDbContext.Invoices
                .Where(I => I.Id == id && I.UserId==userId)
                .Select(I => new InvoiceDto
                {
                    Id = I.Id,
                    CustomerName = I.CustomerName,
                    Date = I.Date,
                    TotalAmount = I.TotalAmount,
                    Items = I.Items.Select(ii => new InvoiceItemDto
                    {
                        InvoiceId = ii.InvoiceId,
                        ProductId = ii.ProductId,
                        ProductName = ii.Product.Name,
                        Quantity = ii.Quantity,
                        UnitPrice = ii.UnitPrice

                    }).ToList()
                }).FirstOrDefaultAsync();


        }
        public async Task<List<InvoiceDto>> GetAll()
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirst("UserId")?.Value);

            return await _appDbContext.Invoices
                .Where (I => I.UserId == userId)
              .Select(I => new InvoiceDto
              {
                  Id = I.Id,
                  CustomerName = I.CustomerName,
                  Date = I.Date,
                  TotalAmount = I.TotalAmount,
                  Items = I.Items.Select(ii => new InvoiceItemDto
                  {
                      InvoiceId = ii.InvoiceId,
                      ProductId = ii.ProductId,
                      ProductName = ii.Product.Name,
                      Quantity = ii.Quantity,
                      UnitPrice = ii.UnitPrice

                  }).ToList()
              }).ToListAsync();


        }

    }
}
