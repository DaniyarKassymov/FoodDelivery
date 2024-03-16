using FoodDelivery.Models;
using FoodDelivery.ViewModels.User;

namespace FoodDelivery.Utilities.Mappers;

public static class UserMapper
{
    public static RestaurantDetailsVm RestaurantRestaurantDetailsVm(Restaurant restaurant)
    {
        return new RestaurantDetailsVm
        {
            Id = restaurant.Id,
            Title = restaurant.Title,
            Image = restaurant.Image,
            Description = restaurant.Description,
            Food = restaurant.Foods.Select(ManagerMapper.FoodFoodVm).ToList()
        };
    }

    public static OrderVm OrderOrderVm(Order order)
    {
        return new OrderVm
        {
            Id = order.Id,
            Counter = order.Counter,
            Food = order.Food,
            FoodId = order.FoodId,
            Basket = order.Basket,
            BasketId = order.BasketId
        };
    }
}