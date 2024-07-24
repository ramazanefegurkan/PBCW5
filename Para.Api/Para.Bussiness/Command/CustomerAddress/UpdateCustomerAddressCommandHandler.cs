using AutoMapper;
using FluentValidation;
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
    internal class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerAddress = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerAddressId);
            if (customerAddress == null)
            {
                throw new NotFoundException("Customer address not found");
            }

            var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            mapped.Id = request.CustomerAddressId;
            unitOfWork.CustomerAddressRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
