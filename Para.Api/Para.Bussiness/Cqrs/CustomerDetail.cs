using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs
{
    public record CreateCustomerDetailCommand(CustomerDetailRequest Request) : IRequest<ApiResponse<CustomerDetailResponse>>;
    public record UpdateCustomerDetailCommand(long CustomerDetailId, CustomerDetailRequest Request) : IRequest<ApiResponse>;
    public record DeleteCustomerDetailCommand(long CustomerDetailId) : IRequest<ApiResponse>;

    public record GetAllCustomerDetailQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
    public record GetCustomerDetailByIdQuery(long CustomerDetailId) : IRequest<ApiResponse<CustomerDetailResponse>>;
    public record GetCustomerDetailByCustomerIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerDetailResponse>>;
    public record GetCustomerDetailByParametersQuery(long? CustomerDetailId, long? CustomerId, string? FatherName, string? MotherName, string? EducationStatus, string? MontlyIncome, string? Occupation) : IRequest<ApiResponse<List<CustomerDetailResponse>>>;

}
