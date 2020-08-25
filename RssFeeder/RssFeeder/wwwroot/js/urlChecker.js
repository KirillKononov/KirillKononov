var url = document.getElementById("urlCheck");
var urlMessage = document.getElementById("urlCheck-message");
var submitButton = document.querySelector('form input[type="submit"]');
url.addEventListener("input", function () {
    var str = url.value;
    var message = "";
    var disable = true;
    if (!isValidUrl(str)) {
        message = "Введенная строка не является URL адресом";
    }
    else {
        disable = false;
    }
    urlMessage.innerText = message;
    submitButton.disabled = disable;
});
function isValidUrl(string) {
    var res = string.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);
    if (res == null)
        return false;
    else
        return true;
}
;
//# sourceMappingURL=urlChecker.js.map