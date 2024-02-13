using RentYourHome.Models.Ads;

namespace RentYourHome.Models.Images;

public class Image
{
    public int Id { get; set; }
    public string ImageName { get; set; }
    
    public Ad Ad { get; init; }
    public int AdId { get; init; }
}