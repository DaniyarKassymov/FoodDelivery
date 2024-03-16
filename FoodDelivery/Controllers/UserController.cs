using System.ComponentModel.DataAnnotations;
using FoodDelivery.Database;
using FoodDelivery.Models;
using FoodDelivery.Utilities.Mappers;
using FoodDelivery.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Controllers;

public class UserController : Controller
{
    private readonly DatabaseContext _db;
    private readonly UserManager<User> _userManager;

    public UserController(DatabaseContext db, UserManager<User> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize]
    [Display(Name = "AllRestaurants")]
    public async Task<IActionResult> AllRestaurantsAsync()
    {
        var restaurants = await _db.Restaurants.ToListAsync();
        var vm = restaurants.Select(ManagerMapper.RestaurantRestaurantVm).ToList();
        
        return View(vm);
    }

    [HttpGet]
    [Authorize]
    [Display(Name = "RestaurantDetails")]
    public async Task<IActionResult> RestaurantDetailsAsync(int id)
    {
        var restaurant = await _db.Restaurants
            .Include(r => r.Foods)
            .FirstOrDefaultAsync(r => r.Id == id);
        var restaurantDetailsVm = UserMapper.RestaurantRestaurantDetailsVm(restaurant);

        var orders = await _db.Orders
            .Where(o => o.Basket.RestaurantId == id 
                        && o.Basket.UserId == _userManager.GetUserId(User))
            .ToListAsync();
        var ordersVm = orders.Select(UserMapper.OrderOrderVm).ToList();

        var vm = new DetailsOrdersVm
        {
            RestaurantDetailsVm = restaurantDetailsVm,
            OrderVms = ordersVm
        };
            
        return View(vm);
    }

    [HttpGet]
    [Authorize]
    [Display(Name = "AddToOrder")]
    public async Task<IActionResult> AddToOrderAsync(int foodId, int restaurantId)
    {
        if (!_db.Baskets.Any(b => b.UserId == _userManager.GetUserId(User) &&
                                 b.RestaurantId == restaurantId))
        {
            var basket = new Basket
            {
                UserId = _userManager.GetUserId(User),
                RestaurantId = restaurantId
            };
            
            await _db.Baskets.AddAsync(basket);
            await _db.SaveChangesAsync();
        }

        var foundedBasket = await _db.Baskets.FirstOrDefaultAsync(b => b.UserId == _userManager.GetUserId(User) &&
                                                                             b.RestaurantId == restaurantId);
        var foundedOrder = await _db.Orders.FirstOrDefaultAsync(o => o.BasketId == foundedBasket.Id &&
                                                                            o.FoodId == foodId);

        if (foundedOrder is null)
        {
            var order = new Order
            {
                FoodId = foodId,
                BasketId = foundedBasket.Id,
                Counter = 1
            };
            
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
        }
        else
        {
            foundedOrder.Counter++;
            await _db.SaveChangesAsync();
        }
        
        var orders = await _db.Orders
            .Where(o => o.Basket.RestaurantId == restaurantId
                        && o.Basket.UserId == _userManager.GetUserId(User))
            .Include(o => o.Food)
            .ToListAsync();
        var ordersVm = orders.Select(UserMapper.OrderOrderVm).ToList();
        
        return PartialView("Order", ordersVm);
    }

    [HttpGet]
    [Authorize]
    [Display(Name = "DeleteFromOrder")]
    public async Task<IActionResult> DeleteFromOrderAsync(int orderId, int restaurantId)
    {
        var order = await _db.Orders.FindAsync(orderId);

        if (order is null) return NotFound();
        
        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();
            
        var orders = await _db.Orders
            .Where(o => o.Basket.RestaurantId == restaurantId
                        && o.Basket.UserId == _userManager.GetUserId(User))
            .Include(o => o.Food)
            .ToListAsync();
        var ordersVm = orders.Select(UserMapper.OrderOrderVm).ToList();
        ViewData["RestaurantId"] = restaurantId;

        return PartialView("Order", ordersVm);

    }
}