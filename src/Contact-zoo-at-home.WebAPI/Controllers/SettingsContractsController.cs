using Contact_zoo_at_home.Shared.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contact_zoo_at_home.Shared;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using AutoMapper;
using Contact_zoo_at_home.Shared.Dto.Contracts;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/{culture=en}/settings/contracts")]
    public class SettingsContractsController : Controller
    {
        private readonly ICustomerManager _customerManager;
        private readonly IPetOwnerManager _petOwnerManager;
        private readonly IMapper _mapper;

        public SettingsContractsController(ICustomerManager customerManager, IIndividualOwnerManager petOwnerManager, IMapper mapper)
        {
            _customerManager = customerManager;
            _petOwnerManager = petOwnerManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var role = User.Claims.GetRole();
            var userId = User.Claims.GetId();

            if(role == Roles.Customer)
            {
                try
                {
                    var contracts = await _customerManager.GetAllContractsAsync(userId);
                    var dtos = _mapper.Map<IList<SimpleDisplayContractDto>>(contracts);
                    return Json(dtos);
                }
                catch
                {
                    return BadRequest();
                }
            }
            else if (role == Roles.IndividualPetOwner || role == Roles.Company)
            {
                try
                {
                    var contracts = await _petOwnerManager.GetAllActiveContractsAsync(userId);
                    var dtos = _mapper.Map<IList<SimpleDisplayContractDto>>(contracts);
                    return Json(dtos);
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("bad user role");
            }
        }

        //[HttpGet]
        //[Route("{contractId}")]
        //public IActionResult Contract(int contractId)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpDelete]
        //[Route("{contractId}")]
        //public IActionResult DeleteContract(int contractId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
