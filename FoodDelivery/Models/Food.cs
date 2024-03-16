namespace FoodDelivery.Models;

public class Food
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public required string Description { get; set; }

    public Restaurant? Restaurant { get; set; }
    public int RestaurantId { get; set; }
    public List<Order> Orders { get; set; } = new();
}