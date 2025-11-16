using Microsoft.EntityFrameworkCore;
using PrasTestJobData;
using PrasTestJobData.Entities;
using PrasTestJobDTO;
using PrasTestJobServices.Abstract;

namespace PrasTestJobServices.Implementations
{
    public class NewsServices : INewsServices
    {
        readonly PrasTestJobContext _dbContext;

        public NewsServices(PrasTestJobContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeNewsAsync(Guid id, CreateNewsDto newNewsData)
        {
            var news = await _dbContext.News.FindAsync(id);
            news.Text = newNewsData.Text;
            news.Headline = newNewsData.Headline;
            news.SubTitle = newNewsData.SubTitle;
            news.ImageType = newNewsData.ImageType;
            news.ImageData = newNewsData.ImageData;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Guid> CreateNewsAsync(CreateNewsDto newNews)
        {
            var addedNews = new News
            {
                Text = newNews.Text,
                Headline = newNews.Headline,
                SubTitle = newNews.SubTitle,
                ImageData = newNews.ImageData,
                ImageType = newNews.ImageType
            };
            await _dbContext.News.AddAsync(addedNews);
            await _dbContext.SaveChangesAsync();
            return addedNews.Id;
        }

        public async Task DeleteNewsAsync(Guid id)
        {
            var news = await _dbContext.News.FindAsync(id);
            _dbContext.News.Remove(news);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<NewsDto[]> GetNewsAsync(int skipNewsCount, int takeNewsCount)
        {
            var news = await _dbContext.News
                .AsNoTracking()
                .Skip(skipNewsCount)
                .Take(takeNewsCount)
                .Select(n => new NewsDto 
                { 
                    Text = n.Text,
                    Headline = n.Headline,
                    SubTitle = n.SubTitle,
                    ImageData = Convert.ToBase64String(n.ImageData),
                    ImageType = n.ImageType
                })
                .ToArrayAsync();
            if (news is null)
                return Array.Empty<NewsDto>();
            return news;
        }

        public async Task<NewsDto?> GetNewsByIdAsync(Guid id)
        {
            var news = await _dbContext.News.FindAsync(id);
            return news is null ? null : new NewsDto
            {
                Text = news.Text,
                Headline = news.Headline,
                SubTitle = news.SubTitle,
                ImageData = Convert.ToBase64String(news.ImageData),
                ImageType = news.ImageType
            };
        }

        public async Task<int> GetNewsCountAsync()
        {
            return await _dbContext.News.CountAsync();
        }
    }
}
