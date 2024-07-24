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
    internal class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DeleteCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerAddress = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerAddressId);
            if (customerAddress == null)
            {
                throw new NotFoundException("Customer address not found");
            }

            await unitOfWork.CustomerAddressRepository.Delete(request.CustomerAddressId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
