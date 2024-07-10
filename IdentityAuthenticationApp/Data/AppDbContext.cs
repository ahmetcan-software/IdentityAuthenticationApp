using AspNetCoreMvc_IdentityAuthenticationApp.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMvc_IdentityAuthenticationApp.Identity.ViewModels;

namespace AspNetCoreMvc_IdentityAuthenticationApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AspNetCoreMvc_IdentityAuthenticationApp.Identity.ViewModels.RegisterViewModel> RegisterViewModel { get; set; } = default!;
    }
}
