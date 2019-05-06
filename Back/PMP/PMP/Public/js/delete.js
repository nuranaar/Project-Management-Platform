$(document).ready(function () {
	$(document).on("click", "#delete-project", function (e) {
		e.preventDefault();
		let that = $(this);
		let slug = that.parents(".project-card").data("slug");
		$.ajax({
			url: "/project/ProjectDelete/",
			type: "post",
			datatype: "json",
			data: { Slug: slug },
			success: function (response) {
				that.parents(".project-card[data-slug='" + slug + "']").remove();
				$(".pr-list").find("li[data-slug='" + slug + "']").remove();
			}
		});
	});
	$(document).on("click", "#delete-task", function (e) {
		e.preventDefault();
		let that = $(this);
		let slug = that.parents(".task-card").data("slug");
		$.ajax({
			url: "/task/TaskDelete/",
			type: "post",
			datatype: "json",
			data: { Slug: slug },
			success: function (response) {
				console.log("delete");
				that.parents(".task-card[data-slug='" + slug + "']").remove();
			}
		});
	});
	$(document).on("click", "#delete-team", function (e) {
		e.preventDefault();
		let that = $(this);
		let slug = that.parents(".team-card").data("slug");
		$.ajax({
			url: "/team/teamDelete/",
			type: "post",
			datatype: "json",
			data: { Slug: slug },
			success: function (response) {
				console.log("delete");
				that.parents(".team-card[data-slug='" + slug + "']").remove();
			}
		});
	});
});