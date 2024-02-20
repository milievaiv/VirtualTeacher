namespace VirtualTeacher.Models.ViewModel.Search;

public class SearchResult
{
    public int PageId { get; set; }
    public string Title { get; set; }
    public string Snippet { get; set; } // Add snippet property
    public string Url { get; set; }
}
