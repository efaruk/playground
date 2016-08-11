using System;
using System.Web.Mvc;
using FluentValidation;

namespace FluentValidAutoFacMvcDemo.Infrastructure
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            var instance = DependencyResolver.Current.GetService(validatorType);
            if (instance == null) return null;
            var validator = instance as IValidator;
            return validator;
        }
    }
}