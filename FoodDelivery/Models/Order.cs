namespace FoodDelivery.Models;

public class Order
{
    public int Id { get; set; }
    public long Counter { get; set; }
    
    public Food? Food { get; set; }
    public required int FoodId { get; set; }

    public Basket? Basket { get; set; }
    public required int BasketId { get; set; }
}