using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.ViewModels.Manager;

public class AddRestaurantVm
{
    [Required(ErrorMessage = "*Поле обязательно")]
    [Remote("CheckRestaurantTitle", "Validation", ErrorMessage = "*Заведение с такмим названием уже существуе")]
    public required string Title { get; set; }
    [Required(ErrorMessage = "*Поле обязательно")]
    [DataType(DataType.Upload)]
    public required IFormFile Image { get; set; }
    public string? Description { get; set; }
}