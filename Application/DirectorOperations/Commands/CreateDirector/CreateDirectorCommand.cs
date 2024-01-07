using AutoMapper;
using MovieStoreWebApi.DBOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.Name == Model.Name);
            if (director !=null)
            {
                throw new InvalidOperationException("Böyle bir yönetmen zaten mevcut");
            }

            director = _mapper.Map<Director>(Model);

            _context.Directors.Add(director);
            _context.SaveChanges();
        }

        public class CreateDirectorViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}
