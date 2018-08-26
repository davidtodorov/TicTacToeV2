$(document).ready(function () {
    setBackground();
    $('body').on('click', '#chooseColorButton',
        function() {
            var value = $('#chooseColor').val();
            localStorage.setItem("backgroundColor", value);
            setBackground();
        });
});

function setBackground() {
    var color = localStorage.getItem("backgroundColor");
    if (color) {
        document.body.style.backgroundColor = color;
    }
}