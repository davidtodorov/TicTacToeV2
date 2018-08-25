$(document).ready(function () {

    // DELETE ONE HISTORY
    $('body').on('click', '#openDeleteHistoryWindow', function () {
        var that = $(this);
        var historyId = that.attr('history-id');
        var userId = that.attr('user-id');

        $('#deleteHisotryWindow .deleteHistoryButton').attr('history-id', historyId);
        $('#deleteHisotryWindow .deleteHistoryButton').attr('user-id', userId);
        $('#deleteHisotryWindow').modal('toggle');
    });

    //DELETE ONE HISTORY
    $('body').on('click', '.deleteHistoryButton', function () {
        var that = $(this);
        var userId = that.attr('user-id');
        var historyId = that.attr('history-id');
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.post("/admin/users/deleteHistory",
            { Id: historyId, UserId: userId, __RequestVerificationToken: token },
            function () {
                window.location.replace("/admin/users/details/" + userId);
            }).fail(function (result) {
            errorMessage(result.statusText);
        });
    });

    $('body').on('click', '#openDeleteAllHistoryWindow', function () {
        var that = $(this);
        var userId = that.attr('user-id');
        $('#deleteHisotryWindow p').val("You gonna delete all user's history! Are you sure");
        $('#deleteHisotryWindow .modal-footer #deleteBtn').removeClass("deleteHistoryButton");
        $('#deleteHisotryWindow .modal-footer #deleteBtn').addClass("deleteAllHistoryButton");
        $('#deleteHisotryWindow .deleteAllHistoryButton').attr('user-id', userId);
        $('#deleteHisotryWindow').modal('toggle');
    });

    $('body').on('click', '.deleteAllHistoryButton', function () {
        var that = $(this);
        var userId = that.attr('user-id');
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.post("/admin/users/deleteAllHistory",
            { UserId: userId, __RequestVerificationToken: token },
            function () {
                window.location.replace("/admin/users/details/" + userId);
            }).fail(function (result) {
            errorMessage(result.statusText);
        });
    });
});




// RESET SCORE
$('body').on('click', '#openResetScoresWindow', function () {
    var that = $(this);
    var userId = that.attr('user-id');

    $('#resetScoreWindow .resetScoresButton').attr('user-id', userId);
    $('#resetScoreWindow').modal('toggle');
});

$('body').on('click', '.resetScoresButton', function () {
    var that = $(this);
    var userId = that.attr('user-id');
    var token = $('input[name="__RequestVerificationToken"]').val();

    $.post("/admin/users/ResetScores",
        { Id: userId, __RequestVerificationToken: token },
        function (result) {
            window.location.replace("/admin/users/details/" + userId);
        }).fail(function (result) {
            errorMessage(result.statusText);
        });
});

function errorMessage(message) {
    toastr["error"](message, "Error");

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

function successMessage(message) {
    toastr["success"](message, "Success");

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

