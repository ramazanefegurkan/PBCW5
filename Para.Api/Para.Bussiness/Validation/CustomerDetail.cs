using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Para.Bussiness.Cqrs;

namespace Para.Bussiness.Validation
{
    public class CustomerDetailRequestBaseValidator : AbstractValidator<CustomerDetailRequest>
    {
        public CustomerDetailRequestBaseValidator()
        {
            RuleFor(x => x.FatherName).NotEmpty().WithMessage("Father's name is required.").MaximumLength(50);
            RuleFor(x => x.MotherName).NotEmpty().WithMessage("Mother's name is required.").MaximumLength(50);
            RuleFor(x => x.EducationStatus).NotEmpty().WithMessage("Education status is required.").MaximumLength(50);
            RuleFor(x => x.MontlyIncome).NotEmpty().WithMessage("Monthly income is required.").MaximumLength(50);
            RuleFor(x => x.Occupation).NotEmpty().WithMessage("Occupation is required.").MaximumLength(50);
        }
    }

    public class CreateCustomerDetailCommandValidator : AbstractValidator<CreateCustomerDetailCommand>
    {
        public CreateCustomerDetailCommandValidator()
        {
            RuleFor(command => command.Request).SetValidator(new CustomerDetailRequestBaseValidator());
        }
    }

    public class UpdateCustomerDetailCommandValidator : AbstractValidator<UpdateCustomerDetailCommand>
    {
        public UpdateCustomerDetailCommandValidator()
        {
            RuleFor(command => command.CustomerDetailId).GreaterThan(0).WithMessage("Customer detail id is required.");
            RuleFor(command => command.Request).SetValidator(new CustomerDetailRequestBaseValidator());
        }
    }
}
