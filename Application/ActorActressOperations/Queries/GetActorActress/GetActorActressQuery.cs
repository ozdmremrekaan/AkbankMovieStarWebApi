using AutoMapper;
using MovieStoreWebApi.DBOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.ActorActressOperations.Queries.GetActorActress
{
    public class GetActorActressQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;


        public GetActorActressQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ActorActressViewModel> Handle()
        {
            var actorActressList = _dbContext.ActorActress.ToList();
            List<ActorActressViewModel> vm = _mapper.Map<List<ActorActressViewModel>>(actorActressList);
            return vm;
        }

        public class ActorActressViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }

        }


    }
}
