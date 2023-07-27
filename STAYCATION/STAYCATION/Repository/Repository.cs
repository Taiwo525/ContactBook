using StayCation.Models;
using STAYCATION.Models;

namespace StayCation.Repository
{
    public class Repository : IRepository
    {
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
                            string Name = fields[2].Trim();
                            string email = fields[3].Trim();
                            string password = fields[4].Trim();
                            DateTime date = DateTime.Parse(fields[5].Trim());


                            Customers customer = new Customers(id, Name, email, password, date);
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;
        }
    }
}
