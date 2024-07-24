using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;
using Para.Schema.Report;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerCountByCountry>>> Get()
        {
            var operation = new GetCustomerReportQuery();
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
