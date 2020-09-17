using FluentValidation.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidation.WebApi.Validators
{
    public class DeveloperValidator : AbstractValidator<Developer>
    {
        public DeveloperValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage("{PropertyName} should not empty. Never!")
                .Length(2, 25)
                .Must(IsValidName).WithMessage("{PropertyName} Should be all letters");

            RuleFor(p => p.Email)
                .EmailAddress();
        }

        private bool IsValidName(string name)
        {
            return name.All(char.IsLetter);
        }
    }
}
