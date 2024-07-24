using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;
using PBCW2.Bussiness.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Command
{
    internal class UpdateCustomerDetailCommandHandler : IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateCustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var customerDetail = await unitOfWork.CustomerDetailRepository.GetById(request.CustomerDetailId);
            if (customerDetail == null)
            {
                throw new NotFoundException("Customer detail not found");
            }

            var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            mapped.Id = request.CustomerDetailId;
            unitOfWork.CustomerDetailRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
