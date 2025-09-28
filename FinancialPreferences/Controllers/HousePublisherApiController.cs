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
            var validationErrors = _service.Validate(request);
            if (validationErrors.Count > 0)
            {
                // 回傳自訂的錯誤格式，不使用 ModelState
                return BadRequest(new
                {
                    message = "驗證失敗",
                    errors = validationErrors
                });
            }

            try
            {
                _service.Create(request.HouseName, request.Address, request.TotalPrice, request.FloorArea, request.Description);

                // 回傳 201 Created 狀態碼和新建立的資源
                return CreatedAtAction(nameof(GetAllHouses), new { id = request.Id }, request);
            }
            catch (Exception ex)
            {
                // 記錄錯誤並回傳 500 Internal Server Error
                return StatusCode(500, new { message = "建立房屋時發生內部錯誤", error = ex.Message });
            }
        }
    }
}