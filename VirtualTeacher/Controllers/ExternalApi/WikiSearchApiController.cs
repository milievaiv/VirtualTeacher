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
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(Messages.WikiEmptySearchQuery);
            }

            try
            {
                _wikiService.GetWikiMediaSearchRequest(query);
                return Ok(); // Return the search results
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
