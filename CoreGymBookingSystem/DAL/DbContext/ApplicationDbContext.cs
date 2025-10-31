using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContext
{
    public partial class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            builder.Entity<Session>().HasOne( e => e.Instructor );
            builder.Entity<Session>()
                .HasMany( e => e.Bookings )
                .WithMany( e => e.Bookings );

            base.OnModelCreating( builder );
        }
    }
}
