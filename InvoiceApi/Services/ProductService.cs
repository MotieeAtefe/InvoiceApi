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
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;
        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var Product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
            };
            _appDbContext.Products.Add(Product);
            await _appDbContext.SaveChangesAsync();
            return new ProductDto
            {
                Id = Product.Id,
                Name = Product.Name,
                Price = Product.Price

            };
        }

        public async Task<ProductDto> DeleteAsync(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null)
                return null;
              _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            return await _appDbContext.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                }).ToListAsync();

        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return await _appDbContext.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                }).FirstOrDefaultAsync();
        }

        public async Task<ProductDto> UpdateAsync(int id, CreateProductDto dto)
        {
            var Product = await _appDbContext.Products.FindAsync(id);
            if (Product == null)
            {
                return null;
            }
            Product.Name = dto.Name;
            Product.Price = dto.Price;

            await _appDbContext.SaveChangesAsync();

            return new ProductDto
            {
                Id = Product.Id,
                Name = Product.Name,
                Price = Product.Price,
            };
        }
    }
}
