namespace FoodDelivery.Models;

public class Basket
{
    public int Id { get; set; }

    public User? User { get; set; }
    public required string UserId { get; set; }

    public Restaurant? Restaurant { get; set; }
    public required int RestaurantId { get; set; }

    public List<Order> Orders { get; set; } = new();
}