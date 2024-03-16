namespace FoodDelivery.ViewModels.Manager;

public class FoodVm
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required decimal Price { get; init; }
    public required string Description { get; init; }

    public required string Restaurant { get; init; }
    public int RestaurantId { get; set; }
}