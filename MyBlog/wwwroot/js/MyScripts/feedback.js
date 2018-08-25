$(document).ready(function () {
    $(".oldFeed").hide();
    $(".newBtn").hide();
});

$(".newBtn").on('click', function (event) {
    $(".newBtn").hide();
    $(".oldFeed").hide();
    $(".newFeed").show();
    $(".oldBtn").show();

});

$(".oldBtn").on('click', function (event) {
    $(".oldBtn").hide();
    $(".newFeed").hide();
    $(".oldFeed").show();
    $(".newBtn").show();
});