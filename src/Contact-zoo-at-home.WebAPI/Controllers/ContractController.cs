using AutoMapper;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Shared.Dto.Contracts;
using Contact_zoo_at_home.Shared.Dto.Notifications;
using Contact_zoo_at_home.Shared.Extentions;
using Contact_zoo_at_home.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Route("api/{culture=en}/contracts")]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    [Authorize]
    public class ContractController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerManager _customerManager;

        public ContractController(IMapper mapper, ICustomerManager customerManager)
        {
            _mapper = mapper;
            _customerManager = customerManager;
        }

        [Authorize(Policy = "Customer")]
        [Route("standart")]
        [HttpPost]
        public async Task<IActionResult> CreateStandartContract([FromBody]CreateStandartContractDto dto)
        {
            int userId = User.Claims.GetId();

            var contractInfo = _mapper.Map<StandartContract>(dto);

            try
            {
                var notification = await _customerManager.CreateNewStandartContractAsync(contractInfo, userId, dto.PetIds);
                var model = _mapper.Map<NotificationDto>(notification);
                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = "Customer")]
        [Route("{contractId}")]
        [HttpDelete] // not actualy delete, but who cares
        public async Task<IActionResult> ForceCloseContract(int contractId)
        {
            // tests only!!
            try
            {
                var notifications = await _customerManager.ForceCloseContractAsync(contractId);
                var model = _mapper.Map<IEnumerable<NotificationDto>>(notifications);
                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
