using System.Collections.Generic;
using FeedProcessing.Models;

namespace FeedProcessing.Interfaces
{
    public interface IFeedItemsExtractorService
    {
        List<Feed> Extract(string feedUrls);
    }
}