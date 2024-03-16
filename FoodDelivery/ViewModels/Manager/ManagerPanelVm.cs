using FoodDelivery.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.ViewModels.Manager;

public class ManagerPanelVm
{
    public List<RestaurantVm> Restaurants { get; set; } = new();
    public List<Models.User> Users { get; set; } = new();
    public List<IdentityRole> Roles { get; set; } = new();
    public string? UserRole { get; set; }
    public string? UserId { get; set; }
    public UserManager<Models.User>? UserManager { get; set; }
    public List<FoodVm> Food { get; set; } = new();
}