using System.ComponentModel.DataAnnotations;

namespace RentYourHome.Contracts;

public record RegistrationRequest(
    [Required]string Email, 
    [Required]string Username, 
    [Required]string FirstName, 
    [Required]string LastName, 
    [Required]string Password);