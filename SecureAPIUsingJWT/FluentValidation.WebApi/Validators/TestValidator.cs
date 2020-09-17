using FluentValidation.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidation.WebApi.Validators
{
    public class TestValidator : AbstractValidator<Tester>
    {
        public TestValidator()
        {
            RuleFor(p => p.FirstName)
               .Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!")
               .Length(2, 25);
        }
    }
}
