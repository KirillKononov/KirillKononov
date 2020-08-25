var FeedList = /** @class */ (function () {
    function FeedList() {
    }
    FeedList.prototype.load = function () {
        var _this = this;
        $.getJSON("/Feed/GetAll", function (data) {
            _this.displayFeeds(data);
        });
    };
    FeedList.prototype.displayFeeds = function (feeds) {
        var table = '<table class="table">';
        for (var i = 0; i < feeds.length; i++) {
            var tableRow = "<tr>\n                <td>" + feeds[i].id + "</td>\n                <td><a href=\"" + feeds[i].uri + "\" target=\"_blank\">" + feeds[i].title + "</a></td>\n                <td>" + feeds[i].publicationDate + "</td>\n                <td><a href=\"javascript:void(0)\" onclick=\"document.getElementById('" + i + "').style.display='block';\">\u041E\u043F\u0438\u0441\u0430\u043D\u0438\u0435</a>\n                <div id=\"" + i + "\" style=\"display: none\">" + this.displayDescription(feeds[i].description) + "\n                <br />\n                <a href=\"javascript:void(0)\" onclick=\"document.getElementById('" + i + "').style.display='none';\">\u0421\u0432\u0435\u0440\u043D\u0443\u0442\u044C</a>\n                </div>\n                </td>\n                </tr>";
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
var FeedItem = /** @class */ (function () {
    function FeedItem() {
    }
    return FeedItem;
}());
window.onload = function () {
    var feedList = new FeedList();
    feedList.load();
    var turnOnFormatting = "Включить форматирование";
    var turnOffFormatting = "Выключить форматирование";
    $("#format").click(function () {
        var button = document.getElementById("format");
        if (button.innerHTML === turnOnFormatting) {
            button.innerHTML = turnOffFormatting;
            feedList.load();
        }
        else {
            button.innerHTML = turnOnFormatting;
            feedList.load();
        }
    });
};
//# sourceMappingURL=index.js.map