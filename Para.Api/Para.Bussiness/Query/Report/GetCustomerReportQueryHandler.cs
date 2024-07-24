using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;
using Para.Schema.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Query.Report
{
    internal class GetCustomerReportQueryHandler : IRequestHandler<GetCustomerReportQuery, ApiResponse<List<CustomerCountByCountry>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCustomerReportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerCountByCountry>>> Handle(GetCustomerReportQuery request, CancellationToken cancellationToken)
        {
            List<CustomerCountByCountry> entityList = await unitOfWork.ReportRepository.GetCustomersCountByCountryReport();

            return new ApiResponse<List<CustomerCountByCountry>>(entityList);
        }
    }
}
