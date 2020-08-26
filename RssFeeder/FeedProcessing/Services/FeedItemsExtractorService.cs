using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using FeedProcessing.Interfaces;
using FeedProcessing.Models;

namespace FeedProcessing.Services
{
    public class FeedItemsExtractorService : IFeedItemsExtractorService
    {
        public List<Feed> Extract(string feedUrls)
        {
            var syndicationFeeds = GetSyndicationFeeds(feedUrls);
            var processedFeed = new List<Feed>();

            if (syndicationFeeds.Count == 0)
                return processedFeed;

            for (var i = 0; i < syndicationFeeds.Count; i++)
            {
                var counter = 1;
                var feed = new Feed()
                {
                    Name = syndicationFeeds[i].Title.Text,
                    Url = feedUrls.Split(',')[i],
                    FeedItems = new List<FeedItem>()
                };
                foreach (var item in syndicationFeeds[i].Items)
                {
                    feed.FeedItems.Add(new FeedItem
                    {
                        Id = counter,
                        Uri = item.Links[0].Uri.AbsoluteUri,
                        Title = item.Title.Text,
                        PublicationDate = item.PublishDate.DateTime.ToString("dd.MM.yyyy HH:mm:ss"),
                        Description = item.Summary.Text
                    });
                    counter++;
                }
                processedFeed.Add(feed);
            }
            
            return processedFeed;
        }

        private static List<SyndicationFeed> GetSyndicationFeeds(string feedUrls)
        {
            var splitFeedUrls = feedUrls.Split(',');
            var syndicationFeeds = new List<SyndicationFeed>();
            foreach (var item in splitFeedUrls)
            {
                var feedReader = XmlReader.Create(item);
                syndicationFeeds.Add(SyndicationFeed.Load(feedReader));
            }
            return syndicationFeeds;
        }
    }
}
