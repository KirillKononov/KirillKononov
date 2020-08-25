class FeedList {
    load(): void {

        $.getJSON("/Feed/GetAll",
            (data) => {
                this.displayFeeds(data);
            });
    }

    private displayFeeds(feeds: Array<FeedItem>): void {
        let table = '<table class="table">';
        for (let i = 0; i < feeds.length; i++) {
            const tableRow = `<tr>
                <td>${feeds[i].id}</td>
                <td><a href="${feeds[i].uri}" target="_blank">${feeds[i].title}</a></td>
                <td>${feeds[i].publicationDate}</td>
                <td><a href="javascript:void(0)" onclick="document.getElementById('${i}').style.display='block';">Описание</a>
                <div id="${i}" style="display: none">${this.displayDescription(feeds[i].description)}
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

class FeedItem {
    id: number;
    uri: string;
    title: string;
    publicationDate: string;
    description: string;
}

window.onload = () => {
    const feedList = new FeedList();
    feedList.load();
    const turnOnFormatting = "Включить форматирование";
    const turnOffFormatting = "Выключить форматирование";
    $("#format").click(() => {
        var button = document.getElementById("format");
        if (button.innerHTML === turnOnFormatting) {
            button.innerHTML = turnOffFormatting;
            feedList.load();
        } else {
            button.innerHTML = turnOnFormatting;
            feedList.load();
        }
    });
};