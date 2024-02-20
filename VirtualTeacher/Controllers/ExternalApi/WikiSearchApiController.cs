using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Constants;
using VirtualTeacher.Services;

namespace VirtualTeacher.Controllers.ExternalApi
{
    [ApiController]
    [Route("api/wiki-search")]
    public class WikiSearchApiController : ControllerBase
    {
        private readonly MediaWikiActionService _wikiService;

        public WikiSearchApiController(MediaWikiActionService wikiService)
        {
            _wikiService = wikiService;
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest(Messages.WikiEmptySearchQuery);
                }

                var searchResponse = _wikiService.GetWikiMediaSearchRequest(query);
                if (searchResponse == null || searchResponse.Query == null || searchResponse.Query.Search == null || searchResponse.Query.Search.Length == 0)
                {
                    return NotFound("No search results found.");
                }

                return Ok(searchResponse.Query.Search); // Return search results as JSON data
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
