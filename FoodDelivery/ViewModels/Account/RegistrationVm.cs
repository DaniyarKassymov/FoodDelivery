using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.ViewModels.Account;

public class RegistrationVm
{
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    [Remote("CheckUserName", "Validation", ErrorMessage = "*Пользователь с таким логином уже существует")]
    public required string UserName { get; set; }
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    [EmailAddress(ErrorMessage = "*Вы ввели адрес в неверном формате")]
    [Remote("CheckEmail", "Validation", ErrorMessage = "*Пользователь с таким эл. адресом уже существует")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "*Пароли не совпадают")]
    public required string ConfirmPassword { get; set; }
}