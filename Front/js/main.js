$(document).ready(function () {

    $(".btn-item").click(function (e) {
        e.preventDefault();
        $(this).addClass("active");
        if ($(this).siblings().hasClass("active")) {
            $(this).siblings().removeClass("active");
        }
    });

    $(".menuBtn").click(function (e) {
        e.preventDefault();
        $(".menu").toggleClass("act");
    });
});
