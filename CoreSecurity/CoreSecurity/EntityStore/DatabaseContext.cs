using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreSecurity.EntityStore
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<AuditTb> AuditTb { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<StockTb> StockTb { get; set; }
    }
}
