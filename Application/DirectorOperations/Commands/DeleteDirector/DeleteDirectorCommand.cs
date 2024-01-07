using MovieStoreWebApi.DBOperations;

namespace MovieStoreWebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;

        public int DirectorId { get; set; }
        public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var Director = _dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);

            if (Director == null)
            {
                throw new InvalidOperationException("Böyle bir yönetmen bulunamadı");
            }

            _dbContext.Directors.Remove(Director);
            _dbContext.SaveChanges();
        }
        
    }
}
