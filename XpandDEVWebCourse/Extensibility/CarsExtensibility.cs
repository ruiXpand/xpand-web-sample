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

        public async Task<CarViewModel> GetCar(int id)
        {
            var carResult = await _carsService.GetCarAsync(id);

            if (carResult.IsFailed)
                return null;

            CarViewModel carViewModel = new CarViewModel()
            {
                Id = carResult.Value.Id,
                Model = carResult.Value.Model,
                NrBolts = carResult.Value.NrBolts
            };

            return carViewModel;
        }

        public async Task<FluentResults.Result> AddCar(Cars car)
        {
            var carDto = new Cars()
            {
                Model = car.Model,
                NrBolts = car.NrBolts
            };

            var result = await _carsService.AddCarAsync(carDto);
            return result;
        }

        public async Task<FluentResults.Result> RemoveCar(int id)
        {
            var carResult = await _carsService.RemoveCarAsync(id);
            return carResult;
        }

        public async Task<FluentResults.Result> EditCar(Cars car)
        {
            var carEditResult = await _carsService.UpdateCarAsync(car);
            if (carEditResult.IsFailed)
                return null;

            return carEditResult;
        }
    }
}
