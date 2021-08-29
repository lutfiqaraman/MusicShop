using FluentValidation;
using MusicShop.API.ViewModels;

namespace MusicShop.API.Validations
{
    public class SaveMusicVMValidator : AbstractValidator<SaveMusicVM>
    {
        public SaveMusicVMValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.ArtistId)
                .NotEmpty()
                .WithMessage("Artist Id must not be 0.");
        }
    }
}
