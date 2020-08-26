const url = document.getElementById("urlCheck") as HTMLInputElement;
const urlMessage = document.getElementById("urlCheck-message");
const submitButton = document.querySelector('form input[type="submit"]') as HTMLInputElement;

url.addEventListener("input", () => {
    const str = url.value;
    let message = "";
    let disable = true;
    if (!isValidUrl(str)) {
        message = "Введенная строка не является URL адресом";
    }
    else {
        disable = false;
    }
    urlMessage.innerText = message;
    submitButton.disabled = disable;
});

function isValidUrl(string: string) {
    const res = string.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);
    if (res == null)
        return false;
    else
        return true;
};