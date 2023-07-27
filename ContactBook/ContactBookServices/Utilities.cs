
using ContactBookModel;
using ContactBookData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Services
{
    public class Utilities
    {
        private readonly BookDbContext _context;
        public Utilities(BookDbContext context)
        {
            _context = context;
        }

        public List<AppUser> GetAllUsers(PaginParameter usersParameter)
        {
            var contacts = _context.appUsers.OrderBy(on => on.FirstName)
                .Skip((usersParameter.PageNumber - 1) * usersParameter.PageSize)
                .Take(usersParameter.PageSize)
                .ToList();
            return contacts;
        }
    }
}
