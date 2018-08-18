/*
 * CookieManager
 * 
 * Show the 'Accept cookie' dialog and provide javascript methods for getting and setting cookies.
 * 
 */

var CookieDialogId = "#cookieMessage";

$(document).ready(dialog_ShowCookieMessage());

function dialog_ShowCookieMessage() {
    if (GetCookieValue("IAcceptCookie") == "") {
        $(CookieDialogId).modal('show');
    }
}

function cookieMessage_click() {
    SetCookie("IAcceptCookie", true, 365);
    $(CookieDialogId).modal('hide');
}

// Javascript pocket reference O'Reilly
// Store the name/value pair as a cookie with suitable encoding.
function SetCookie(strName, anyValue, intDaysActive) {
    var cookie = strName + "=" + encodeURIComponent(anyValue);

    if (this.GetCookieValue(strName) != "") {
        this.DeleteCookie(strName);
    }

    if (typeof intDaysActive == "number") {
        cookie += "; max-age=" + (intDaysActive * 60 * 60 * 24);
    }

    document.cookie += cookie;
}

function DeleteCookie(strName) {
    this.setCookie(strName, '', 0);
}

function GetCookies() {
    var objResultCookies = {}; // empty object
    var objsAllCookies = document.cookie;

    if (objsAllCookies == "") {
        objResultCookies = {}; // empty object;
    }
    else {
        // split the cookies into parts
        var strsCookies = objsAllCookies.split("; ");

        for (var i = 0; i < strsCookies.length; i++) {
            var strCookie = strsCookies[i];
            var intSplit = strCookie.indexOf("=");
            var strName = strCookie.substring(0, intSplit);
            var anyValue = strCookie.substring(intSplit + 1);

            // store the value
            objResultCookies[strName] = decodeURIComponent(anyValue);
        }
    }

    return objResultCookies;
}

function GetCookieValue(strCookieName) {
    var anyResult = "";
    var objAllCookie = document.cookie;

    if (objAllCookie == "") {
        anyResult = "";
    }
    else {
        // split the cookies into parts
        var strsCookieList = objAllCookie.split("; ");

        for (var i = 0; i < strsCookieList.length; i++) {
            var cookie = strsCookieList[i];
            var p = cookie.indexOf("=");
            var strItemName = cookie.substring(0, p);

            if (strItemName == strCookieName) {
                var anyValue = cookie.substring(p + 1);

                // store the value
                anyResult = decodeURIComponent(anyValue);
                break;
            }
            
        }
    }
    
    return anyResult;
}