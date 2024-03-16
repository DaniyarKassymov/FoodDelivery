using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.ViewModels.Manager;

public class AddFoodVm
{
    public int Id { get; set; }
    [Required(ErrorMessage = "*Поле обязательно")]
    [Remote("CheckFoodTitle", "Validation", ErrorMessage = "*Блюдо с таким названием уже существует", AdditionalFields = nameof(RestaurantId))]
    public required string Title { get; set; }
    [Required(ErrorMessage = "*Поле обязательно")]
    public required decimal Price { get; set; }
    [Required(ErrorMessage = "*Поле обязательно")]
    public required string Description { get; set; }

    public List<RestaurantVm> RestaurantVms { get; set; } = new();
    public int RestaurantId { get; set; }
}