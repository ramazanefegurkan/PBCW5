using MediatR;
using Para.Base.Response;
using Para.Schema;
using Para.Schema.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs
{
    public record GetCustomerReportQuery() : IRequest<ApiResponse<List<CustomerCountByCountry>>>;
}
