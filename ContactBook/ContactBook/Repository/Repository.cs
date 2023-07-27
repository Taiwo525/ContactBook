using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using ContactBookData;
using ContactBook.Model.DTOs;
using ContactBookModel;
using ContactBook.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ContactBook.Repository
{
    public class Repository : IRepository
    {
        private readonly BookDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly Cloudinary _cloudinary;

        public Repository(BookDbContext context, UserManager<AppUser> userManager, Cloudinary cloudinary)
        {
            _context = context;
            _userManager = userManager;
            _cloudinary = cloudinary;
        }
        public IEnumerable<AppUserDto> GetAll(PaginParameter usersParameter)
        {
            var utility = new Utilities(_context);
            var data = new List<AppUserDto>();
            foreach (var userdata in utility.GetAllUsers(usersParameter))
            {
                data.Add(new AppUserDto
                {
                    FirstName = userdata.FirstName,
                    LastName = userdata.LastName,
                    Email = userdata.Email,
                    PhoneNunmber = userdata.PhoneNumber,
                    ImgUrl = userdata.ImgUrl,
                    FaceBookUrl = userdata.FaceBookUrl,
                    TwitterUrl = userdata.TwitterUrl,
                    City = userdata.city,
                    State = userdata.State,
                    Country = userdata.country
                });
            }
            return data;
        }


        public async Task<AppUserDto> GetByIdAsync(string Id)
        {
            var result = await _context.appUsers.FindAsync(Id);
            if (result == null)
            {
                return null;
            }

            var data = new AppUserDto
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNunmber = result.PhoneNumber,
                ImgUrl = result.ImgUrl,
                FaceBookUrl = result.FaceBookUrl,
                TwitterUrl = result.TwitterUrl,
                City = result.city,
                State = result.State,
                Country = result.country
            };
            return data;
        }

        public async Task<AppUserDto> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var data = new AppUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNunmber = user.PhoneNumber,
                ImgUrl = user.ImgUrl,
                FaceBookUrl = user.FaceBookUrl,
                TwitterUrl = user.TwitterUrl,
                City = user.city,
                State = user.State,
                Country = user.country
            };
            return data;
        }

        public async Task<bool> CreateUserAsync(AppUserDto appUser)
        {
            var user = await _userManager.FindByEmailAsync(appUser.Email);
            if (user == null)
            {
                var data = new AppUser()
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    UserName = appUser.Email,
                    Email = appUser.Email,
                    ImgUrl = appUser.ImgUrl,
                    PhoneNumber = appUser.PhoneNunmber,
                    FaceBookUrl = appUser.FaceBookUrl,
                    TwitterUrl = appUser.TwitterUrl,
                    city = appUser.City,
                    State = appUser.State,
                    country = appUser.Country
                };

                var res = await _userManager.CreateAsync(data, appUser.Password);
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(data, "Regular");
                    return true;
                }
            }
            return false;
        }


        public async Task<bool> DeleteByIdAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            { return false; }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                // Handle deletion error if needed
                return false;
            }

            _context.SaveChanges();
            return true;
        }

        //
        public async Task<bool> AddUserPhoto(string userId, PhotoToAddDto model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var file = model.PhotoFile;
            if (file.Length <= 0)
                return false;

            var imageUploadResult = new ImageUploadResult();
            using (var fs = file.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, fs),
                    Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
                };
                imageUploadResult = _cloudinary.Upload(imageUploadParams);
            }

            var publicId = imageUploadResult.PublicId;
            var Url = imageUploadResult.Url;
            user.ImgUrl = Url.AbsolutePath;

            await _userManager.UpdateAsync(user);
            return true;
        }

        //
        public async Task<List<AppUserDto>> SearchUsersAsync(string term)
        {
            if (string.IsNullOrEmpty(term))
                return new List<AppUserDto>();

            var users = await _userManager.Users
                .Where(u => u.Email.Contains(term)
                    || u.FirstName.Contains(term)
                    || u.LastName.Contains(term)
                    || u.city.Contains(term)
                    || u.State.Contains(term)
                    || u.country.Contains(term))
                .ToListAsync();

            var appUserDTOs = users.Select(item => new AppUserDto
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                UserName = item.UserName,
                Email = item.Email,
                ImgUrl = item.ImgUrl,
                PhoneNunmber = item.PhoneNumber,
                FaceBookUrl = item.FaceBookUrl,
                TwitterUrl = item.TwitterUrl,
                City = item.city,
                State = item.State,
                Country = item.country
            }).ToList();

            return appUserDTOs;
        }
    }
}
