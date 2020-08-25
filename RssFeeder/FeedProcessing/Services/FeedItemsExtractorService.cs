using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using FeedProcessing.Interfaces;
using FeedProcessing.Models;

namespace FeedProcessing.Services
{
    public class FeedItemsExtractorService : IFeedItemsExtractorService
    {
        public List<FeedItem> Extract(string feedUrl)
        {
            var unprocessedFeedItems = GetUnprocessedFeedItems(feedUrl);
            var processedFeedItems = new List<FeedItem>();

            if (unprocessedFeedItems == null)
                return processedFeedItems;

            var counter = 1;
            foreach (var item in unprocessedFeedItems.Items)
            {
                processedFeedItems.Add(new FeedItem
                {
                    Id = counter,
                    Uri = item.Links[0].Uri.AbsoluteUri,
                    Title = item.Title.Text,
                    PublicationDate = item.PublishDate.DateTime.ToString("dd.MM.yyyy HH:mm:ss"),
                    Description = item.Summary.Text
                });
                counter++;
            }

            return processedFeedItems;
        }

        private static SyndicationFeed GetUnprocessedFeedItems(string feedUrl)
        {
            var feedReader = XmlReader.Create(feedUrl);
            return SyndicationFeed.Load(feedReader);
        }
    }
}
