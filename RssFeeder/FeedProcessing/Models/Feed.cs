using System.Collections.Generic;

namespace FeedProcessing.Models
{
    public class Feed
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public List<FeedItem> FeedItems { get; set; }
    }
}
