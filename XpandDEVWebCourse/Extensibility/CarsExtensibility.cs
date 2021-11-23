using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Business;
using XpandDEVWebCourse.Data;
using XpandDEVWebCourse.Models;
using XpandDEVWebCourse.Web.ViewModels;

namespace XpandDEVWebCourse.Web.Entensibility
{
    public class CarsExtensibility
    {
        private readonly ICarsService _carsService;

        public CarsExtensibility(ICarsService carsService)
        {
            _carsService = carsService;
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

        public async Task<FluentResults.Result> EditCar(Cars car)
        {
            var carEditResult = await _carsService.UpdateCarAsync(car);
            if (carEditResult.IsFailed)
                return null;

            return carEditResult;
        }

        public async Task<FluentResults.Result> RemoveCar(int Id)
        {
            var carResult = await _carsService.RemoveCarAsync(Id);
            return carResult;
        }

        public async Task<FluentResults.Result<int>> AddCar(Cars car)
        {
            var carDto = new Cars()
            {
                Model = car.Model,
                NrBolts = car.NrBolts
            };
            var result = await _carsService.AddCarAsync(carDto);
            return result;
        }
    }
}
