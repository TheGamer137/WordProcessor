using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Domain.Entities;

namespace WordProcessor.Infrastructure
{
    public class WordProcessorContext:DbContext
    {
        public DbSet<Dictionary> Dictionaries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("MS SQLServerConnectionString");
        }
    }
}
