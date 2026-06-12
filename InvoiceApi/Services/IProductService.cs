using InvoiceApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<ProductDto> UpdateAsync(int id, CreateProductDto dto);
        Task<ProductDto> DeleteAsync(int id);
    }
}
