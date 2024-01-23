using LandSpace.Models;
using LandSpace.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LandSpace.DAL
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _conf;
        private readonly UserManager<AppUser> _userManager;

        public AppDbContextInitializer(AppDbContext context, RoleManager<IdentityRole> roleManager, IConfiguration conf, UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _conf = conf;
            _userManager = userManager;
        }

        public async Task MigrationAsync()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task CreateRoleAsync()
        {
            foreach (var role in Enum.GetNames(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole { Name = role });
            }
        }

        public async Task CreateAdminAsync()
        {
            AppUser admin = new AppUser
            {
                Name = "admin",
                Surname = "admin",
                UserName = _conf["AdminSettings:UserName"],
                Email = _conf["AdminSettings:Email"]
            };
            await _userManager.CreateAsync(admin, _conf["AdminSettings:Password"]);
            await _userManager.AddToRoleAsync(admin, UserRoles.Admin.ToString());
        }

    }
}
