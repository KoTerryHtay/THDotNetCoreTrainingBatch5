using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THDotNetTrainingBatch5.ConsoleApp.Models;

namespace THDotNetTrainingBatch5.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=LAPTOP-0NOHR6LI;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True";

                optionsBuilder.UseSqlServer(connectionString);
            }

        }

        public DbSet<BlogDataModel> Blog { get; set; }
    }
}
