﻿using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using XpandDEVWebCourse.Data;
using XpandDEVWebCourse.Models;

namespace XpandDEVWebCourse.Business
{
    public interface ICarsService
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Result<Car>> GetCarAsync(int id);
        Task<Result> AddCarAsync(Cars car);
        Task<Result> RemoveCarAsync(int id);
        Task<Result> UpdateCarAsync(Cars car);
    }
}