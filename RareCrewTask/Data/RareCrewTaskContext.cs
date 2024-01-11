using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RareCrewTask.Models;

namespace RareCrewTask.Data
{
    public class RareCrewTaskContext : DbContext
    {
        public RareCrewTaskContext (DbContextOptions<RareCrewTaskContext> options)
            : base(options)
        {
        }

        public DbSet<RareCrewTask.Models.Employee> Employee { get; set; } = default!;
    }
}
