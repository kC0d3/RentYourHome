namespace RentYourHome.Models.Images;

public class Image
{
    public int Id { get; set; }
    public byte[] ImageData { get; set; }
    public string FileName { get; set; }
}