using FoodDelivery.Models;

namespace FoodDelivery.ViewModels.User;

public class OrderVm
{
    public int Id { get; init; }
    public long Counter { get; init; }
    
    public Food? Food { get; init; }
    public required int FoodId { get; set; }

    public Basket? Basket { get; set; }
    public required int BasketId { get; set; }
}