using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Business;
using XpandDEVWebCourse.Data;
using XpandDEVWebCourse.Web.ViewModels;

namespace XpandDEVWebCourse.Web.Controllers
{
    [Route("Cars")]
    public class CarsController : Controller
    {
        private readonly ICarsService _carsService;

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _carsService.GetAllCarsAsync();

            var carResult = await _carsService.GetCarAsync(1);

            if (carResult.IsFailed)
                return BadRequest();

            var carsVm = cars
            .Select(m => new CarViewModel()
            {
                Id = m.Id,
                Model = m.Model,
                NrBolts = m.NrBolts
            }).ToList();

            return View(carsVm);
        }

        [HttpPost]
          
        public async Task<IActionResult> AddCar( [Bind("Model, NrBolts")] Cars car )
        {
            var result = await _carsService.AddCarAsync(car);
            // TEMP string script = "<script>alert('" + result + "');</script>";
            // TEMP Response.Write("<script>alert('Exception: ')</script>");
            return RedirectToAction(nameof(CarsController.Index));
        }
    }
}
