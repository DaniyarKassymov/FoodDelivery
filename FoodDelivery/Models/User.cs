using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Models;

public class User : IdentityUser
{
    public List<Basket> Baskets { get; set; } = new();
}