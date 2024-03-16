namespace FoodDelivery.ViewModels.User;

public class DetailsOrdersVm
{
    public required RestaurantDetailsVm RestaurantDetailsVm { get; set; }
    public List<OrderVm> OrderVms { get; set; } = new();
}