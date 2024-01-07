using FluentValidation;

namespace MovieStoreWebApi.Application.ActorActressOperations.Commands.CreateActorActress
{
    public class CreateActorActressCommandValidator : AbstractValidator<CreateActorActressCommand>
    {
        public CreateActorActressCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2);
        }
    }
}
