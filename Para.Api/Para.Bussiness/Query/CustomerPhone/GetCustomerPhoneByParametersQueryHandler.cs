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
    internal class GetCustomerPhoneByParametersQueryHandler : IRequestHandler<GetCustomerPhoneByParametersQuery, ApiResponse<List<CustomerPhoneResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCustomerPhoneByParametersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetCustomerPhoneByParametersQuery request, CancellationToken cancellationToken)
        {
            List<CustomerPhone> entityList = await unitOfWork.CustomerPhoneRepository.GetAll(
                x => (!request.CustomerPhoneId.HasValue || x.Id == request.CustomerPhoneId) &&
                            (!request.CustomerId.HasValue || x.CustomerId == request.CustomerId) &&
                            (string.IsNullOrEmpty(request.Phone) || x.Phone.Contains(request.Phone)) &&
                            (string.IsNullOrEmpty(request.CountyCode) || x.CountyCode == request.CountyCode) &&
                            (!request.IsDefault.HasValue || request.IsDefault == x.IsDefault));

            var mappedList = mapper.Map<List<CustomerPhoneResponse>>(entityList);
            return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
        }
    }
}
