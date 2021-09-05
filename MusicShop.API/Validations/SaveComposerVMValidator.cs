using FluentValidation;
using MusicShop.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.API.Validations
{
    public class SaveComposerVMValidator : AbstractValidator<SaveComposerVM>
    {
        public SaveComposerVMValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.LastName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
