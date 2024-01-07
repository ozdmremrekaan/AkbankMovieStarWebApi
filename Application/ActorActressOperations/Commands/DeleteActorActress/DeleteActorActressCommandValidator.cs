using FluentValidation;

namespace MovieStoreWebApi.Application.ActorActressOperations.Commands.DeleteActorActress
{
    public class DeleteActorActressCommandValidator : AbstractValidator<DeleteActorActressCommand>
    {
        public DeleteActorActressCommandValidator()
        {
            RuleFor(command => command.ActorActressId).GreaterThan(0);
        }
    }
}
