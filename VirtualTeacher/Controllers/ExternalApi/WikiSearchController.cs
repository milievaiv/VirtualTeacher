using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VirtualTeacher.Models.ViewModel.Search;
using VirtualTeacher.Services;

namespace VirtualTeacher.Controllers.ExternalApi
{
    [Route("wiki-search")]
    public class WikiSearchController : Controller
    {
        private readonly MediaWikiActionService _wikiService;

        public WikiSearchController(MediaWikiActionService wikiService)
        {
            _wikiService = wikiService;
        }

        public IActionResult Search(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest("Search query cannot be empty.");
                }

                var searchResponse = _wikiService.GetWikiMediaSearchRequest(query);
                if (searchResponse == null || searchResponse.Query == null || searchResponse.Query.Search == null || searchResponse.Query.Search.Length == 0)
                {
                    return View("NoResults");
                }

                var searchResults = new SearchViewModel
                {
                    Results = new List<SearchResult>()
                };

                foreach (var item in searchResponse.Query.Search)
                {
                    searchResults.Results.Add(new SearchResult
                    {
                        PageId = item.PageId,
                        Title = item.Title,
                        Snippet = item.Snippet,
                        Url = $"https://en.wikipedia.org/wiki/{Uri.EscapeDataString(item.Title)}"
                    });
                }

                return View("SearchResults", searchResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
