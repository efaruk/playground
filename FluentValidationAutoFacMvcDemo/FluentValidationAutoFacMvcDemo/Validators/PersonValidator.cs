using FluentValidation;
using PersonModel = FluentValidAutoFacMvcDemo.Models.PersonModel;

namespace FluentValidAutoFacMvcDemo.Validators
{
    public class PersonValidator : AbstractValidator<PersonModel>
    {
        public PersonValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty().WithMessage("Can not be empty!")
                .Length(2, 100).WithMessage("length must between 2-100");

            RuleFor(m => m.LastName)
                .NotEmpty().WithMessage("Can not be empty!")
                .Length(2, 100).WithMessage("length must between 2-100");
        }
    }
}