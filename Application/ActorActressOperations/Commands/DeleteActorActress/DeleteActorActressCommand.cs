using MovieStoreWebApi.DBOperations;

namespace MovieStoreWebApi.Application.ActorActressOperations.Commands.DeleteActorActress
{
    public class DeleteActorActressCommand
    {
        private readonly IMovieStoreDbContext _dbContext;

        public int ActorActressId { get; set; }

        public DeleteActorActressCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var actorActress = _dbContext.ActorActress.SingleOrDefault(x => x.Id == ActorActressId);

            if (actorActress == null)
            {
                throw new InvalidOperationException("Böyle bir aktör bulunamadı");
            }

            _dbContext.ActorActress.Remove(actorActress);
            _dbContext.SaveChanges();

        }
    }
}
