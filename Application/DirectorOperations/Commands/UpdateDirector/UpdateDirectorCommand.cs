using AutoMapper;
using MovieStoreWebApi.DBOperations;

namespace MovieStoreWebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        public UpdateDirectorViewModel Model { get; set; }
        public int DirectorId;
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.Id == DirectorId);
            if (director == null)
            {
                throw new InvalidOperationException("Böyle bir yönetmen bulunamadı");
            }

            _mapper.Map(Model, director);
            _context.SaveChanges();
        }

        public class UpdateDirectorViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}
