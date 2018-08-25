$(document).ready(function () {
    setInterval(updateGame, 250);
});

var isShown = false;

function updateGame() {
    var gameId = $('#hiddenGameId').val();

    $.get('/game/status',
        { Id: gameId },
        function (result) {
            if (result.success === true) {
                updateBoard(result.status.board);
                updateStatus(result.status);
                //createNotification(result.status, isShown);
            }
        });
}

function updateBoard(board) {
    var cells = $('.game-grid-table td');

    for (var i = 0; i < board.length; i++) {
        var cell = $(cells[i]);
        cell.text(board[i]);
    }
}

function updatePlayerVsPlayer(status) {
    if (status.state !== 1) {
        var players = $('h3#pvsp');
        players.text(status.creatorUsername + "[X] vs " + status.opponentUsername + "[O]");
    }
}

function updateStatus(status) {
    $('.game-grid-table td').on('click');
    var gameStatus = $('h3#gameStatus ');

    updatePlayerVsPlayer(status);

    if (status.state === 1) {
        gameStatus.text("Status: Waiting for a second player");
        forbidClick();
    }
    else if (status.state === 2) {
        gameStatus.text("Status: X turn");
        allowClick(status.creatorUserId);
    }
    else if (status.state === 3) {
        gameStatus.text("Status: O turn");
        allowClick(status.opponentUserId);
    }
    else if (status.state === 4) {
        gameStatus.text("Status: X won");
        forbidClick();
    }
    else if (status.state === 5) {
        gameStatus.text("Status: O won");
        forbidClick();
    }
    else if (status.state === 6) {
        gameStatus.text("Status: Draw");
        forbidClick();
    }
}

function forbidClick() {
    $('.game-grid-table').css('cursor', 'not-allowed');
    $('.game-grid-table td').off('click');
}

function allowClick(currentUserId) {
    var userId = $('#hiddenUserId').val();

    forbidClick();
    if (currentUserId === userId) {
        $('.game-grid-table').css('cursor', 'context-menu');
        $('.game-grid-table td').on('click',
            function () {
                var that = $(this);
                var row = that.attr('row');
                var col = that.attr('col');
                var gameId = $('#hiddenGameId').val();
                var token = $('input[name="__RequestVerificationToken"]').val();

                $.post("/game/play",
                    { GameId: gameId, Row: row, Col: col, __RequestVerificationToken: token },
                    function (result) {
                        if (result.success === true) {

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
    }
}

function createNotification(status, isShown) {

    if (status.state === 3 || status.state === 2) {
        if (isShown === false) {
            this.isShown = true;
            toastr["info"]("Player: " + status.opponentUsername + " has joined!", "Notification");
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
    }
}