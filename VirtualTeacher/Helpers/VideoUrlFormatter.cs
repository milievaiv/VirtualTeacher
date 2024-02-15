using System.Web;

namespace VirtualTeacher.Helpers
{
    public static class VideoUrlFormatter
    {
        public static string FormatYouTubeUrl(string originalUrl)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
                return string.Empty;

            Uri uriResult;
            bool result = Uri.TryCreate(originalUrl, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result)
                return originalUrl; // Return original URL if it's not a valid HTTP/HTTPS URL

            // Check if the URL is a YouTube watch URL
            if (uriResult.Host.Contains("youtube.com") && uriResult.AbsolutePath.Contains("/watch") && uriResult.Query.Contains("v="))
            {
                var videoId = HttpUtility.ParseQueryString(uriResult.Query).Get("v");
                return $"https://www.youtube.com/embed/{videoId}";
            }

            return originalUrl; // Return original URL if it does not match the expected YouTube watch URL format
        }
    }
}
