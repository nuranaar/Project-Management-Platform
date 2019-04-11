$(document).ready(function () {

    $(".btn-item").click(function (e) {
        e.preventDefault();
        $(this).addClass("active");
        $($(this).attr("href")).css({
            "display": "inline-block"
        });

        $($(this).siblings(".active").attr("href")).css({
            "display": "none"
        });
        $(this).siblings(".active").removeClass("active");


    });

    $(".menuBtn").click(function (e) {
        e.preventDefault();
        $(".menu").toggleClass("act");
    });


    $($(".btn-list").find(".active").attr("href")).css({
        "display": "inline-block"
    });

});
