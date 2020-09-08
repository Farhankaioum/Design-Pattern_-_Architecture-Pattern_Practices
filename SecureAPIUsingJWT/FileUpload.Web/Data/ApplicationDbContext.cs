using System;
using System.Collections.Generic;
using System.Text;
using FileUpload.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileUpload.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FileOnFileSystemModel> FileOnFileSystemModels { get; set; }
        public DbSet<FileOnDatabaseModel> FileOnDatabaseModels { get; set; }
    }
}
