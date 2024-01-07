using FluentValidation;

namespace MovieStoreWebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.Model.GenreTitle).NotEmpty().MinimumLength(3);
        }
    }
}
