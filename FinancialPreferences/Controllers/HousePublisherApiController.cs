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

        [HttpPost]
        public ActionResult<House> Create([FromBody] House request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validationErrors = _service.Validate(request);
            if (validationErrors.Count > 0)
            {
                foreach (var error in validationErrors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }

            try
            {
                _service.Create(request.HouseName, request.Address, request.TotalPrice, request.FloorArea, request.Description);

                return CreatedAtAction(nameof(GetAllHouses), new { id = request.Id }, request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "建立房屋時發生內部錯誤", error = ex.Message });
            }
        }
    }
}