using AutoMapper;
using MovieStoreWebApi.DBOperations;

namespace MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public int DirectorId { get; set; }

        public GetDirectorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public DirectorDetailViewModel Handle()
        {
            var director = _dbContext.Directors.Where(director => director.Id == DirectorId).FirstOrDefault();

            if (director == null)
            {
                throw new InvalidOperationException("Böyle bir yönetmen bulunamadı");
            }

            DirectorDetailViewModel vm = _mapper.Map<DirectorDetailViewModel>(director);

            return vm;
        }


        public class DirectorDetailViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
        
    }
}
