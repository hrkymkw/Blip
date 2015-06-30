$(document).ready(function () {
    $(window).konami({
        code: [38, 40, 37, 39, 66, 65],
        cheat: function () {
            alertify.confirm('Achievement Unlocked. Enter funkytown?', function (e) {
                if (e) {
                    $('#konamiCheat').show();
                }
                else {
                }
            });

        }
    });
});