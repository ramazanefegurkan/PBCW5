using Para.Data.Domain;
using FluentValidation;
using Para.Schema;
using Para.Bussiness.Cqrs;

namespace Para.Bussiness.Validation
{
    public class CustomerRequestBaseValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerRequestBaseValidator()
        {
            RuleFor(request => request.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(request => request.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(request => request.IdentityNumber)
                .NotEmpty().WithMessage("Identity number is required.")
                .Length(11).WithMessage("Identity number must be 11 characters.");

            RuleFor(request => request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(request => request.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.");
        }
    }
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(command => command.Request).SetValidator(new CustomerRequestBaseValidator());
        }
    }

    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(command => command.CustomerId).GreaterThan(0).WithMessage("Customer id is required.");
            RuleFor(command => command.Request).SetValidator(new CustomerRequestBaseValidator());
        }
    }
}
