using PrasTestJobDTO;
using PrasTestJobServices.Abstract;

namespace PrasTestJobServices.Implementations
{
    public class NewsServices : INewsServices
    {
        public Task<Guid> CreateNewsAsync(CreateNewsDto newNews)
        {
            throw new NotImplementedException();
        }

        public Task<NewsDto[]> GetNewsAsync(int skipNewsCount, int takeNewsCount)
        {
            throw new NotImplementedException();
        }

        public Task<NewsDto> GetNewsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
