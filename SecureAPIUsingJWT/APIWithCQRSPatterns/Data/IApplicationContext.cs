using APIWithCQRSPatterns.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APIWithCQRSPatterns.Data
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }

        Task<int> SaveChanges();
    }
}