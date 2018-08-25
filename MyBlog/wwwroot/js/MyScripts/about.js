$(".info").hide();

$(".about").click(function (e) {
    $(".info-about")
        .stop(true, true)
        .toggle("slow", "swing");
});

$(".education").click(function (e) {
    $(".info-education")
        .stop(true, true)
        .toggle("slow", "swing");
});

$(".purpose").click(function (e) {
    $(".info-purpose")
        .stop(true, true)
        .toggle("slow", "swing");
});