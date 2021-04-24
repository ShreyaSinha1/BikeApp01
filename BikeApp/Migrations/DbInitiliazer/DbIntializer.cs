using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApp.Migrations.DbInitiliazer
{
    public class DbIntializer: IDbIntializer
    {
        private readonly VroomDbContext _context;
        private readonly UserManager<IdentityUser> _userManger;
        private readonly RoleManager<IdentityRole> roleManager;
        public void DbIntializer(VroomDbContext _context,UserManager<IdentityUser> _userManger, RoleManager<IdentityRole> roleManager)
        {
            _context = _context;
            _userManger = _userManger;
            roleManager = roleManager;
        }
        public void Initialize()
        {
            if (_context.Database.GetPendingMigrations().Count()>0)
            _context.Database.Migrate();
            if (_context.Roles.Any(roleManager => roleManager.Name == Helpers.Role.Admin)) return;
            roleManager.CreateAsync(new IdentityRole(Helpers.Role.Admin)).GetAwaiter().GetResult();
            _userManger.CreateAsync(
                new ApplicationUser { 
                UserName="Admin",
                Email="Admin@gmail.com",
                EmailConfirmed=true
                },
                "Admin@123").GetAwaiter().GetResult();
            await _userManger.AddToRoleAsync(
                await _userManger.FindByNameAsync("Admin"));

        }
    }
}
