using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class UpdateUser
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Bio { get; set; }

    public string MainVideo { get; set; }

    public string AvatarUrl { get; set; }

    // Поточний пароль для підтвердження зміни паролю
    public string CurrentPassword { get; set; }

    // Новий пароль, якщо користувач хоче змінити пароль
    public string NewPassword { get; set; }
}