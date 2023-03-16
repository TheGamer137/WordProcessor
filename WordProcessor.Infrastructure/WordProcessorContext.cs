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
        public WordProcessorContext(DbContextOptions<WordProcessorContext> options) : base(options) { }
        public DbSet<Dictionary> Dictionaries { get; set; }
    }
}
