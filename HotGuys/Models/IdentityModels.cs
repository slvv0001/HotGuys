using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotGuys.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        // @author Lu Chen
        // mapping to hotChoices and Comments
        public virtual ICollection<HotChoiceViewModels> HotChoices { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        // @author Lu Chen
        // Add Comments, HotChoices and ApplicationUser model to dbContext
        public System.Data.Entity.DbSet<HotGuys.Models.Comments> Comments { get; set; }

        public System.Data.Entity.DbSet<HotGuys.Models.HotChoiceViewModels> HotChoiceViewModels { get; set; }

        public System.Data.Entity.DbSet<HotGuys.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}