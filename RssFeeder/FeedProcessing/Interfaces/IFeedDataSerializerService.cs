using FeedProcessing.Models;

namespace FeedProcessing.Interfaces
{
    public interface IFeedDataSerializerService
    {
        FeedData Deserialize(string pathToConfigurationFile);

        void Serialize(FeedData feedData, string pathToConfigurationFile);
    }
}