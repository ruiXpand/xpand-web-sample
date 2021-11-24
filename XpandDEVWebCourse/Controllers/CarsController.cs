using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Business;
using XpandDEVWebCourse.Data;
using XpandDEVWebCourse.Models;
using XpandDEVWebCourse.Web.Entensibility;
using XpandDEVWebCourse.Web.ViewModels;

namespace XpandDEVWebCourse.Web.Controllers
{
    [Route("Cars")]
    public class CarsController : Controller
    {
        private readonly CarsExtensibility _carsExtensibility;

        public CarsController(CarsExtensibility carsExtensibility)
        {
            _carsExtensibility = carsExtensibility;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var carsVm = await _carsExtensibility.GetAllCars();
            return View(carsVm);
        }
          
        public async Task<Car> GetCar(int id)
        {
            var carResult = await _carsExtensibility.GetCar(1);
            return carResult;
        }

        public async Task<IActionResult> RemoveCar([Bind("Id")] int Id)
        {
            var carResult = await _carsExtensibility.RemoveCar(Id);
            return RedirectToAction(nameof(CarsController.Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarViewModel car)
        {
            var carDto = new Cars()
            {
                Model = car.Model,
                NrBolts = car.NrBolts
            };

            var result = await _carsExtensibility.AddCar(carDto);

            if (result.IsFailed)
            {
                ModelState.TryAddModelError("FailMessage", "Failed to add car!");
            }
            else
            {
                ModelState.TryAddModelError("SuccessMessage", "Car created successfully!");
            }

            var cars = await _carsExtensibility.GetAllCars();

            return View(nameof(CarsController.Index), cars);
        }

        [Route("SimpleMethod")]
        public IActionResult SimpleMethod()
        {
            Console.WriteLine("It works!");
            return RedirectToAction(nameof(CarsController.Index));
        }

    }
}
