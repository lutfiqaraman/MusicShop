using FluentValidation;
using MusicShop.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.API.Validations
{
    public class SaveArtistVMValidator : AbstractValidator<SaveArtistVM>
    {
        public SaveArtistVMValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
