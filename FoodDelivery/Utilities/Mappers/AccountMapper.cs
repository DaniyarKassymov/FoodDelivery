using FoodDelivery.Models;
using FoodDelivery.ViewModels.Account;

namespace FoodDelivery.Utilities.Mappers;

public static class AccountMapper
{
    public static User RegistrationVmUser(RegistrationVm vm)
    {
        return new User
        {
            UserName = vm.UserName,
            Email = vm.Email
        };
    } 
}