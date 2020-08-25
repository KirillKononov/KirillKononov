using System;
using System.Xml.Serialization;

namespace FeedProcessing.Models
{
    [Serializable, XmlRoot("FeedData")]
    public class FeedData
    {
        public string FeedUrl { get; set; }

        public int UpdateTimeInSeconds { get; set; }
    }
}
