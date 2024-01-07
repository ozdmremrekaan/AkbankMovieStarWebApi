using FluentValidation;

namespace MovieStoreWebApi.Application.ActorActressOperations.Commands.UpdateActorActress
{
    public class UpdateActorActressCommandValidator : AbstractValidator<UpdateActorActressCommand>
    {
        public UpdateActorActressCommandValidator()
        {
            RuleFor(command => command.ActorActressId).GreaterThan(0);
        }
    }
}
