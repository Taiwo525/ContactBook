using StayCation.Models;
using STAYCATION.Models;

namespace StayCation.Repository
{
    public interface IRepository
    {
        List<Customers> ReadCustomersFromFile(string filePath);
    }
}
