using FoodDelivery.Models;
using FoodDelivery.ViewModels.Manager;

namespace FoodDelivery.Utilities.Mappers;

public static class ManagerMapper
{
    public static Restaurant AddRestaurantRestaurant(AddRestaurantVm vm, string image)
    {
        return new Restaurant
        {
            Title = vm.Title,
            Image = image,
            Description = vm.Description
        };
    }
    
    public static RestaurantVm RestaurantRestaurantVm(Restaurant restaurant)
    {
        return new RestaurantVm
        {
            Id = restaurant.Id,
            Title = restaurant.Title,
            Image = restaurant.Image,
            Description = restaurant.Description
        };
    }

    public static Food AddFoodVmFood(AddFoodVm vm)
    {
        return new Food
        {
            Title = vm.Title,
            Price = vm.Price,
            Description = vm.Description,
            RestaurantId = vm.RestaurantId
        };
    }

    public static FoodVm FoodFoodVm(Food food)
    {
        return new FoodVm
        {
            Id = food.Id,
            Title = food.Title,
            Price = food.Price,
            Description = food.Description,
            Restaurant = food.Restaurant.Title
        };
    }
}