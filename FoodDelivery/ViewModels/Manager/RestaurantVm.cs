namespace FoodDelivery.ViewModels.Manager;

public class RestaurantVm
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public string? Image { get; init; }
    public string? Description { get; init; }
}