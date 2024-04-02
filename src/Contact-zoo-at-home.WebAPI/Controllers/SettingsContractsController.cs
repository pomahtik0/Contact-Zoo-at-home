using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/settings/contracts")]
    public class SettingsContractsController : Controller
    {
        public IActionResult Index()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{contractId}")]
        public IActionResult Contract(int contractId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{contractId}")]
        public IActionResult DeleteContract(int contractId)
        {
            throw new NotImplementedException();
        }
    }
}
