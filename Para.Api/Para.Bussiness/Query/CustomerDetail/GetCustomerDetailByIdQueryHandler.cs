using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.UnitOfWork;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Query
{
    internal class GetCustomerDetailByIdQueryHandler : IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCustomerDetailByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerDetailResponse>> Handle(GetCustomerDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CustomerDetailRepository.GetById(request.CustomerDetailId);
            var mappedEntity = mapper.Map<CustomerDetailResponse>(entity);
            return new ApiResponse<CustomerDetailResponse>(mappedEntity);
        }
    }
}
