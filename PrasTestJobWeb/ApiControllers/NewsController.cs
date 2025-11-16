using System.ComponentModel.Design;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrasTestJobDTO;
using PrasTestJobServices.Abstract;
using PrasTestJobWeb.Models;

namespace PrasTestJobWeb.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        readonly INewsServices _newsServices;

        public NewsController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNewsById(Guid id)
        {
            try
            {
                var news = await _newsServices.GetNewsByIdAsync(id);
                if (news is null)
                    return NotFound();
                return Ok(news);
            }
            catch (Exception)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NewsDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNewsFeed(int skipCount, int takeCount)
        {
            try
            {
                var newsFeed = await _newsServices.GetNewsAsync(skipCount, takeCount);
                return Ok(newsFeed);
            }
            catch (Exception)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNews([FromForm] NewNewsRequest newNews)
        {
            try
            {
                using(var ms = new MemoryStream())
                {
                    await newNews.ImageFormFile.CopyToAsync(ms);
                    var newNewsDto = new CreateNewsDto
                    {
                        Text = newNews.Text,
                        SubTitle = newNews.SubTitle,
                        Headline = newNews.Headline,
                        ImageType = newNews.ImageFormFile.ContentType,
                        ImageData = ms.ToArray()
                    };
                    var newNewsId = await _newsServices.CreateNewsAsync(newNewsDto);
                    return Created($"api/News/{newNewsId}", new { Id = newNewsId });
                };
            }
            catch (Exception)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DelNews(Guid id)
        {
            try
            {
                await _newsServices.DeleteNewsAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeNews(Guid id, [FromForm] NewNewsRequest newNews)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    await newNews.ImageFormFile.CopyToAsync(ms);
                    var newNewsDto = new CreateNewsDto
                    {
                        Text = newNews.Text,
                        SubTitle = newNews.SubTitle,
                        Headline = newNews.Headline,
                        ImageType = newNews.ImageFormFile.ContentType,
                        ImageData = ms.ToArray()
                    };
                    await _newsServices.ChangeNewsAsync(id, newNewsDto);
                    return NoContent();
                };
            }
            catch (Exception)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }
    }
}
