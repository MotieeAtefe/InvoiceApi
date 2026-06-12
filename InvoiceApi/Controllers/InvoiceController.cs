using InvoiceApi.Dtos;
using InvoiceApi.Filters;
using InvoiceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApi.Controllers
{
    [ApiController]
    [MyAuthorize]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto dto)
        {
            var invoice = await _invoiceService.CreateAsync(dto);
            return Ok(invoice);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _invoiceService.GetById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            else return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoice = await _invoiceService.GetAll();
            if (invoice == null)
            {
                return NotFound();
            }
            else return Ok(invoice);
        }
        [HttpGet("{id}/print")]
        public async Task<IActionResult> Print(int id)
        {
            var invoice = await _invoiceService.GetById(id);

            if (invoice == null)
                return NotFound();

            var sb = new StringBuilder();

            sb.AppendLine($"Invoice #{invoice.Id}");
            sb.AppendLine($"Customer: {invoice.CustomerName}");
            sb.AppendLine($"Date: {invoice.Date}");
            sb.AppendLine("-----------------------------------");
            sb.AppendLine("Product | Qty | Price | Total");

            foreach (var item in invoice.Items)
            {
                sb.AppendLine($"{item.ProductName} | {item.Quantity} | {item.UnitPrice} | {item.TotalPrice}");
            }

            sb.AppendLine("-----------------------------------");
            sb.AppendLine($"Total Amount: {invoice.TotalAmount}");

            return Content(sb.ToString(), "text/plain");
        }

    }
}
