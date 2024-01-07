using FluentValidation;

namespace MovieStoreWebApi.Application.ActorActressOperations.Queries.GetActorActressDetail
{
    public class GetActorActressDetailQueryValidator : AbstractValidator<GetActorActressDetailQuery>
    {
        public GetActorActressDetailQueryValidator()
        {
            RuleFor(query => query.ActorActressId).GreaterThan(0);
        }
    }
}
