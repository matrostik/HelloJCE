using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HelloJCE.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
        public bool IsConfirmed { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }

    //public class CustomUserManager<T> : UserManager<T> where T : IUser
    //{
    //    public override Task<T> FindAsync(string userName, string password)
    //    {
    //        return base.FindAsync(userName, password);
    //    }
    //}
}