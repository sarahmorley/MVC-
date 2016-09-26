$(document).ready(function () {
    $("ol p").hide();
    $("li").click(function () {
        $(this).next("p").slideToggle(500);
    });
});