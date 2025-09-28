using BusinessLogic.Services.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialPreferences.Controllers
{
    [ApiController]
    [Route("api/houses")]
    public class HousePublisherApiController : ControllerBase
    {
        private readonly IHousePublisherService _service;

        public HousePublisherApiController(IHousePublisherService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<House>> GetAllHouses() => Ok(_service.GetAllHouses());
    }
}