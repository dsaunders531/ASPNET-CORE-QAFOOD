/*
 * Ajax functions for the ReviewStart page.
 * 
 * The getting and saving of data is all made on one page using the api.
 * 
 */

var CursorTimer;
var BusyTimer;

// Get the test result when the document loads.
$(document).ready(foodItemSelect_Change(document.getElementById("SelectedFoodItemId")));

// Get the data for the selected item.
function foodItemSelect_Change(foodItemSelect) {
    var selectedId = foodItemSelect.value;
    var foodParcelId = document.getElementsByName("FoodParcelId")[0].value;
    var testId = document.getElementsByName("TestId")[0].value;
    var jsonBody = JSON.stringify({ FoodParcelId: foodParcelId, SelectedFoodItemId: selectedId, TestId: testId });

    HideSaved();
    ShowWorking();

    // Show the food image for the item.
    var imageClass = "foodImage" + selectedId;
    var imageEles = document.getElementsByClassName("foodImage");
    for (var i = 0; i < imageEles.length; i++) {
        if ($(imageEles[i]).hasClass(imageClass) == true) {
            if ($(imageEles[i]).hasClass("hidden") == true) {
                $(imageEles[i]).removeClass("hidden");
            }
        }
        else if ($(imageEles[i]).hasClass("hidden") == false) {
            $(imageEles[i]).addClass("hidden");
        }
    }

    // Get the data for the item.
    $.ajax({
        url: '/api/TestResultItem/',
        type: 'POST',
        data: jsonBody,
        dataType: 'json',
        contentType: 'application/json; charset=UTF-8',
        success: function (data, textStatus, jqXHR) { ShowResult(data); },
        error: function (jqXHR, textStatus, errorThrown) { ShowErrorResult(jqXHR, textStatus, errorThrown); }
    });
}

// Set the form controls to disabled and show a spinner
function ShowWorking() {
    if ($("#btnSubmit").hasClass("disabled") == false) {
        $("#btnSubmit").addClass("disabled");
    }

    if ($("#btnReset").hasClass("disabled") == false) {
        $("#btnReset").addClass("disabled");
    }

    // disable the form controls
    if ($("#SelectedFoodItemId").hasClass("disabled") == false) {
        $("#SelectedFoodItemId").addClass("disabled");
        document.getElementById("SelectedFoodItemId").disabled = "disabled";
    }

    // all the option buttons
    var optionEles = $("form input");
    for (var i = 0; i < optionEles.length; i++) {
        var optionEle = optionEles[i];

        if (optionEle.disabled == "" || optionEle.disabled == false) {
            optionEle.disabled = "disabled";
        }
    }

    // When the process takes a while show the busy cursor first then a message or other visual indicator.
    // The timings are the recommended intervals 1 second and 3 seconds.
    CursorTimer = window.setTimeout(function () { timer_ShowBusyCursor(); }, 1000);    
}

// Set the form controls to enabled and hide the spinner
function HideWorking() {    
    // enable the form controls
    // disable the form controls
    if ($("#SelectedFoodItemId").hasClass("disabled")) {
        $("#SelectedFoodItemId").removeClass("disabled");
        document.getElementById("SelectedFoodItemId").disabled = "";
    }

    // all the option buttons
    var optionEles = $("form input");
    for (var i = 0; i < optionEles.length; i++) {
        var optionEle = optionEles[i];

        if (optionEle.disabled == "disabled" || optionEle.disabled == true) {
            optionEle.disabled = "";
        }
    }

    if ($("#btnSubmit").hasClass("disabled")) {
        $("#btnSubmit").removeClass("disabled");
    }

    if ($("#btnReset").hasClass("disabled")) {
        $("#btnReset").removeClass("disabled");
    }

    // Stop the timers.
    window.clearTimeout(CursorTimer);
    window.clearTimeout(BusyTimer);    

    // Get the document back to its original state.
    timer_Ended();
}

function timer_ShowBusyCursor() {
    if ($("body").hasClass("busy-cursor") == false) {
        $("body").addClass("busy-cursor");
    }   

    // Show the busy timer a few seconds from now.
    BusyTimer = window.setTimeout(function () { timer_ShowBusyMessage(); }, 2000);
}

function timer_ShowBusyMessage() {
    if ($("body").hasClass("busy-cursor")) {
        $("body").removeClass("busy-cursor");
    }    
    $("body").addClass("wait-cursor");
    $("#btnWorking").opacity = 0;
    $("#btnWorking").removeClass("hidden");
    $("#btnWorking").animate({ opacity: '1' }, "slow");
}

function timer_Ended() {
    if ($("body").hasClass("busy-cursor")) {
        $("body").removeClass("busy-cursor");
    }    

    if ($("body").hasClass("wait-cursor")) {
        $("body").removeClass("wait-cursor");
    }    

    if ($("#btnWorking").hasClass("hidden") == false ) {
        $("#btnWorking").animate({ opacity: '0' }, "slow");
        $("#btnWorking").addClass("hidden");
    }    
}

function ShowSaved() {
    $("#alertSaved").opacity = 0;
    $("#alertSaved").removeClass("hidden");
    $("#alertSaved").animate({ opacity: '1' }, "slow");
}

function HideSaved() {
    $("#alertSaved").animate({ opacity: '0' }, "slow");
    $("#alertSaved").addClass("hidden");
}

function ShowErrorResult(jqXHR, textStatus, errorThrown) {
    HideWorking();
    alert(jqXHR.statusText + ". Something went wrong using the api. " + textStatus + " " + errorThrown);
}

// Display the results of the ajax query.
// If the user has saved a test previously, the values will be displayed here.
function ShowResult(data) {
    // find the relevant item in the html and set the option value.
    var eleName;

    // find the element to change. There is a lot of junk in the returned data as it is used for other things.
    // its easier than using data.presentationValue and having repeated loops for these input options.
    $.each(data,
        function (key, item) {
            switch (key) {
                case "presentationValue":
                    eleName = key + "_";
                    break;
                case "textureValue":
                    eleName = key + "_";
                    break;
                case "aromaValue":
                    eleName = key + "_";
                    break;
                case "flavourValue":
                    eleName = key + "_";
                    break;
                default:
                    eleName = "";
                    break;
            }

            if (eleName.length > 0) {
                // unset disabled and set the checked property on the appropriate item.
                // note that if the result has not been set (has value 0) then the default is 'Excellent' 5.
                var ele;

                for (var i = 1; i <= 5; i++) {
                    ele = document.getElementById(eleName + i);

                    if (i == item || (item == 0 && i == 5)) {
                        ele.checked = "checked";
                        break;
                    }
                }
            }
        }
    );

    HideWorking();
}

// Save the data via the api.
function btnSubmit_Click() {
    if ($("#btnSubmit").hasClass("disabled")) {
        // do nothing
        return;
    }

    ShowWorking();

    // get the values
    var selectedId = document.getElementById("SelectedFoodItemId").value;
    var foodParcelId = document.getElementsByName("FoodParcelId")[0].value;
    var testId = document.getElementsByName("TestId")[0].value;

    var presentationValue = GetOptionValue("presentationValue");
    var textureValue = GetOptionValue("textureValue");
    var aromaValue = GetOptionValue("aromaValue");
    var flavourValue = GetOptionValue("flavourValue");

    var jsonBody = JSON.stringify({
        FoodParcelId: foodParcelId, SelectedFoodItemId: selectedId, TestId: testId,
        PresentationValue: presentationValue, TextureValue: textureValue,
        AromaValue: aromaValue, FlavourValue: flavourValue
        });

    $.ajax({
        url: '/api/TestResultItem/',
        type: 'PATCH',
        data: jsonBody,
        dataType: 'json',
        contentType: 'application/json; charset=UTF-8',
        success: function (data, textStatus, jqXHR) { SubmitResult(); },
        error: function (jqXHR, textStatus, errorThrown) { ShowErrorResult(jqXHR, textStatus, errorThrown); }
    });
}

function btnReset_Click() {
    if ($("#btnReset").hasClass("disabled")) {
        // do nothing
        return;
    }

    // get the original values from the database
    foodItemSelect_Change(document.getElementById("SelectedFoodItemId"));
}

// get the selected value from an option.
// assumes the option ids are named idName_i
function GetOptionValue(idName) {
    var result = 0;
    var ele;

    for (var i = 1; i <= 5; i++) {
        ele = document.getElementById(idName + "_" + i);

        if (ele.checked == true) {
            result = i;
            break;
        }
    }

    return result;
}

// Show a thanks message to the user.
function SubmitResult() {
    ShowSaved();
    HideWorking();
}