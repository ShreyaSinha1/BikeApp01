using BikeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApp.AppContext
{
    public class VroomDbContext:IdentityDbContext<IdentityUser>
    {
        public VroomDbContext(DbContextOptions<VroomDbContext> options):base(options)
        {

        }

        public DbSet<Make> Make { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}
