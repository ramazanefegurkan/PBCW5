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
    internal class GetCustomerDetailByCustomerIdQueryHandler : IRequestHandler<GetCustomerDetailByCustomerIdQuery, ApiResponse<CustomerDetailResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCustomerDetailByCustomerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            List<CustomerDetail> entityList = await unitOfWork.CustomerDetailRepository.GetAll(x => x.CustomerId == request.CustomerId);

            var mappedEntity = mapper.Map<CustomerDetailResponse>(entityList.FirstOrDefault());
            return new ApiResponse<CustomerDetailResponse>(mappedEntity);
        }
    }
}
