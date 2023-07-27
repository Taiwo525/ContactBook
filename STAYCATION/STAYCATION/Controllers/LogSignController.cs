using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NuGet.Protocol.Core.Types;
using STAYCATION.Models;
using STAYCATION.Data;
using StayCation.Repository;

namespace STAYCATION.Controllers
{
    public class LogSignController : Controller
    {
        private readonly IRepository _repository;
        public LogSignController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            //List<Customers> st = _repository.customers.ToList();
            return View();
        }
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
                string email = cust.Email;
                string password = cust.PassWord;

                var validUser = allUsers.FirstOrDefault(user => user.Email == email && user.PassWord == password);

                if (validUser != null)
                {
                    // Redirect to the home page
                    TempData["Success"] = "Logged in successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
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

        //public IActionResult Login(Customers cust)
        //{
        //    try
        //    {
        //        var allUsers = _repository.ReadCustomersFromFile("StayRegFile.txt");
        //        string? email = cust.Email;
        //        string? password = cust.PassWord;

        //        var validUser = allUsers.FirstOrDefault(user => user.Email == email && user.PassWord == password);

        //        if (validUser != null)
        //        {
        //            _repository.customers.Add(cust);
        //            _repository.SaveChanges();
        //            TempData["Success"] = "Logged in successfully";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid email or password");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or handle the exception as needed
        //        ModelState.AddModelError(string.Empty, "An error occurred during login");
        //    }

        //    return View();
        //}

        //public IActionResult Login(Customers cust)
        //{
        //    try
        //    {
        //        var allUsers = _repository.ReadCustomersFromFile("StayRegFile.txt");
        //        string? email = cust.Email;
        //        string? password = cust.PassWord;

        //        var validUser = allUsers.FirstOrDefault(user => user.Email == email && user.PassWord == password);
        //        //if (ModelState.IsValid)
        //        //{
        //        //    _repository.customers.Add(cust);
        //        //    _repository.SaveChanges();
        //        //    TempData["Success"] = "login successfully";
        //        //    return RedirectToAction("index");
        //        //}
        //        if (validUser != null)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid email or password");
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or handle the exception as needed
        //        ModelState.AddModelError(string.Empty, "An error occurred during login");
        //        return View();
        //    }
        //}

        //public IActionResult Login(Customers cust)
        //{
        //    var alluser = _repository.ReadCustomersFromFile("StayRegFile.txt");
        //    string? e = cust.Email;
        //    string? p = cust.PassWord;

        //    var valid = alluser.FirstOrDefault(user => user.Email == e && user.PassWord == p);

        //    if (valid != null)
        //    {
        //        return RedirectToAction("Sign_Up");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to log in");
        //    }
        //    return View();
        //}
        public IActionResult Sign_Up()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sign_Up(Customers dto)
        {

            if (ModelState.IsValid)
            {
                using (StreamWriter sw = new StreamWriter("StayRegFile.txt", true))
                {
                    sw.WriteLine($"| {dto.Id} | {dto.Name} | {dto.Email} | {dto.PassWord} | {dto.RegisteredOn}");
                }
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
