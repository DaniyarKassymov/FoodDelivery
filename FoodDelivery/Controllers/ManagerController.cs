using System.ComponentModel.DataAnnotations;
using FoodDelivery.Database;
using FoodDelivery.Models;
using FoodDelivery.Utilities.Mappers;
using FoodDelivery.Utilities.Services;
using FoodDelivery.ViewModels.Account;
using FoodDelivery.ViewModels.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Controllers;

public class ManagerController : Controller
{
    private readonly DatabaseContext _db;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ManagerController(DatabaseContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    [Authorize(Roles = "manager")]
    [Display(Name = "ManagerPanel")] 
    public async Task<IActionResult> ManagerPanelAsync()
    {
        var restaurants = await _db.Restaurants.ToListAsync();
        var restaurantsVms = restaurants.Select(ManagerMapper.RestaurantRestaurantVm).ToList();
        var users = await _db.Users.ToListAsync();
        var roles = await _roleManager.Roles.ToListAsync();
        var food = await _db.Foods
            .Include(f => f.Restaurant)
            .ToListAsync();
        var foodVms = food.Select(ManagerMapper.FoodFoodVm).ToList();

        var vm = new ManagerPanelVm
        {
            Restaurants = restaurantsVms,
            Users = users,
            Roles = roles,
            UserManager = _userManager,
            Food = foodVms
        };
        
        return View(vm);
    }

    [HttpGet]
    [Authorize(Roles = "manager")]
    public IActionResult AddRestaurant()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "manager")]
    [ValidateAntiForgeryToken]
    [Display(Name = "AddRestaurant")]
    public async Task<IActionResult> AddRestaurantAsync(AddRestaurantVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        
        var image = await FileUpload.Upload(vm.Title, vm.Image);
        var restaurant = ManagerMapper.AddRestaurantRestaurant(vm, image);

        await _db.Restaurants.AddAsync(restaurant);
        await _db.SaveChangesAsync();

        return RedirectToAction("ManagerPanel", "Manager");
    }

    [HttpGet]
    [Authorize(Roles = "manager")]
    [Display(Name = "DeleteRestaurant")]
    public async Task<IActionResult> DeleteRestaurantAsync(int id)
    {
        var restaurant = await _db.Restaurants.FindAsync(id);

        if (restaurant is null) return NotFound();
        
        _db.Restaurants.Remove(restaurant);
        await _db.SaveChangesAsync();
            
        return RedirectToAction("ManagerPanel");
    }

    [HttpGet]
    [Authorize(Roles = "manager")]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "manager")]
    [ValidateAntiForgeryToken]
    [Display(Name = "AddUser")]
    public async Task<IActionResult> AddUserAsync(RegistrationVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        
        var user = AccountMapper.RegistrationVmUser(vm);
        var userCreateResult = await _userManager.CreateAsync(user, vm.Password);

        if (userCreateResult.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "user");

            return RedirectToAction("ManagerPanel", "Manager");
        }

        foreach (var error in userCreateResult.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(vm);
    }
    
    [HttpGet]
    [Authorize(Roles = "manager")]
    [Display(Name = "DeleteUser")]
    public async Task<IActionResult> DeleteUserAsync(string id)
    {
        var user = await _db.Users.FindAsync(id);

        if (user is null) return NotFound();
        
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
            
        return RedirectToAction("ManagerPanel");
    }

    [HttpPost]
    [Authorize(Roles = "manager")]
    [ValidateAntiForgeryToken]
    [Display(Name = "ChangeUserRole")]
    public async Task<IActionResult> ChangeUserRoleAsync(ManagerPanelVm? vm)
    {
        if (vm.UserRole is null) return RedirectToAction("ManagerPanel");
        
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == vm.UserId);
        
        if (await _userManager.IsInRoleAsync(user, vm.UserRole)) return NotFound();
        
        await _userManager.AddToRoleAsync(user, vm.UserRole);

        return RedirectToAction("ManagerPanel");
    }
    
    [HttpGet]
    [Authorize(Roles = "manager,admin")]
    public IActionResult AddFood()
    {
        var vm = new AddFoodVm
        {
            Title = null,
            Price = 0,
            Description = null,
            RestaurantVms = _db.Restaurants.Select(ManagerMapper.RestaurantRestaurantVm).ToList()
        };

        return View(vm);
    }
    
    [HttpPost]
    [Authorize(Roles = "manager, admin")]
    [ValidateAntiForgeryToken]
    [Display(Name = "AddFood")]
    public async Task<IActionResult> AddFoodAsync(AddFoodVm vm)
    {
        if (!ModelState.IsValid) return NotFound();
        
        var food = ManagerMapper.AddFoodVmFood(vm);

        await _db.Foods.AddAsync(food);
        await _db.SaveChangesAsync();

        return RedirectToAction("AllRestaurants", "User");
    }
}