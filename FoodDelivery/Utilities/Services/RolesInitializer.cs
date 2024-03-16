using FoodDelivery.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Utilities.Services;

public class RolesInitializer
{
    private const string ManagerUserName = "manager";
    private const string ManagerEmail = "manager@manager.com";
    
    public static async Task Initializer(
        RoleManager<IdentityRole> roleManager,
        UserManager<User> userManager)
    {
        var roles = new[] { "user", "admin" , "manager"};

        foreach (var role in roles)
            if (await roleManager.FindByNameAsync(role) is null)
                await roleManager.CreateAsync(new IdentityRole(role));

        if (await userManager.FindByNameAsync(ManagerUserName) is null)
        {
            var manager = new User { UserName = ManagerUserName, Email = ManagerEmail };
            var createManagerResult = await userManager.CreateAsync(manager, ManagerUserName);
            if (createManagerResult.Succeeded)
                await userManager.AddToRoleAsync(manager, "manager");
        }
    }
}