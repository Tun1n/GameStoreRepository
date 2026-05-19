using FluentValidation;
using GameStore.Application.DTO.GameDTO;

namespace GameStore.Application.Validators
{
    public class GameUpdateDTOValidator : AbstractValidator<GameUpdateDTO>
    {
        public GameUpdateDTOValidator()
        {
            RuleFor(game => game.Name)
                .NotEmpty()
                .WithMessage("Game name is required.")
                .MaximumLength(50)
                .WithMessage("Game name cannot exceed 50 characters.")
                .Must(name => !name.All(char.IsDigit))
                .WithMessage("Game name cannot contain only numbers.");

            RuleFor(game => game.ImageURL)
                .NotNull()
                .WithMessage("Game image URL is required.")
                .Must(url => Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _))
                .WithMessage("Game image URL is not valid.")
                .Must(url => !url.All(char.IsDigit))
                .WithMessage("Game image URL cannot contain only numbers.");
        }
    }
}
