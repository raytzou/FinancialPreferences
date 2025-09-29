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
                return BadRequest(new
                {
                    message = "驗證失敗",
                    errors = validationErrors
                });
            }

            try
            {
                _service.Create(request.HouseName, request.Address, request.TotalPrice, request.FloorArea, request.Description);
                return Ok(new { message = "房屋建立成功" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "建立房屋時發生內部錯誤", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                // 驗證 ID 格式
                if (!Guid.TryParse(id, out _))
                {
                    return BadRequest(new
                    {
                        message = "無效的房屋 ID 格式"
                    });
                }

                _service.Delete(id);
                return Ok(new { message = "房屋刪除成功" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = "找不到指定的房屋", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "刪除房屋時發生內部錯誤", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<House> Update(string id, [FromBody] House request)
        {
            if (!Guid.TryParse(id, out var guidId) || request.Id != guidId)
            {
                return BadRequest(new { message = "ID 不一致或格式錯誤" });
            }

            var validationErrors = _service.Validate(request);
            if (validationErrors.Count > 0)
            {
                return BadRequest(new
                {
                    message = "驗證失敗",
                    errors = validationErrors
                });
            }

            try
            {
                _service.Update(request);
                return Ok(new { message = "房屋更新成功" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "更新房屋時發生內部錯誤", error = ex.Message });
            }
        }
    }
}