// Get the test result when the document loads.
$(document).ready(foodItemSelect_Change(document.getElementById("SelectedFoodItemId")));

// Get the data for the selected item.
function foodItemSelect_Change(foodItemSelect) {
    var selectedId = foodItemSelect.value;
    var foodParcelId = document.getElementsByName("FoodParcelId")[0].value;
    var testId = document.getElementsByName("TestId")[0].value;
    var apiUrl = "/api/TestResultItem/" + foodParcelId + "/" + selectedId + "/" + testId;

    HideSaved();
    ShowWorking();

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

    $.ajax(
        {
            type: 'GET',
            url: apiUrl,
            error: function (textStatus) { ShowErrorResult(textStatus); },
            success: function (data) { ShowResult(data); }
        }
    );
}

// Set the form controls to disabled and show a spinner
function ShowWorking() {
    $(document).addClass("disabled");
    document.getElementById("working").opacity = 0;
    $(document.getElementById("working")).removeClass("hidden");
    $(document.getElementById("working")).animate({ opacity: '1' }, "slow");
}

// Set the form controls to enabled and hide the spinner
function HideWorking() {
    $(document).removeClass("disabled");
    $(document.getElementById("working")).animate({ opacity: '0' }, "slow");
    $(document.getElementById("working")).addClass("hidden");
}

function ShowSaved() {
    document.getElementById("saved").opacity = 0;
    $(document.getElementById("saved")).removeClass("hidden");
    $(document.getElementById("saved")).animate({ opacity: '1' }, "slow");
}

function HideSaved() {
    $(document.getElementById("saved")).animate({ opacity: '0' }, "slow");
    $(document.getElementById("saved")).addClass("hidden");
}

function ShowErrorResult(textStatus) {
    alert("Something went wrong using the api. " + textStatus);
    HideWorking();
}

// Display the results of the ajax query.
// If the user has saved a test previously, the values will be displayed here.
function ShowResult(data) {
    // find the relevant item in the html and set the option value.
    var eleName;

    // find the element to change. There is a lot of junk in the returned data as it is used for other things.
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
                        ele.disabled = "";
                        ele.checked = "checked";
                    }
                    else {
                        ele.disabled = "";
                    }
                }
            }

            // remove the css which sets the items as disabled.
            var disabledEles = document.getElementsByClassName("show-disabled");

            for (var i = 0; i < disabledEles.length; i++) {
                $(disabledEles[i]).removeClass("show-disabled");
            }

            disabledEles = document.getElementsByClassName("disabled");

            for (var i = 0; i < disabledEles.length; i++) {
                $(disabledEles[i]).removeClass("disabled");
            }
        }
    );

    HideWorking();
}

// Save the data via the api.
function Submit_click() {
    ShowWorking();

    // get the values
    var selectedId = document.getElementById("SelectedFoodItemId").value;
    var foodParcelId = document.getElementsByName("FoodParcelId")[0].value;
    var testId = document.getElementsByName("TestId")[0].value;

    var presentationValue = GetOptionValue("presentationValue");
    var textureValue = GetOptionValue("textureValue");
    var aromaValue = GetOptionValue("aromaValue");
    var flavourValue = GetOptionValue("flavourValue");

    var apiUrl = "/api/TestResultItem/" + foodParcelId + "/" + selectedId + "/" + testId
        + "/?presentationValue=" + presentationValue + "&textureValue=" + textureValue + "&aromaValue=" + aromaValue + "&flavourValue=" + flavourValue;

    // send to api
    $.ajax(
        {
            type: 'PATCH',
            url: apiUrl,
            error: function (textStatus) { ShowErrorResult(textStatus); },
            success: function (data) { SubmitResult(data); }
        }
    );
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
function SubmitResult(data) {
    ShowSaved();
    HideWorking();
}