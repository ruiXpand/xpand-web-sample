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
            var carResult = await _carsExtensibility.GetCar(id);
            return carResult;
        }

        [Route("CarEdit")]
        public async Task<IActionResult> EditCar(int id, string model, int nrBolts)
        {
            var carDto = new Cars()
            {
                Id = id,
                Model = model,
                NrBolts = nrBolts
            };
            Console.WriteLine(id + model + nrBolts);
            var carResult = await _carsExtensibility.EditCar(carDto);
            Console.WriteLine(carResult);
            if (carResult.IsSuccess)
                return Ok();
            return BadRequest();
        }

        [Route("CarRemove")]
        public async Task<IActionResult> RemoveCar(int carId)
        {
            var carResult = await _carsExtensibility.RemoveCar(carId);
            Console.WriteLine(carResult);
            if (carResult.IsSuccess)
                return Ok();
            return BadRequest();
        }

        [Route("CarList")]
        public async Task<IActionResult> CarsPartial()
        {
            var carsVm = await _carsExtensibility.GetAllCars();
            return PartialView("_ListedCar", carsVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(string model, int nrBolts)
        {
            var carDto = new Cars()
            {
               Model = model,
               NrBolts = nrBolts
            };

            var result = await _carsExtensibility.AddCar(carDto);

            if (result.IsFailed)
            {
                ModelState.TryAddModelError("FailMessage", "Failed to add car!");
                return BadRequest();
            }
            else
            {
                ModelState.TryAddModelError("SuccessMessage", "Car created successfully!");
                return PartialView("_ListedCar", new CarViewModel() { Id = result.Value, Model = model, NrBolts = nrBolts});
            }
        }
    }
}
