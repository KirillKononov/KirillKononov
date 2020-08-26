using System;
using System.Xml.Serialization;

namespace FeedProcessing.Models
{
    [Serializable]
    public class FeedData
    {
        public string FeedUrls { get; set; }

        public int UpdateTimeInSeconds { get; set; }
    }
}
