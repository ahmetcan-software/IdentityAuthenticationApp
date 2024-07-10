using Microsoft.AspNetCore.Identity;

namespace AspNetCoreMvc_IdentityAuthenticationApp.Identity.Models
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
