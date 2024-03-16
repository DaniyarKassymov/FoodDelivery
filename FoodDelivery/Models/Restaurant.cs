namespace FoodDelivery.Models;

public class Restaurant
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Image { get; set; }
    public string? Description { get; set; }

    public List<Food> Foods { get; set; } = new();
    public List<Basket> Baskets { get; set; } = new();
}