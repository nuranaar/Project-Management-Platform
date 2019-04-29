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

	$(".check").click(function (e) {
		if ($(this).attr("checked") != "checked") {
			$(this).attr("checked", "checked");
			$(this).next().css({
				"text-decoration-line": "line-through"
			});
		}
		else {
			$(this).removeAttr("checked");
			$(this).next().css({
				"text-decoration-line": "none"
			});
		}
	});
	$("input:checked").next().css({
		"text-decoration-line": "line-through"
	});

	//if ($.trim($(".warning-message").html()) == '') {
	//	$(".warning").css({ "display": "none" });
	//}
	////else {
	////	 $(".warning").css({ "display": "block" });
	////}
});
