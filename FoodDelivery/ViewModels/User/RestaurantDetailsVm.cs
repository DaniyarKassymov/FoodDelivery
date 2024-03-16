using FoodDelivery.ViewModels.Manager;

namespace FoodDelivery.ViewModels.User;

public class RestaurantDetailsVm
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required string Image { get; init; }
    public string? Description { get; init; }
    public required List<FoodVm> Food{ get; init; }
}