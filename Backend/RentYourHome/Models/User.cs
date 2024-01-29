namespace RentYourHome.Models;

public class User
{
    public int Id { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public Address Address { get; init; }
    
    public ICollection<Ad> PublishedAds { get; init; }
    public ICollection<UserAdApply> AppliedAds { get; init; }
}