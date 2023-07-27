using ContactBookData;
using ContactBookModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Data.InitializedData
{
    public class UserandRolesInitializedData
    {
        public static async Task SeedData(BookDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            if(userManager.FindByEmailAsync("thebta52@gmail.com").Result == null)
            {
                AppUser user = new AppUser
                {
                    FirstName = "Taiwo",
                    LastName = "Ayoth",
                    Email = "thebta52@gmail.com",
                    ImgUrl = "openForCorrection",
                    FaceBookUrl = "facebookurl",
                    TwitterUrl = "twitterurl",
                    UserName = "thebta52@gmail.com",
                    PhoneNumber = "2348150953200",
                    city = "Ibadan",
                    State = "Oyo",
                    country = "Nigeria"
                };
                IdentityResult result = userManager.CreateAsync(user, "ysb123@32").Result;
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
            if(userManager.FindByEmailAsync("ayoth52@gmail.com").Result == null)
            {
                AppUser user = new AppUser
                {
                    FirstName = "Tomiwa",
                    LastName = "David",
                    Email = "ayoth52@gmail.com",
                    ImgUrl = "openForCorrection",
                    FaceBookUrl = "faceBookurl",
                    TwitterUrl = "twitterurl",
                    PhoneNumber = "+2348144760462",
                    city = "Akure",
                    State = "Ondo",
                    country = "Nigeria"
                };
                IdentityResult result = userManager.CreateAsync(user, "ysb123@32").Result;
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {

            if(roleManager.RoleExistsAsync("Admin").Result == false)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };

                await roleManager.CreateAsync(role);
            }
        }

        public static object SeedData(object context, object userManager, object roleManager)
        {
            throw new NotImplementedException();
        }
    }
}
