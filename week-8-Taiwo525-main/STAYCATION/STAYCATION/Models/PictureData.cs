namespace StayCation.Models
{
    public class PictureData
    {
        public string? ImageUrl { get; set; }
        public string? City { get; set; }
        public string? Location { get; set; }
        public string? Price { get; set; }
        public string? Description { get; set; }
        public string? Group { get; set; }
        public string? Popularity { get; set; }
        public string Ispopular { get; set; }

        public PictureData(string? imgUrl, string? city, string? location, string? price, string? description, string? group, string? popularity, string ispopular)
        {
            ImageUrl = imgUrl;
            City = city;
            Location = location;
            Price = price;
            Description = description;
            Group = group;
            Popularity = popularity;
            Ispopular = ispopular;
        }
        
    }
}
