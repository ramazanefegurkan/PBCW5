using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerAddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
        {
            var operation = new GetAllCustomerAddressQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerAddressId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> Get([FromRoute] long customerAddressId)
        {
            var operation = new GetCustomerAddressByIdQuery(customerAddressId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("fromCustomer/{customerId}")]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> GetFromCustomerId([FromRoute] long customerId)
        {
            var operation = new GetAllCustomerAddressByCustomerIdQuery(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("search")]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> GetByParameters([FromQuery] long? customerAddressId, [FromQuery] long? customerId, [FromQuery] string? country, [FromQuery] string? city, [FromQuery] string? zipCode)
        {
            var query = new GetCustomerAddressByParametersQuery(customerAddressId, customerId, country, city, zipCode);
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerAddressResponse>> Post([FromBody] CustomerAddressRequest value)
        {
            var operation = new CreateCustomerAddressCommand(value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerAddressId}")]
        public async Task<ApiResponse> Put(long customerAddressId,[FromBody] CustomerAddressRequest value)
        {
            var operation = new UpdateCustomerAddressCommand(customerAddressId,value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerAddressId}")]
        public async Task<ApiResponse> Delete(long customerAddressId)
        {
            var operation = new DeleteCustomerAddressCommand(customerAddressId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
