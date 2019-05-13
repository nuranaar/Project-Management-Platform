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
				that.parents(".team-card[data-slug='" + slug + "']").remove();
			}
		});
	});
	$(document).on("click", "#delete-checkitem", function (e) {
		e.preventDefault();
		let that = $(this);
		let id = that.parents(".check-card").data("id");
		$.ajax({
			url: "/task/checkitemDelete/",
			type: "post",
			datatype: "json",
			data: { Id: id },
			success: function (response) {
				that.parents(".check-card[data-id='" + id + "']").remove();
			}
		});
	});
	$(document).on("click", "#delete-note", function (e) {
		e.preventDefault();
		let that = $(this);
		let id = that.parents(".note-card").data("id");
		$.ajax({
			url: "/task/noteDelete/",
			type: "post",
			datatype: "json",
			data: { Id: id },
			success: function (response) {
				that.parents(".note-card[data-id='" + id + "']").remove();
			}
		});
	});
	$(document).on("click", "#delete-file", function (e) {
		e.preventDefault();
		let that = $(this);
		let id = that.parents("#file-card").data("id");
		$.ajax({
			url: "/task/fileDelete/",
			type: "post",
			datatype: "json",
			data: { Id: id },
			success: function (response) {
				that.parents(".file-card[data-id='" + id + "']").remove();
			}
		});
	});
	$(document).on("click", "#delete-member", function (e) {
		e.preventDefault();
		let memId = $(this).parent().data("id");
		$.ajax({
			url: "/team/MemberDelete/",
			type: "post",
			datatype: "json",
			data: {
				MemId: memId
			},
			success: function (response) {
				$(`.member-card[data-id="${memId}"]`).remove();
			}
		});
	});
});