using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XpandDEVWebCourse.Business;
using XpandDEVWebCourse.Web.Entensibility;

namespace XpandDEVWebCourse.Web.Controllers.api
{
    [ApiController]
    [Route("api/cars")]
    public class CarsController : Controller
    {
        private readonly CarsExtensibility _carsExtensibility;

        public CarsController (CarsExtensibility carsExtensibility)
        {
            _carsExtensibility = carsExtensibility;
        }

        [HttpGet]
        [Route("all")]
        public async Task<PartialViewResult> CarsPartial()
        {
            var carsVm = await _carsExtensibility.GetAllCars();
            return PartialView("_ListedCar", carsVm);
        }
    }
}
