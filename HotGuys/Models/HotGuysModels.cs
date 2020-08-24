using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotGuys.Models
{
    public class HotGuysModels : IdentityDbContext
    {
        public HotGuysModels()
           
        {

        }

 

        public virtual DbSet<HotChoiceViewModels> HotChoiceViewModels { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}