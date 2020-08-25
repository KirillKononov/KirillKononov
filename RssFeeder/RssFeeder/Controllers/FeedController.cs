using System;
using System.Diagnostics;
using FeedProcessing.Interfaces;
using FeedProcessing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RssFeeder.Models;

namespace RssFeeder.Controllers
{
    public class FeedController : Controller
    {
        private readonly ILogger<FeedController> _logger;

        private readonly string _pathToConfigurationFile = Environment.CurrentDirectory + @"\FeedData.xml";

        private readonly IFeedItemsExtractorService _feedItemsExtractorService;

        private readonly IFeedDataSerializerService _feedDataSerializerService;

        public FeedController(IFeedItemsExtractorService feedItemsExtractorService,
            IFeedDataSerializerService feedDataSerializerService,
            ILogger<FeedController> logger)
        {
            _feedItemsExtractorService = feedItemsExtractorService;
            _feedDataSerializerService = feedDataSerializerService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(GetFeedData());
        }

        //GET - Feeds
        public JsonResult GetAll()
        {
            var feeds = _feedItemsExtractorService.Extract(GetFeedData().FeedUrl);

            return Json(feeds);
        }

        //GET - EDIT
        public IActionResult Settings()
        {
            return View(GetFeedData());
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings(FeedData feedData)
        {
            _feedDataSerializerService.Serialize(feedData, _pathToConfigurationFile);
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
