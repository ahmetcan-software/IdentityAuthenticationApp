using Microsoft.AspNetCore.Identity;

namespace AspNetCoreMvc_IdentityAuthenticationApp.Identity.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
