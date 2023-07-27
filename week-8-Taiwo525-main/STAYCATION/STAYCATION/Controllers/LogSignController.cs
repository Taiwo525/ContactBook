using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NuGet.Protocol.Core.Types;
using STAYCATION.Models;
using STAYCATION.Data;
using StayCation.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace STAYCATION.Controllers
{
    public class LogSignController : Controller
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;
        public LogSignController(IRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            //List<Customers> st = _repository.customers.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Customers cust)
        {
            try
            {
                var allUsers = _repository.ReadCustomersFromFile("StayRegFile.txt");
                string? email = cust.Email;
                string? password = cust.PassWord;

                var validUser = allUsers.FirstOrDefault(user => user.Email == email && user.PassWord == password);

                if (validUser != null)
                {
                    // Redirect to the home page
                    TempData["Success"] = "Logged in successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string? connectString = _configuration.GetConnectionString("DefaultConnection");
                    using (SqlConnection con = new SqlConnection(connectString))
                    {
                        SqlCommand cmd = new();
                        cmd.CommandText = "SELECT COUNT(*) FROM RegisteredUser WHERE Email = @Email AND UPassword = @Password";
                        cmd.Connection = con;

                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PassWord", _repository.HashPassword(password));

                        con.Open();

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            TempData["success"] = "Login successful";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            // Login failed
                            TempData["error"] = "Incorrect Credentials!";

                        }
                    }
                    ModelState.AddModelError(string.Empty, "Invalid email or password");
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                ModelState.AddModelError(string.Empty, "An error occurred during login");
                return View(); 
            }
        }

        
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Customers dto)
        {

            if (ModelState.IsValid)
            {
                string? connectString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection con = new SqlConnection(connectString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO RegisteredUsers(Id, FullName, Email, PassWord, DateTime) " +
                                      "VALUES (@Id, @FullName, @Email, @UPassWord, @DateTime)";
                    cmd.Connection = con;

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    cmd.Parameters.AddWithValue("@FullName", dto.FullName);
                    cmd.Parameters.AddWithValue("@Email", dto.Email);
                    cmd.Parameters.AddWithValue("@UPassword", _repository.HashPassword(dto.PassWord));
                    cmd.Parameters.AddWithValue("@DateTime", dto.RegisteredOn);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                TempData["success"] = "Registered successfully";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
