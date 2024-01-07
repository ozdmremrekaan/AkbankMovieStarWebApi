using AutoMapper;
using MovieStoreWebApi.DBOperations;

namespace MovieStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public GenreDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.Where(x => x.Id == GenreId).FirstOrDefault();
            if (genre == null)
            {
                throw new InvalidOperationException("Böyle bir janra bulunamadı");
            }

            GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);

            return vm;
        }

        public class GenreDetailViewModel
        {
            public string GenreTitle { get; set; }
        }
    }
}
