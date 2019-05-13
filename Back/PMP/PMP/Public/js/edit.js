$(document).ready(function () {

	//get-team
	$(document).on("click", "#edit-team", function (e) {
		e.preventDefault();
		let id = $(this).parents(".team-card").data("id");
		$.ajax({
			url: "/team/TeamDetails/",
			type: "post",
			dataType: "json",
			data: { TeamId: id },
			success: function (response) {
				$("#team-form").find("input[name='name']").val(response.Name);
				$("#team-form").find("input[name='slug']").val(response.Slug);
				$("#team-form").find("textarea[name='desc']").text(response.Desc);
				$("#team-form").attr("data-type", "update");
				let input_id = `<div class="type-id"><input type="text" id="id" name="id" value='${response.Id}'></div>`;
				$("#team-form").find(".slug-id").append(input_id);
				$("#team-form").find(".createBtn").text("Update team");
				$("#team").find(".popup-head").html(`<h3>${response.Name}</h3>`);
				if ($("#team-form").data("type") == "update") {
					$(".mem-input").css({ "display": "none" });
				}
			}
		});
	});

	$(document).on("click", "#edit-project", function (e) {
		e.preventDefault();
		let id = $(this).parents(".project-card").data("id");
		$.ajax({
			url: "/project/ProjectDetails/",
			type: "post",
			dataType: "json",
			data: { ProjectId: id },
			success: function (response) {
				$("#project-form").find("input[name='name']").val(response.Name);
				$("#project-form").find("input[name='slug']").val(response.Slug);
				$("#project-form").find("textarea[name='desc']").val(response.Desc);
				$("#project-form").attr("data-type", "update");
				let input_id = `<div class="type-id"><input type="text" id="id" name="id" value='${response.Id}'></div>`;
				$("#project-form").find(".slug-id").append(input_id);
				$("#project-form").find(".createBtn").text("Update project");
				$("#project").find(".popup-head").html(`<h3>${response.Name}</h3>`);
				if ($("#project-form").data("type") == "update") {
					$(".mem-input").empty();
				}
				console.log(response);
			}
		});
	});

	$(document).on("click", "#edit-note", function (e) {
		let id = $(this).parents(".note-card").data("id");
		e.preventDefault();
		$.ajax({
			url: "/task/NoteDetails",
			type: "post",
			dataType: "json",
			data: {
				Id: id
			},
			success: function (response) {
				$("#title").val(response.Title);
				$("#description").val(response.Desc);
				$("#note-form").attr("data-type", "update");
				$("#note-create").find(".popup-head").html(`<h3>${response.Title}</h3>`);
				let input_id = `<input type="text" id="id" name="id" value='${response.Id}'>`;
				$("#note-form").find(".type-id").append(input_id);
				$("#note-form").find(".createBtn").text("Update note");

			}
		});
	});

	$(document).on("click", "#edit-task", function (e) {
		e.preventDefault();
		let id = $(this).parents(".task-card").data("id");
		$.ajax({
			url: "/task/TaskDetails/",
			type: "post",
			dataType: "json",
			data: { TaskId: id },
			success: function (response) {
				$("#task-form").find("input[name='name']").val(response.Name);
				$("#task-form").find("input[name='slug']").val(response.Slug);
				$("#task-form").find("textarea[name='desc']").val(response.Desc);
				setTimeout(function () {
					$("select[name='TaskStageId'").val(response.Stage.Id);
				},400);
				$("#task-form").attr("data-type", "update");
				let input_id = `<div class="type-id"><input type="text" id="id" name="id" value='${response.Id}'></div>`;
				$("#task-form").find(".slug-id").append(input_id);
				$("#task-form").find(".createBtn").text("Update task");
				$("#task").find(".popup-head").html(`<h3>${response.Name}</h3>`);
				if ($("#task-form").data("type") == "update") {
					$(".mem-input").empty();
				}
			}
		});
	});

});