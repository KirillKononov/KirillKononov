using System;
using System.Collections.Generic;
using System.Diagnostics;
using FeedProcessing.Interfaces;
using FeedProcessing.Models;
using Microsoft.AspNetCore.Mvc;
using RssFeeder.Models;

namespace RssFeeder.Controllers
{
    public class FeedController : Controller
    {
        private readonly string _pathToConfigurationFile = Environment.CurrentDirectory + @"\feeddata.json";

        private readonly IFeedItemsExtractorService _feedItemsExtractorService;

        private readonly IFeedDataSerializerService _feedDataSerializerService;

        public FeedController(IFeedItemsExtractorService feedItemsExtractorService,
            IFeedDataSerializerService feedDataSerializerService)
        {
            _feedItemsExtractorService = feedItemsExtractorService;
            _feedDataSerializerService = feedDataSerializerService;
        }

        public IActionResult Index()
        {
            var feedData = GetFeedData();
            var tuple = new Tuple<FeedData, List<Feed>>(
                new FeedData()
                {
                    FeedUrls = feedData.FeedUrls,
                    UpdateTimeInSeconds = feedData.UpdateTimeInSeconds
                },
                _feedItemsExtractorService.Extract(GetFeedData().FeedUrls));
            return View(tuple);
        }

        //GET - Feeds
        public JsonResult GetAll()
        {
            var feeds = _feedItemsExtractorService.Extract(GetFeedData().FeedUrls);

            return Json(feeds);
        }

        //GET - Settings
        public IActionResult Settings()
        {
            return View(GetFeedData());
        }

        //PUT - Settings
        [HttpPost]
        public IActionResult Settings(FeedData feedData)
        {
            _feedDataSerializerService.Serialize(feedData, _pathToConfigurationFile);
            return RedirectToAction(nameof(Index));
        }

        //GET - Post
        public IActionResult Post()
        {
            return View();
        }

        //POST - Post
        [HttpPost]
        public IActionResult Post(FeedData feedData)
        {
            _feedDataSerializerService.SerializeForPost(feedData, _pathToConfigurationFile);
            return RedirectToAction(nameof(Index));
        }

        //DELETE
        [HttpDelete]
        public IActionResult Delete(string url)
        {
            _feedDataSerializerService.SerializeForDelete(url, _pathToConfigurationFile);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private FeedData GetFeedData()
        {
            return _feedDataSerializerService.Deserialize(_pathToConfigurationFile);
        }
    }
}
