$(document).ready(function () {
    $('body').on('click', '.openJoinGameWindow', function () {
        var that = $(this);
        var gameId = that.attr('game-id');

        $('#joinGameWindow #gamePassword').val('');
        $('#joinGameWindow .joinGameButton').attr('game-id', gameId);
        $('#joinGameWindow').modal('show');
    });

    $('body').on('click', '.joinGameButton', function () {
        var that = $(this);
        var gameId = that.attr('game-id');
        var password = $('#joinGameWindow #gamePassword').val();
        var token = $('input[name="__RequestVerificationToken"]').val();

        //$.ajax({
        //    type: "POST",
        //    url: window.location + '/game/join',
        //    data: '{gameId: ' + JSON.stringify(gameId) + ', Password:}' + password + '}',
        //    dataType: "json",
        //    contentType: "application/json; charset=utf-8",
        //    success: function () {
        //        window.location.replace("/game/play/" + gameId);
        //    },
        //    error: function (data) {
        //        alert("Error while inserting data");
        //    }
        //});  

        $.post("/game/join", { GameId: gameId, Password: password, __RequestVerificationToken: token }, function (result) {
            if (result.success === true) {
                window.location.replace("/game/play/" + gameId);

            } else {
                toastr["error"](result.exception, "Error");
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": true,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": true,
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
        });
    });
});

$('.visibility-type').change(function (e) {
    var type = $(".visibility-type").val();
    $('.gamePassword').toggle(type === '3');
});