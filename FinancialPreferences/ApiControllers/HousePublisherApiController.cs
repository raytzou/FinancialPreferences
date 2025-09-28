using BusinessLogic.Services.Interfaces;
using Common.Models;
using System.Web.Http;

namespace FinancialPreferences.ApiControllers
{
    public class HousePublisherApiController : ApiController
    {
        private IHousePublisherService _service;

        public HousePublisherApiController(IHousePublisherService service)
        {
            _service = service;
        }

        public List<House> GetAllHouses()
        {
            throw new NotImplementedException();
        }
    }
}
