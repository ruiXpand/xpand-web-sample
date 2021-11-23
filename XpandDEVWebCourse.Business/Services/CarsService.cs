using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Data;
using XpandDEVWebCourse.Models;

namespace XpandDEVWebCourse.Business
{
    public class CarsService : ICarsService
    {
        private readonly CourseDbContext _dbContext;
        private readonly ILogger<CarsService> _logger;

        public CarsService(CourseDbContext dbContext, ILogger<CarsService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Models.Car>> GetAllCarsAsync()
        {
            var dbCars = await _dbContext.Cars.AsNoTracking().ToListAsync();

            var cars = dbCars
                .Select(m => new Models.Car() { Id = m.Id, Model = m.Model, NrBolts=m.NrBolts })
                .ToList();

            return cars;
        }

        public async Task<Result<Models.Car>> GetCarAsync(int Id)
        {
            var car = await _dbContext.Cars.AsNoTracking().FirstOrDefaultAsync(m => m.Id == Id);

            if (car == null)
                return Result.Fail("Error while trying to get car");

            var dtoCar = new Models.Car()
            {
                Id = car.Id,
                Model = car.Model,
                NrBolts = car.NrBolts
            };

            return Result.Ok(dtoCar);
        }

        public async Task<Result<int>> AddCarAsync(Cars car)
        {
            Cars newCar = new Cars
            {
                Model = car.Model,
                NrBolts = car.NrBolts
            };

            try
            {
                _dbContext.Cars.Add(newCar);

                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    _logger.LogInformation("Ok", newCar);
                    return Result.Ok(newCar.Id);
                }
                _logger.LogError("Error while trying to add car!", newCar);
                return Result.Fail(string.Empty);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }

        public async Task<Result> RemoveCarAsync(int Id)
        {
            try
            {
                Cars carToRemove = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == Id);
                var result = _dbContext.Cars.Remove(carToRemove);
                _dbContext.SaveChanges();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> UpdateCarAsync(Cars car)
        {
            try
            {
                //Cars carToEdit = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == Id);
                var result = _dbContext.Cars.Update(car);
                await _dbContext.SaveChangesAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
