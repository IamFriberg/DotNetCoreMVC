$(document).ready(function () {

    var url = window.location;

    // SUBMIT FORM
    $("#messageForm").submit(function (event) {
        // Prevent the form from submitting via the browser.
        event.preventDefault();
        ajaxPost();
    });

    function ajaxPost() {
        var token = document.getElementsByName("__RequestVerificationToken")[0].value;
        // DO POST
        $.ajax({
            type: "POST",
            url: url,
            data: { message: $("#message").val(), __RequestVerificationToken: token },
            success: function (result) {
                //$("#postResultDiv").html("<strong>" + "Post Successfully!</strong>");
                //TODO fix better handling
                alert("Post Successfully! reload page to see it ^^")
            },
            error: function (e) {
                //TODO fix better handling
                alert("Error! could not save message")
                console.log("ERROR: ", e);
            }
        });

        // Reset FormData after Posting
        resetData();

    }

    function resetData() {
        $("#message").val("");
    }

});