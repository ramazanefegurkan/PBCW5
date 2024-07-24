using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPhonesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerPhonesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Get()
        {
            var operation = new GetAllCustomerPhoneQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerPhoneId}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> Get([FromRoute] long customerPhoneId)
        {
            var operation = new GetCustomerPhoneByIdQuery(customerPhoneId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("fromCustomer/{customerId}")]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> GetFromCustomerId([FromRoute] long customerId)
        {
            var operation = new GetAllCustomerPhoneByCustomerIdQuery(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("search")]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> GetByParameters([FromQuery] long? customerPhoneId, [FromQuery] long? customerId, [FromQuery] string? countryCode, [FromQuery] string? phone, [FromQuery] bool? isDefault)
        {
            var query = new GetCustomerPhoneByParametersQuery(customerPhoneId, customerId, countryCode, phone, isDefault);
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerPhoneResponse>> Post([FromBody] CustomerPhoneRequest value)
        {
            var operation = new CreateCustomerPhoneCommand(value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerPhoneId}")]
        public async Task<ApiResponse> Put(long customerPhoneId, [FromBody] CustomerPhoneRequest value)
        {
            var operation = new UpdateCustomerPhoneCommand(customerPhoneId, value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerPhoneId}")]
        public async Task<ApiResponse> Delete(long customerPhoneId)
        {
            var operation = new DeleteCustomerPhoneCommand(customerPhoneId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
