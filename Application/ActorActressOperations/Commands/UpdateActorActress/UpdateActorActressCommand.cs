using AutoMapper;
using MovieStoreWebApi.DBOperations;

namespace MovieStoreWebApi.Application.ActorActressOperations.Commands.UpdateActorActress
{
    public class UpdateActorActressCommand
    {
        public UpdateActorActressModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public int ActorActressId { get; set; }

        public UpdateActorActressCommand(IMapper mapper, IMovieStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var actorActress = _dbContext.ActorActress.SingleOrDefault(x => x.Id == ActorActressId);

            if (actorActress == null)
            {
                throw new InvalidOperationException("Böyle bir aktör bulunamadı");
            }

            _mapper.Map(Model, actorActress);
            _dbContext.SaveChanges();
        }


        public class UpdateActorActressModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}
