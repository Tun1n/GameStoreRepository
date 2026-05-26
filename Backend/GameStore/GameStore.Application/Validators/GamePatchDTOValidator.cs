using FluentValidation;
using GameStore.Application.DTO.GameDTO;

namespace GameStore.Application.Validators
{
    public class GamePatchDTOValidator : AbstractValidator<GamePatchDTO>
    {
        public GamePatchDTOValidator() 
        {
            When(game => game.Name != null, () =>
            {
                RuleFor(game => game.Name)
                    .NotEmpty()
                    .WithMessage("Game name is required.")
                    .MaximumLength(50)
                    .WithMessage("Game name cannot exceed 50 characters.")
                    .Must(name => !name.All(char.IsDigit))
                    .WithMessage("Game name cannot contain only numbers.");
            });

            When(game => game.ImageURL != null, () =>
            {
                RuleFor(game => game.ImageURL)
                    .Must(url => Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _))
                    .WithMessage("Game image URL is not valid.")
                    .Must(url => !url.All(char.IsDigit))
                    .WithMessage("Game image URL cannot contain only numbers.");
            });

            When(game => game.IsInstalled != null, () =>
            {
                RuleFor(game => game.IsInstalled)
                    .NotNull()
                    .WithMessage("Game installation status is required.");
            });
        }
    }
}
