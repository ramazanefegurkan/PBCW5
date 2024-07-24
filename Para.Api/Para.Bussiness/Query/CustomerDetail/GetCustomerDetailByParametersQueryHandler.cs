using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Query
{

    internal class GetCustomerDetailByParametersQueryHandler : IRequestHandler<GetCustomerDetailByParametersQuery, ApiResponse<List<CustomerDetailResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCustomerDetailByParametersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Handle(GetCustomerDetailByParametersQuery request, CancellationToken cancellationToken)
        {
            List<CustomerDetail> entityList = await unitOfWork.CustomerDetailRepository.GetAll(
            x => (!request.CustomerDetailId.HasValue || x.Id == request.CustomerDetailId) &&
                 (!request.CustomerId.HasValue || x.CustomerId == request.CustomerId) &&
                 (string.IsNullOrEmpty(request.EducationStatus) || x.EducationStatus == request.EducationStatus) &&
                 (string.IsNullOrEmpty(request.MontlyIncome) || x.MontlyIncome == request.MontlyIncome) &&
                 (string.IsNullOrEmpty(request.FatherName) || x.FatherName.Contains(request.FatherName)) &&
                 (string.IsNullOrEmpty(request.MotherName) || x.FatherName.Contains(request.MotherName)) &&
                 (string.IsNullOrEmpty(request.Occupation) || x.Occupation == request.Occupation));

            var mappedList = mapper.Map<List<CustomerDetailResponse>>(entityList);
            return new ApiResponse<List<CustomerDetailResponse>>(mappedList);
        }
    }
}
