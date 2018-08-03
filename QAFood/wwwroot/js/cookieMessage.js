$(document).ready(Dialog_ShowCookieMessage());

function Dialog_ShowCookieMessage() {    
    if (this.getCookie("IAcceptCookie") == "") {
        $("#cookieMessage").modal('show');
    }
}

function cookieMessage_click() {
    if (this.getCookie("IAcceptCookie") != "") {
        this.deleteCookie("IAcceptCookie");
    }

    this.setCookie("IAcceptCookie", true, 365);
}

// Javascript pocket reference O'Reilly
// Store the name/value pair as a cookie with suitable encoding.
function setCookie(name, value, daysLife) {
    if (this.getCookie(name) != "") {
        this.deleteCookie(name);
    }

    var cookie = name + "=" + encodeURIComponent(value);
    if (typeof daysLife === "number") {
        cookie += "; max-age=" + (daysLife * 60 * 60 * 24);
    }
    document.cookie += cookie;
}

function deleteCookie(name) {
    this.setCookie(name, '', 0);
}

function getCookies() {
    var cookies = {};
    var all = document.cookie;
    if (all === "") {
        return cookies;
    }

    // split the cookies into parts
    var cookieList = all.split("; ");

    for (var i = 0; i < cookieList.length; i++) {
        var cookie = cookieList[i];
        var p = cookie.indexOf("=");
        var name = cookie.substring(0, p);
        var value = cookie.substring(p + 1);

        // store the value
        cookies[name] == decodeURIComponent(value);
    }

    return cookies;
}

function getCookie(name) {
    var result = "";

    var all = document.cookie;
    if (all === "") {
        result = "";
    }
    else {
        // split the cookies into parts
        var cookieList = all.split("; ");

        for (var i = 0; i < cookieList.length; i++) {
            var cookie = cookieList[i];
            var p = cookie.indexOf("=");
            var name = cookie.substring(0, p);
            var value = cookie.substring(p + 1);

            // store the value
            result = decodeURIComponent(value);
            break;
        }
    }
    
    return result;
}