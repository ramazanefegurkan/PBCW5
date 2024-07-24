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
    internal class GetCustomerAddressByParametersQueryHandler : IRequestHandler<GetCustomerAddressByParametersQuery, ApiResponse<List<CustomerAddressResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCustomerAddressByParametersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetCustomerAddressByParametersQuery request, CancellationToken cancellationToken)
        {
            List<CustomerAddress> entityList = await unitOfWork.CustomerAddressRepository.GetAll(
                x => (!request.CustomerAddressId.HasValue || x.Id == request.CustomerAddressId) &&
                            (!request.CustomerId.HasValue || x.CustomerId == request.CustomerId) &&
                            (string.IsNullOrEmpty(request.Country) || x.Country.Contains(request.Country)) &&
                            (string.IsNullOrEmpty(request.ZipCode) || x.ZipCode == request.ZipCode) &&
                            (string.IsNullOrEmpty(request.City) || x.City.Contains(request.City)));

            var mappedList = mapper.Map<List<CustomerAddressResponse>>(entityList);
            return new ApiResponse<List<CustomerAddressResponse>>(mappedList);
        }
    }
}
