using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using VirtualTeacher.Models.ViewModel.Search;

namespace VirtualTeacher.Services
{
    public class MediaWikiActionService
    {
        private readonly Serilog.ILogger _logger;

        public MediaWikiActionService()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public WikiMediaSearchResponse? GetWikiMediaSearchRequest(string searchQuery, string action = "query", string list = "search", string format = "json")
        {
            // Check if the search query is empty
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                _logger.Warning("Search query is empty. Please provide a valid search query.");
                return null;
            }

            string apiUrl = "https://en.wikipedia.org/w/api.php";

            var client = new RestClient(apiUrl);

            var request = new RestRequest();
            request.AddParameter("action", action);
            request.AddParameter("list", list);
            request.AddParameter("srsearch", searchQuery);
            request.AddParameter("format", format);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                _logger.Information("Request successful: {ResponseUri}", response.ResponseUri);

                var result = JsonConvert.DeserializeObject<WikiMediaSearchResponse>(response.Content);

                if (result == null || result.Query == null || result.Query.Search == null || result.Query.Search.Length == 0)
                {
                    _logger.Warning("Search response is null or empty.");
                    return null;
                }
                return result;
            }
            else
            {
                _logger.Error("Error: {StatusCode} - {StatusDescription}", response.StatusCode, response.StatusDescription);
                return null;
            }
        }
    }
}
