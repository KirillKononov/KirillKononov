using System;
using System.IO;
using FeedProcessing.Interfaces;
using FeedProcessing.Models;
using Newtonsoft.Json;

namespace FeedProcessing.Services
{
    public class FeedDataSerializerService : IFeedDataSerializerService
    {
        public FeedData Deserialize(string pathToConfigurationFile)
        {

            var fileData = File.ReadAllText(pathToConfigurationFile);
            var feedData = JsonConvert.DeserializeObject<FeedData>(fileData);
            return feedData;
            
        }

        public void SerializeForPost(FeedData feedData, string pathToConfigurationFile)
        {
            var previousFeedData = Deserialize(pathToConfigurationFile);

            if (previousFeedData.FeedUrls.Contains(feedData.FeedUrls))
                return;

            if (previousFeedData.FeedUrls.Length == 0)
                previousFeedData.FeedUrls = feedData.FeedUrls;
            else
                previousFeedData.FeedUrls += "," + feedData.FeedUrls;

            Serialize(previousFeedData, pathToConfigurationFile);
        }

        public void SerializeForDelete(string feedUrl, string pathToConfigurationFile)
        {
            var feedData = Deserialize(pathToConfigurationFile);

            if (!feedData.FeedUrls.Contains(feedUrl))
                return;

            feedData.FeedUrls = feedData.FeedUrls.Replace(feedUrl, "");
            feedData.FeedUrls = feedData.FeedUrls.Trim(',');

            if (feedData.FeedUrls.Contains(",,")) 
                feedData.FeedUrls = feedData.FeedUrls.Replace(",,", ",");

            Serialize(feedData, pathToConfigurationFile);
        }

        public void Serialize(FeedData feedData, string pathToConfigurationFile)
        {
            var jsonString = JsonConvert.SerializeObject(feedData);
            File.WriteAllText(pathToConfigurationFile, jsonString);
        }
    }
}
