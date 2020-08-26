using FeedProcessing.Models;

namespace FeedProcessing.Interfaces
{
    public interface IFeedDataSerializerService
    {
        FeedData Deserialize(string pathToConfigurationFile);

        void SerializeForPost(FeedData feedData, string pathToConfigurationFile);

        void SerializeForDelete(string feedUrl, string pathToConfigurationFile);

        void Serialize(FeedData feedData, string pathToConfigurationFile);
    }
}