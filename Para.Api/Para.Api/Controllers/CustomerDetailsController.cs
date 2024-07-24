using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Get()
        {
            var operation = new GetAllCustomerDetailQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerDetailId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> Get([FromRoute] long customerDetailId)
        {
            var operation = new GetCustomerDetailByIdQuery(customerDetailId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("fromCustomer/{customerId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> GetFromCustomerId([FromRoute] long customerId)
        {
            var operation = new GetCustomerDetailByCustomerIdQuery(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("search")]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> GetByParameters([FromQuery]long? customerDetailId, [FromQuery] long? customerId, [FromQuery] string? fatherName, [FromQuery] string? motherName, [FromQuery] string? educationStatus, [FromQuery] string? montlyIncome, [FromQuery] string? occupation)
        {
            var query = new GetCustomerDetailByParametersQuery(customerDetailId, customerId, fatherName, motherName, educationStatus,montlyIncome,occupation);
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerDetailResponse>> Post([FromBody] CustomerDetailRequest value)
        {
            var operation = new CreateCustomerDetailCommand(value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerDetailId}")]
        public async Task<ApiResponse> Put(long customerDetailId, [FromBody] CustomerDetailRequest value)
        {
            var operation = new UpdateCustomerDetailCommand(customerDetailId, value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerDetailId}")]
        public async Task<ApiResponse> Delete(long customerDetailId)
        {
            var operation = new DeleteCustomerDetailCommand(customerDetailId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
