using ContactBook.Model.DTOs;
using ContactBook.Services;

namespace ContactBook.Repository
{
    public interface IRepository
    {
        IEnumerable<AppUserDto> GetAll(PaginParameter usersParameter);
        Task<AppUserDto> GetByIdAsync(string Id);
        Task<AppUserDto> GetByEmailAsync(string email);
        Task<bool> CreateUserAsync(AppUserDto appUser);
        Task<bool> DeleteByIdAsync(string Id);
        Task<bool> AddUserPhoto(string userId, PhotoToAddDto model);
        Task<List<AppUserDto>> SearchUsersAsync(string term);
    }
}
