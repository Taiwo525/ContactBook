namespace StayCation.Models
{
    public class Property
    {
        public Property(string id, string name, string city, string location, string price, string description, string type, string popularity)
        {
            Id = id;
            Name = name;
            City = city;
            Location = location;
            Price = price;
            Description = description;
            Type = type;
            Popularity = popularity;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Popularity { get; set; }
    }
}
