using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Synthetic_Seismogram.Models;

namespace Synthetic_Seismogram.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<ReflectiveCoefficient> ReflectiveCoefficients { get; set; }
        public DbSet<AcousticImpedance> AcousticImpedances { get; set; }
        public DbSet<Synthetic> Synthetics { get; set; }
    }
}
