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
    internal class GetCustomerAddressByIdQueryHandler : IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCustomerAddressByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerAddressId);
            var mappedEntity = mapper.Map<CustomerAddressResponse>(entity);
            return new ApiResponse<CustomerAddressResponse>(mappedEntity);
        }
    }
}
