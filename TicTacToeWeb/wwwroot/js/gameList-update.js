$(document).ready(function () {
    setInterval(updateList, 3850);
});

function updateList() {
   $.get('/game/gamelist', function(result) {
        if (result) {
            $('#game-list-container').html(result);
        }
    });
}