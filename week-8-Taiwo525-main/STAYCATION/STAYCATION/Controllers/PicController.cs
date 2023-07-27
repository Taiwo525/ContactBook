using Microsoft.AspNetCore.Mvc;
using StayCation.Models;
using StayCation.Repository;

namespace STAYCATION.Controllers
{
    public class PicController : Controller
    {
        private readonly IRepository _repository;
        public PicController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult PicsPage()
        {
            List<PictureData> Properties = _repository.GetHotels();
            var mostpicks = Properties.Where(p => p.Popularity == "Most Picks").ToList();
            var treasures = Properties.Where(p => p.Description == "Treasures").ToList();
            ViewData["mostpicks"] = mostpicks;
            ViewData["treasures"] = treasures;

            return View();
        }
    }
}
