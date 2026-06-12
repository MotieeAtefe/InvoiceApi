using InvoiceApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public interface IInvoiceService
    {
        Task<int> CreateAsync(CreateInvoiceDto dto);    
        Task<InvoiceDto?> GetById(int id);
        Task<List<InvoiceDto>> GetAll();

    }
}
