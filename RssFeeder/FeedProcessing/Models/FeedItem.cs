namespace FeedProcessing.Models
{
    public class FeedItem
    {
        public int Id { get; set; }

        public string Uri { get; set; }

        public string Title { get; set; }

        public string PublicationDate { get; set; }

        public string Description { get; set; }
    }
}
