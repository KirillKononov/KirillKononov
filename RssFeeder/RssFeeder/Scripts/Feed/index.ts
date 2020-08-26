class FeedList {
    public currentFeed: Feed;

    public load(): void {
        $.getJSON("/Feed/GetAll",
            (data) => {
                this.setCurrentFeed(data);
                this.displayFeeds();
            });
    }

    private setCurrentFeed(feeds: Feed[]): void {
        const selectedNewsFeed = $("#newsFeed :selected").text();
        const temp = feeds.filter(item => item.name === selectedNewsFeed);

        this.currentFeed = temp[0];
    }

    private displayFeeds(): void {
        const feedItems = this.currentFeed.feedItems;

        let table = '<table class="table">';

        for (let i = 0; i < feedItems.length; i++) {
            const tableRow = `<tr>
                <td>${feedItems[i].id}</td>
                <td><a href="${feedItems[i].uri}" target="_blank">${feedItems[i].title}</a></td>
                <td>${feedItems[i].publicationDate}</td>
                <td><a href="javascript:void(0)" onclick="document.getElementById('${i}').style.display='block';">Описание</a>
                <div id="${i}" style="display: none">${this.displayDescription(feedItems[i].description)}
                <br />
                <a href="javascript:void(0)" onclick="document.getElementById('${i}').style.display='none';">Свернуть</a>
                </div>
                </td>
                </tr>`;
            table += tableRow;
        }
        table += "</table>";
        $("#content").html(table);
    }

    private displayDescription(description: string): string {
        const formattingType = "Включить форматирование";

        if (document.getElementById("format").innerHTML === formattingType) {
            description = description.replace(/<\/?[^>]+(>|$)/g, "");
        }

        return description;
    }
}

class ButtonProcessing {
    private feedList: FeedList;

    constructor(feedList: FeedList) {
        this.feedList = feedList;
    }

    public formatting(): void {
        const turnOnFormatting = "Включить форматирование";
        const turnOffFormatting = "Выключить форматирование";
        $("#format").click(() => {
            var button = document.getElementById("format");
            if (button.innerHTML === turnOnFormatting) {
                button.innerHTML = turnOffFormatting;
                this.feedList.load();
            } else {
                button.innerHTML = turnOnFormatting;
                this.feedList.load();
            }
        });
    }

    public dropDownList(): void {
        const selectedValue = document.getElementById("newsFeed");
        selectedValue.onchange = () => {
            this.feedList.load();
        };
    }

    public delete(): void {
        $("#deleteButton").click(() => {
            if (confirm(`Вы уверены, что хотите удалить ленту ${this.feedList.currentFeed.name}?`)) {
                $.ajax({
                    url: `/Feed/Delete?url=${this.feedList.currentFeed.url}`,
                    type: "DELETE",
                });
                location.reload();
                alert(`Лента ${this.feedList.currentFeed.name} была успешно удалена`);
            }
        });
    }
}

type FeedItem = {
    id: number,
    uri: string,
    title: string,
    publicationDate: string,
    description: string,
}

type Feed = {
    name: string,
    url: string,
    feedItems: FeedItem[],
}

window.onload = () => {
    const feedList = new FeedList();
    const buttonProcessing = new ButtonProcessing(feedList);

    feedList.load();
    buttonProcessing.formatting();
    buttonProcessing.dropDownList();
    buttonProcessing.delete();
};