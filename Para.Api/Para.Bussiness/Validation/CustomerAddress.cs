using Para.Data.Domain;
using FluentValidation;
using Para.Schema;
using Para.Bussiness.Cqrs;

namespace Para.Bussiness.Validation
{
    public class CustomerAddressRequestBaseValidator : AbstractValidator<CustomerAddressRequest>
    {
        public CustomerAddressRequestBaseValidator()
        {
            RuleFor(request => request.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(request => request.Country).NotEmpty().WithMessage("Country is required.").MaximumLength(50).WithMessage("Country must not exceed 50 characters.");
            RuleFor(request => request.City).NotEmpty().WithMessage("City is required.").MaximumLength(50).WithMessage("City must not exceed 50 characters.");
            RuleFor(request => request.AddressLine).NotEmpty().WithMessage("Address line is required.").MaximumLength(250).WithMessage("Address line must not exceed 250 characters.");
            RuleFor(request => request.ZipCode).MaximumLength(6).WithMessage("Zip code must not exceed 6 characters.");
            RuleFor(request => request.IsDefault).NotNull().WithMessage("IsDefault must be specified.");
        }
    }
    public class CreateCustomerAddressCommandValidator : AbstractValidator<CreateCustomerAddressCommand>
    {
        public CreateCustomerAddressCommandValidator()
        {
            RuleFor(command => command.Request).SetValidator(new CustomerAddressRequestBaseValidator());
        }
    }

    public class UpdateCustomerAddressCommandValidator : AbstractValidator<UpdateCustomerAddressCommand>
    {
        public UpdateCustomerAddressCommandValidator()
        {
            RuleFor(command => command.CustomerAddressId).GreaterThan(0).WithMessage("Customer address id is required.");
            RuleFor(command => command.Request).SetValidator(new CustomerAddressRequestBaseValidator());
        }
    }
}
