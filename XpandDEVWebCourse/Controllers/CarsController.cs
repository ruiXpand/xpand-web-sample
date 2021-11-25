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

        public CarViewModel GetCar(int id)
        {
            var carResult = _carsExtensibility.GetCar(1);
            return carResult.Result;
        }

        [HttpPost]
        public IActionResult AddCar(CarViewModel car)
        {
            var carDto = new Cars()
            {
                Model = car.Model,
                NrBolts = car.NrBolts
            };

            var carResult = _carsExtensibility.AddCar(carDto);

            if (carResult == null)
                ModelState.TryAddModelError("FailMessage", "Failed to add car!");
            else
                ModelState.TryAddModelError("SuccessMessage", "Car created successfully!");

            var cars = _carsExtensibility.GetAllCars();
            return View(nameof(CarsController.Index), cars);
        }

        [Route("RemoveCar")]
        public IActionResult RemoveCar(int id)
        {
            var carResult = _carsExtensibility.RemoveCar(id);
            if (carResult.IsCompletedSuccessfully)
                return RedirectToAction(nameof(CarsController.Index));
            return BadRequest();
        }

        [Route("EditCar")]
        public IActionResult EditCar(int id)
        {
            var result = _carsExtensibility.GetCar(id);
            return View(result);
        }

        [HttpPost]
        [Route("SaveEditCar")]
        public IActionResult SaveEditCar(CarViewModel car)
        {
            var carDto = new Cars()
            {
                Id = car.Id,
                Model = car.Model,
                NrBolts = car.NrBolts
            };

            var carResult = _carsExtensibility.EditCar(carDto);
            if (carResult == null)
            {
                ModelState.TryAddModelError("FailMessage", "Failed to edit car!");
                return View(nameof(EditCar), car);
            }
            ModelState.TryAddModelError("SuccessMessage", "Car edited successfully!");
            return View(nameof(EditCar), car);
        }

        [Route("BackEditCar")]
        public IActionResult CancelEditCar()
        {
            return RedirectToAction(nameof(CarsController.Index));
        }
    }
}
