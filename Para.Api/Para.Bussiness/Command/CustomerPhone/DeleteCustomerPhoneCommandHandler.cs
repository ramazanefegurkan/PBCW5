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
    internal class DeleteCustomerPhoneCommandHandler : IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DeleteCustomerPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
        {
            var customerPhone = await unitOfWork.CustomerPhoneRepository.GetById(request.CustomerPhoneId);
            if (customerPhone == null)
            {
                throw new NotFoundException("Customer phone not found");
            }

            await unitOfWork.CustomerPhoneRepository.Delete(request.CustomerPhoneId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
