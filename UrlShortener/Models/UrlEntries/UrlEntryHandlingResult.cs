namespace UrlShortener.Models.UrlEntries
{
    public class UrlEntryHandlingResult
    {
        public bool Success { get; set; }

        public string Errors { get; set; }

        public UrlEntryItemModel Entry { get; set; }

        public UrlEntryHandlingResult(string errors)
        {
            Success = false;
            Errors = errors;
        }

        public UrlEntryHandlingResult(UrlEntryItemModel entry)
        {
            Success = true;
            Entry = entry;
        }
    }
}