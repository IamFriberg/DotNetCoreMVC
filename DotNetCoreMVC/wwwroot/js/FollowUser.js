$(document).ready(function () {

    var url = window.location;

    // TOGGLE SUBSRIPTION
    $(":button").click(function () {
        var self = $(this);
        var value = self.attr("value");
        var token = document.getElementsByName("__RequestVerificationToken")[0].value;
        if (self.text() == "Follow") {
            ajaxPost(value, token);
            self.text('Unfollow');
            self.removeClass("btn btn-success");
            self.addClass("btn btn-danger");
        } else if (self.text() == "Unfollow") {
            ajaxDelete(value, token);
            self.text('Follow');
            self.removeClass("btn btn-danger");
            self.addClass("btn btn-success");
        }

    });

    function ajaxPost(userName, token) {
        console.log(userName);
        console.log(token);
        // DO POST
        $.ajax({
            type: "POST",
            url: url,
            data: { userName: userName, __RequestVerificationToken: token },
            success: function (result) {
                //TODO fix better handling
            },
            error: function (e) {
                //TODO fix better handling
                console.log("ERROR: ", e);
            }
        });

    }

    function ajaxDelete(userName, token) {
        console.log(userName);
        console.log(token);
        // DO DELETE
        $.ajax({
            type: "DELETE",
            url: url,
            data: { userName: userName, __RequestVerificationToken: token },
            success: function (result) {
                //TODO fix better handling
            },
            error: function (e) {
                //TODO fix better handling
                console.log("ERROR: ", e);
            }
        });

    }

});