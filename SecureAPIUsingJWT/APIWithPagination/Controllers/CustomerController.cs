using APIWithPagination.Contexts;
using APIWithPagination.Filter;
using APIWithPagination.Helpers;
using APIWithPagination.Models;
using APIWithPagination.Services;
using APIWithPagination.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWithPagination.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService _uriService;

        public CustomerController(ApplicationDbContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        public IActionResult Index()
        {
            return Ok(_context.Customers.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.Where(a => a.Id == id).FirstOrDefaultAsync();
            return Ok(new Response<Customer>(customer));
        }

        // with pagination
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var pageData = await _context.Customers
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            var totalRecords = await _context.Customers.CountAsync();

            var pageResponse = PaginationHelper.CreatePagedResponse<Customer>(pageData, validFilter, totalRecords, _uriService, route);

            //return Ok(new PagedResponse<List<Customer>>(pageData, validFilter.PageNumber, validFilter.PageSize)); // For return paging value without link
            return Ok(pageResponse);
        }
    }
}
