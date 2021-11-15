using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Business;
using XpandDEVWebCourse.Data;
using XpandDEVWebCourse.Models;
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
            var carsVm = await GetAllCars();

            return View(carsVm);
        }

        public async Task<List<CarViewModel>> GetAllCars()
        {
            var cars = await _carsService.GetAllCarsAsync();

            var carsVm = cars
            .Select(m => new CarViewModel()
            {
                Id = m.Id,
                Model = m.Model,
                NrBolts = m.NrBolts
            }).ToList();

            return carsVm;
        }

        public async Task<Car> GetCar(int id)
        {
            var carResult = await _carsService.GetCarAsync(1);

            if (carResult.IsFailed)
                return null;

            return carResult.Value;
        }

        public async Task<IActionResult> RemoveCar([Bind("Id")] int Id)
        {
            var carResult = await _carsService.RemoveCarAsync(Id);
            Console.WriteLine("ID É: " + Id);

            return RedirectToAction(nameof(CarsController.Index));
            //return null;
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarViewModel car)
        {
            var carDto = new Cars()
            {
                Model = car.Model,
                NrBolts = car.NrBolts
            };

            var result = await _carsService.AddCarAsync(carDto);

            if (result.IsFailed)
            {
                ModelState.TryAddModelError("FailMessage", "Failed to add car!");
            }
            else
            {
                ModelState.TryAddModelError("SuccessMessage", "Car created successfully!");
            }

            //return View(nameof(CarsController.Index), GetAllCars().Result);
            return RedirectToAction(nameof(CarsController.Index));

        }

    }
}
