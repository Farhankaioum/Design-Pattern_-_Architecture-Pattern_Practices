using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Select2.WebApplication.Models;

namespace Select2.WebApplication.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options)
            :base(options)
        {

        }
        public DbSet<Customer> Customer { get; set; }
    }
}
