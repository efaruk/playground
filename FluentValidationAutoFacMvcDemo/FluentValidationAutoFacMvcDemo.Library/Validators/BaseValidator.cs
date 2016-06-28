using FluentValidation;
using FluentValidationAutoFacMvcDemo.Models;

namespace FluentValidationAutoFacMvcDemo.Validators
{
    public class BaseValidator<TModel> : AbstractValidator<TModel> where TModel: BaseModel
    {
        public BaseValidator()
        {
        }
    }
}