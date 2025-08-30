using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie1.Models;

namespace MvcMovie1.Data
{
    public class MvcMovie1Context : DbContext
    {
        public MvcMovie1Context (DbContextOptions<MvcMovie1Context> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie1.Models.Product> Product { get; set; } = default!;
    }
}
