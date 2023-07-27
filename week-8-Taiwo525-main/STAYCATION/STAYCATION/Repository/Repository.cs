using StayCation.Models;
using STAYCATION.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.IO;
namespace StayCation.Repository
{
    public class Repository : IRepository
    {
        public readonly IConfiguration _configuration;
        public Repository(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public List<Customers> ReadCustomersFromFile(string filePath)
        {
            List<Customers> customers = new List<Customers>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] fields = line.Split('|');

                        if (fields.Length >= 5)
                        {
                            Guid id = Guid.Parse(fields[1].Trim());
                            string fullname = fields[2].Trim();
                            string email = fields[3].Trim();
                            string password = fields[4].Trim();
                            DateTime date = DateTime.Parse(fields[5].Trim());


                            Customers customer = new Customers(id, fullname, email, password, date);
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;
        }

        public List<PictureData> GetHotels()
        {
            string connectString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new(connectString))
            {
                string query = "SELECT * FROM PictureDb";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<PictureData> hotels = new List<PictureData>();

                        while (reader.Read())
                        {
                            string hotelImageUrl = reader.GetString(reader.GetOrdinal("hotelImageUrl"));
                            string hotelName = reader.GetString(reader.GetOrdinal("hotelName"));
                            string hotelLocation = reader.GetString(reader.GetOrdinal("hotelLocation"));
                            string hotelPrice = reader.GetString(reader.GetOrdinal("hotelPrice"));
                            string hotelGroup = reader.GetString(reader.GetOrdinal("hotelGroup"));
                            string hotelDescription = reader.GetString(reader.GetOrdinal("hotelDescription"));
                            string hotelPopularity = reader.GetString(reader.GetOrdinal("hotelPopularity"));
                            string isPopular = reader.GetString(reader.GetOrdinal("isPopular"));

                            var hotel = new PictureData(hotelImageUrl, hotelName, hotelLocation, hotelPrice, hotelDescription, hotelGroup, hotelPopularity, isPopular);

                            hotels.Add(hotel);
                        }

                        return hotels;
                    }
                }
            }
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
