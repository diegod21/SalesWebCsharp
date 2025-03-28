using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context : IdentityDbContext<ApplicationUser>
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.Departament> Departament { get; set; } = default!;
        public DbSet<WebApplication1.Models.Seller> Seller { get; set; }
        public DbSet<WebApplication1.Models.SalesRecord> SalesRecord { get; set; }
    }
}
