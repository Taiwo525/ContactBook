using Microsoft.AspNetCore.Mvc;
using StayCation.Models;
using System.Diagnostics;

namespace StayCation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Property> Allproperties = ReadPropertiesFromFile("Allproperties.txt");
            var mostpicks = Allproperties.Where(prop => prop.Popularity == "Most Picks").ToList();
           
            ViewData["mostpicks"] = mostpicks;
         

            //var Allproperties = ReadPropertiesFromFile("Database.txt");
            var housewithbeautifulbackyard = Allproperties.Where(prop => prop.Type == "Houses with beautiful Backyards").ToList();
            ViewData["backyards"] = housewithbeautifulbackyard;



            var Largelivingroom = Allproperties.Where(prop => prop.Type == "Hotels with large living rooms").ToList();
            ViewData["livingRooms"] = Largelivingroom;



            var Kitchenset = Allproperties.Where(prop => prop.Type == "Apartments with Kitchen set").ToList();
            ViewData["withKitchen"] = Kitchenset;


            return View();
        }

        public static List<Property> ReadPropertiesFromFile(string filePath)
        {
            List<Property> properties = new();

            using (StreamReader reader = new(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] fields = line.Split('|');

                        if (fields.Length >= 8)
                        {
                            string id = fields[1].Trim();
                            string name = fields[2].Trim();
                            string city = fields[3].Trim();
                            string location = fields[4].Trim();
                            string price = fields[5].Trim();
                            string description = fields[6].Trim();
                            string type = fields[7].Trim();
                            string popularity = fields[8].Trim();

                            Property property = new(id, name, city, location, price, description, type, popularity);
                            properties.Add(property);
                        }
                    }
                }
            }

            return properties;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}