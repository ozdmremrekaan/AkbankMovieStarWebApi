using AutoMapper;
using MovieStoreWebApi.DBOperations;

namespace MovieStoreWebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenresQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenreViewModel> Handle()
        {
            var genres = _dbContext.Genres.ToList();
            List<GenreViewModel> vm = _mapper.Map<List<GenreViewModel>>(genres);
            return vm;
        }

        
        public class GenreViewModel
        {
            public string GenreTitle { get; set; }
        }

    }
}
