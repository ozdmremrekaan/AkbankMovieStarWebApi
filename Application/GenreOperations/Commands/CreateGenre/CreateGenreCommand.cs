using AutoMapper;
using MovieStoreWebApi.DBOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel Model { get; set; }

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Hande()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.GenreTitle == Model.GenreTitle);
            if (genre != null)
            {
                throw new InvalidOperationException("Böyle bir janra zaten mevcut");
            }

            genre = _mapper.Map<Genre>(Model);
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }

        public class CreateGenreViewModel
        {
            public string GenreTitle { get; set; }
        }
    }
}
