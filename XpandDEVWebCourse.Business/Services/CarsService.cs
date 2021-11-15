using FluentResults;
using Microsoft.EntityFrameworkCore;
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

        public CarsService(CourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.Car>> GetAllCarsAsync()
        {
            var dbCars = await _dbContext.Cars.ToListAsync();

            var cars = dbCars
                .Select(m => new Models.Car() { Id = m.Id, Model = m.Model, NrBolts=m.NrBolts })
                .ToList();

            return cars;
        }

        public async Task<Result<Models.Car>> GetCarAsync(int Id)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(m => m.Id == Id);

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

        public async Task<Result> AddCarAsync(Cars car)
        {
            Cars newCar = new Cars
            {
                Model = car.Model,
                NrBolts = car.NrBolts
            };

            try
            {
                await _dbContext.Cars.AddAsync(newCar);
                _dbContext.SaveChanges();
                Console.WriteLine(Result.Ok());
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }

        public async Task<Result> RemoveCarAsync(int Id)
        {
            Console.WriteLine("ID É: " + Id);
            return Result.Ok();
        }

        public Task<Result> UpdateCarAsync(Cars car)
        {
            throw new NotImplementedException();
        }
    }
}
