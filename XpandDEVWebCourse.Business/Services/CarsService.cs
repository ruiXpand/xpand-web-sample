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
                .Select(m => new Models.Car() { Id = m.Id, Model = m.Model })
                .ToList();

            return cars;
        }

        public async Task<Result<Models.Car>> GetCarAsync(int Id)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(m => m.Id == Id);

            if (car == null)
                return Result.Fail("Error while trying to get card");

            var dtoCar = new Models.Car()
            {
                Id = car.Id,
                Model = car.Model
            };

            return Result.Ok(dtoCar);
        }
    }
}
