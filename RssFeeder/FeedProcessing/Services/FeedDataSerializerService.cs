using System.IO;
using System.Xml.Serialization;
using FeedProcessing.Interfaces;
using FeedProcessing.Models;

namespace FeedProcessing.Services
{
    public class FeedDataSerializerService : IFeedDataSerializerService
    {
        public FeedData Deserialize(string pathToConfigurationFile)
        {
            var serializer = new XmlSerializer(typeof(FeedData));
            var stream = new FileStream(pathToConfigurationFile, FileMode.Open);
            var feedData = (FeedData)serializer.Deserialize(stream);
            stream.Close();
            return feedData;
        }

        public void Serialize(FeedData feedData, string pathToConfigurationFile)
        {
            var serializer = new XmlSerializer(typeof(FeedData));
            var stream = new FileStream(pathToConfigurationFile, FileMode.Open); 
            serializer.Serialize(stream, feedData); 
            stream.Close();
        }
    }
}
