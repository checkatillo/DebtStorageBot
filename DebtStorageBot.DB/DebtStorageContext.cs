using DebtStorageBot.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtStorageBot.DB
{
    public class DebtStorageContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<DebtLog> DebtLogs { get; set; }
        public DbSet<DebtSum> DebtSums { get; set; }
    }
}
