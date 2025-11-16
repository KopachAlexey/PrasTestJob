using PrasTestJobDTO;

namespace PrasTestJobServices.Abstract
{
    public interface INewsServices
    {
        Task<Guid> CreateNewsAsync(CreateNewsDto newNews);
        Task<NewsDto?> GetNewsByIdAsync(Guid id);
        Task<NewsDto[]> GetNewsAsync(int skipNewsCount, int takeNewsCount);
        Task<int> GetNewsCountAsync();
        Task DeleteNewsAsync(Guid id);
        Task ChangeNewsAsync(Guid id, CreateNewsDto newNewsData);
    }
}
