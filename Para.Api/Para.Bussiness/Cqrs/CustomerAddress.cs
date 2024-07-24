using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerAddressCommand(CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record UpdateCustomerAddressCommand(long CustomerAddressId, CustomerAddressRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerAddressCommand(long CustomerAddressId) : IRequest<ApiResponse>;

public record GetAllCustomerAddressQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
public record GetCustomerAddressByIdQuery(long CustomerAddressId) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record GetAllCustomerAddressByCustomerIdQuery(long CustomerId) : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
public record GetCustomerAddressByParametersQuery(long? CustomerAddressId,long? CustomerId, string? Country, string? City,string? ZipCode) : IRequest<ApiResponse<List<CustomerAddressResponse>>>;

