var FeedList = /** @class */ (function () {
    function FeedList() {
    }
    FeedList.prototype.load = function () {
        var _this = this;
        $.getJSON("/Feed/GetAll", function (data) {
            _this.setCurrentFeed(data);
            _this.displayFeeds();
        });
    };
    FeedList.prototype.setCurrentFeed = function (feeds) {
        var selectedNewsFeed = $("#newsFeed :selected").text();
        var temp = feeds.filter(function (item) { return item.name === selectedNewsFeed; });
        this.currentFeed = temp[0];
    };
    FeedList.prototype.displayFeeds = function () {
        var feedItems = this.currentFeed.feedItems;
        var table = '<table class="table">';
        for (var i = 0; i < feedItems.length; i++) {
            var tableRow = "<tr>\n                <td>" + feedItems[i].id + "</td>\n                <td><a href=\"" + feedItems[i].uri + "\" target=\"_blank\">" + feedItems[i].title + "</a></td>\n                <td>" + feedItems[i].publicationDate + "</td>\n                <td><a href=\"javascript:void(0)\" onclick=\"document.getElementById('" + i + "').style.display='block';\">\u041E\u043F\u0438\u0441\u0430\u043D\u0438\u0435</a>\n                <div id=\"" + i + "\" style=\"display: none\">" + this.displayDescription(feedItems[i].description) + "\n                <br />\n                <a href=\"javascript:void(0)\" onclick=\"document.getElementById('" + i + "').style.display='none';\">\u0421\u0432\u0435\u0440\u043D\u0443\u0442\u044C</a>\n                </div>\n                </td>\n                </tr>";
            table += tableRow;
        }
        table += "</table>";
        $("#content").html(table);
    };
    FeedList.prototype.displayDescription = function (description) {
        var formattingType = "Включить форматирование";
        if (document.getElementById("format").innerHTML === formattingType) {
            description = description.replace(/<\/?[^>]+(>|$)/g, "");
        }
        return description;
    };
    return FeedList;
}());
var ButtonProcessing = /** @class */ (function () {
    function ButtonProcessing(feedList) {
        this.feedList = feedList;
    }
    ButtonProcessing.prototype.formatting = function () {
        var _this = this;
        var turnOnFormatting = "Включить форматирование";
        var turnOffFormatting = "Выключить форматирование";
        $("#format").click(function () {
            var button = document.getElementById("format");
            if (button.innerHTML === turnOnFormatting) {
                button.innerHTML = turnOffFormatting;
                _this.feedList.load();
            }
            else {
                button.innerHTML = turnOnFormatting;
                _this.feedList.load();
            }
        });
    };
    ButtonProcessing.prototype.dropDownList = function () {
        var _this = this;
        var selectedValue = document.getElementById("newsFeed");
        selectedValue.onchange = function () {
            _this.feedList.load();
        };
    };
    ButtonProcessing.prototype.delete = function () {
        var _this = this;
        $("#deleteButton").click(function () {
            if (confirm("\u0412\u044B \u0443\u0432\u0435\u0440\u0435\u043D\u044B, \u0447\u0442\u043E \u0445\u043E\u0442\u0438\u0442\u0435 \u0443\u0434\u0430\u043B\u0438\u0442\u044C \u043B\u0435\u043D\u0442\u0443 " + _this.feedList.currentFeed.name + "?")) {
                $.ajax({
                    url: "/Feed/Delete?url=" + _this.feedList.currentFeed.url,
                    type: "DELETE",
                });
                location.reload();
                alert("\u041B\u0435\u043D\u0442\u0430 " + _this.feedList.currentFeed.name + " \u0431\u044B\u043B\u0430 \u0443\u0441\u043F\u0435\u0448\u043D\u043E \u0443\u0434\u0430\u043B\u0435\u043D\u0430");
            }
        });
    };
    return ButtonProcessing;
}());
window.onload = function () {
    var feedList = new FeedList();
    var buttonProcessing = new ButtonProcessing(feedList);
    feedList.load();
    buttonProcessing.formatting();
    buttonProcessing.dropDownList();
    buttonProcessing.delete();
};
//# sourceMappingURL=index.js.map