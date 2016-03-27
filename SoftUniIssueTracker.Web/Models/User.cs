using Microsoft.AspNet.Identity.EntityFramework;
using SIT.Data.Interfaces;

namespace SIT.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser, IDentificatable<string>
    {
    }
}
