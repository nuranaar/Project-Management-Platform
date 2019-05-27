$(document).ready(function () {
	$("#project-form").submit(function (e) {
		e.preventDefault();

		if ($(this).data("type") == "create") {

			$.ajax({
				url: "/project/projectcreate",
				type: "post",
				dataType: "json",
				data: $(this).serialize(),
				success: function (response) {
					const url = `/project?slug=${response.Slug}&adminId=${response.UserId}`;
					window.location.href = url;
				}
			});
		} else {
			var name = $("#project-form").find("input[name='name']").val();
			var slug = $("#project-form").find("input[name='slug']").val();
			var desc = $("#project-form").find("textarea[name='desc']").val();
			var id = $("#project-form").find("input[name='id']").val();
			$.ajax({
				url: "/project/ProjectEdit",
				type: "post",
				dataType: "json",
				data: {
					Id: id,
					Name: name,
					Slug: slug,
					Desc: desc
				},
				success: function (response) {
					$(`div[data-id="${response.Id}"]`).find(".title").find("span").text(response.Name);
					$(`div[data-id="${response.Id}"]`).find("p").text(response.Desc);
					$.fancybox.close();
					$("#project-form").find("input[name='name']").val('');
					$("#project-form").find("textarea[name='desc']").val('');
					$("#project-form").attr("data-type", "create");
					$("#project-form").find(".type-id").remove();
					$("#project-form").find(".createBtn").text("Create project");
					$("#project").find(".popup-head").html(`<h3>New project</h3>`);
					let mem = `<label for="member">Members</label>
						<input type="text" id="member" name="member" class="form-control" aria-describedby="projectMember"
							   placeholder="Add project members">


						<div class="row">
							<div class="col-6">
								<label for="StartTime">Start date</label>
								<input type="date" id="StartTime" name="StartTime" class="form-control"
									   aria-describedby="StartTime">
							</div>
							<div class="col-6">
								<label for="EndTime">End date</label>
								<input type="date" id="EndTime" name="EndTime" class="form-control"
									   aria-describedby="EndTime">
							</div>
						</div>`;
					$(".mem-input").append(mem);

				}
			});

		}
	});
	$("#team-form").submit(function (e) {
		e.preventDefault();
		if ($(this).data("type") == "create") {

			$.ajax({
				url: "/team/teamcreate",
				type: "post",
				dataType: "json",
				data: $(this).serialize(),
				success: function (response) {
					const url = `/team?slug=${response.Slug}&adminId=${response.UserId}`;
					window.location.href = url;
				}
			});
		} else {
			$.ajax({
				url: "/team/TeamEdit",
				type: "post",
				dataType: "json",
				data: $(this).serialize(),
				success: function (response) {
					$(`div[data-id="${response.Id}"]`).find("span").text(response.Name);
					$(`div[data-id="${response.Id}"]`).find("p").text(response.Desc);
					$.fancybox.close();
					$("#team-form").find("input[name='name']").val('');
					$("#team-form").find("textarea[name='desc']").val('');
					$("#team-form").attr("data-type", "create");
					$("#team-form").find(".type-id").remove();
					$("#team-form").find(".createBtn").text("Create team");
					$("#team").find(".popup-head").html(`<h3>New Team</h3>`);
					$(".mem-input").css({ "display": "block" });

				}
			});

		}

	});
	$("[data-fancybox]").fancybox({
		afterShow: function () {
			GetStages();
		}
	});
	function GetStages() {
		$.ajax({
			url: "/task/GetStages",
			type: "get",
			dataType: "json",
			success: function (resp) {
				$("select[name='TaskStageId']").empty();
				let def = `<option value="0">Select stage</option>`;
				$("select[name='TaskStageId']").append(def);
				$.each(resp, function (key, stage) {
					let opt = `<option value="${stage.Id}">${stage.Name}</option>`;
					$("select[name='TaskStageId']").append(opt);
				});
			}

		})
	}
	$("#task-form").submit(function (e) {
		e.preventDefault();

		let form = $(this);
		let formdata = false;
		if (window.FormData) {
			formdata = new FormData(form[0]);
		}
		if ($(this).data("type") == "create") {
			$.ajax({
				url: "/task/TaskCreate",
				type: "post",
				dataType: "json",
				data: formdata ? formdata : form.serialize(),
				processData: false,
				contentType: false,
				success: function (response) {
					const url = `/task?slug=${response.Slug}`;
					window.location.href = url;
				}
			});
		} else {
			var name = $("#task-form").find("input[name='name']").val();
			var slug = $("#task-form").find("input[name='slug']").val();
			var desc = $("#task-form").find("textarea[name='desc']").val();
			var stage = $("#task-form").find("select[name='TaskStageId']").val();
			var id = $("#task-form").find("input[name='id']").val();
			$.ajax({
				url: "/task/TaskEdit",
				type: "post",
				dataType: "json",
				data: {
					Id: id,
					Name: name,
					Slug: slug,
					Desc: desc,
					Stage: stage
				},
				success: function (response) {
					const url = `/task?slug=${response.Slug}&adminId=${response.UserId}`;
					window.location.href = url;
				}
			});
		}
	});

	$("#add-check").click(function (e) {
		e.preventDefault();
		let form = `<div id="form-create-click" class="file-card w-100 d-flex justify-content-start align-items-center">
					<div class="file-info pl-4 py-4 w-100">
						<div class="input-group pl-5 mb-3">
							<div class="bar">
								<i class="fas fa-bars"></i>
							</div>
							<form id="check-form" data-type="create"  method="post">
<input id="checkbox" class="check" type="checkbox" />
								<input id="text" type="text" class="form-control" placeholder="Enter Text" required maxlength="100">
								<button type="submit" class="createBtn btn btn-primary my-3">
						Create
					</button>
							</form>
						</div>
					</div>
				</div>`;
		$("#check-body").prepend(form);
		$(".check").click(function () {
			if ($(this).attr("checked") != "checked") {
				$(this).attr("checked", "checked");
			}
			else {
				$(this).removeAttr("checked");
			}
		});
		$("#check-form").submit(function (e) {
			e.preventDefault();
			let that = $(this);
			let checking = $(".check").prop('checked');
			let tid = $(".head").data("id");
			let text = that.find("input:text").val();
			if ($(this).data("type") == "create") {
				$.ajax({
					url: "/task/ChecklistCreate",
					type: "POST",
					dataType: "json",
					data: {
						Check: checking,
						TaskId: tid,
						Text: text
					},
					success: function (response) {
						let card = `<div  class="check-card file-card  w-100 d-flex justify-content-start align-items-center" data-id="${response.Id}">
						<div class="file-info pl-4 py-4 w-100">
							<div class="input-group pl-5 mb-3">
								<div class="bar">
									<i class="fas fa-bars"></i>
								</div>
								<form id="check-form" data-type="update" method="post">
									<input name="checked" class="check" type="checkbox" ${checking ? 'checked = "checked"' : ''}>
									<input name="text" id="text" type="text" class="form-control" placeholder="Enter Text" value="${response.Text}">
								</form>
							</div>
						</div>
						<div class="dropdown">
							<a class="nav-link" href="#" id="navbarDropdownMenuLink" role="button"
							   data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
								<i class="fas fa-ellipsis-v"></i>
							</a>
							<div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
									<a id="edit-checkitem" class="dropdown-item" href="#">Edit</a>
							<a id="delete-checkitem" class="dropdown-item" href="#">Delete</a>
							</div>
						</div>
					</div>`;
						$("#check-body").prepend(card);
						$("#check-body").find("#form-create-click").remove();
						$("input:checked").next().css({
							"text-decoration-line": "line-through"
						});
					}
				});
			}

		});
	});
	$(document).on("click", "#edit-checkitem",function (e) {
		e.preventDefault();
		let that=$(this).parents(".check-card");
		let edit = `<button type="submit" class="createBtn btn btn-primary my-3">
						Update
					</button>`;
		that.find("#check-form").append(edit);
		$(document).on("submit","#check-form" ,function (e) {
			e.preventDefault();
			let form = $(this);
			let Id = form.parents(".check-card").data("id");
			let texts = form.find("input:text").val();
			var checking = $(".check").prop('checked');

			if (form.data("type") == "update") {

				$.ajax({
					url: "/task/CheckEdit",
					type: "POST",
					dataType: "json",
					data: {
						id: Id,
						check: checking,
						text: texts
					},
					success: function (response) {
						$(`.check-card[data-id="${response.Id}"]`).find("button").remove();

					}
				});
			}
		});
	});
	$("#note-form").submit(function (e) {
		e.preventDefault();
		let taskid = $(".head").data("id");
		let title = $("#title").val();
		let description = $("#description").val();
		let id = $(this).find("#id").val()
		if ($(this).data("type") == "create") {
			$.ajax({
				url: "/task/NoteCreate",
				type: "post",
				dataType: "json",
				data: {
					TaskId: taskid,
					Title: title,
					Desc: description
				},
				success: function (response) {
					let note = `<div class="note-card p-4" data-id="${response.Id}">
						<p>
							${response.Title}
						</p>
						<span>
							${response.Desc}
						</span>
						<div class="dropdown">
							<a class="nav-link" href="#" id="navbarDropdownMenuLink" role="button"
							   data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
								<i class="fas fa-ellipsis-v"></i>
							</a>
							<div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
								<a id="edit-note" class="dropdown-item" data-src="#note-create" data-fancybox href="javascript:;">Edit</a>
								<a id="delete-note" class="dropdown-item" href="#">Delete</a>
							</div>
						</div>
					</div>`;
					$("#note-body").prepend(note);
					$.fancybox.close();
				}
			});
		} else {
			$.ajax({
				url: "/task/NoteEdit",
				type: "post",
				dataType: "json",
				data: {
					Id: id,
					Title: title,
					Desc: description
				},
				success: function (response) {
					$(`.note-card[data-id="${response.Id}"]`).find("p").text(response.Title)
					$(`.note-card[data-id="${response.Id}"]`).find("span").text(response.Desc)
					$.fancybox.close();
					$("#note-form").find(".type-id").remove();
					$("#note-form").attr("data-type", "create");
					$("#note-create").find(".popup-head").html(`<h3>New note</h3>`);
					$("#note-form").find(".createBtn").text("Create note");
				}
			});
		}
	});
	//team
	$("#member-form").submit(function (e) {
		e.preventDefault();
		let that = $(this);
		let teamid = $(".head").data("id");
		let mem = that.find("input:text").val();
		$.ajax({
			url: "/team/AddMember",
			type: "post",
			dataType: "json",
			data: {
				TeamId: teamid,
				member: mem
			},
			success: function (response) {
				let mem = `<div class="member-card text-center" data-id="${response.Id}">
<a id="delete-member" class="del" href="#"><i class="fas fa-times"></i></a>
					<div class="member-photo mt-5">
						<img src="/Uploads/${response.Photo}" alt="${response.User}">
					</div>
						<div class="member-info mt-3">
							<a href="#">
								<h6>${response.User}</h6>
							</a>
							<p>${response.Position}</p>
						</div>
						<ul class="icons mx-4">
							<li><i class="fab fa-twitter"></i></li>
							<li><i class="fab fa-linkedin-in"></i></li>
							<li><i class="fab fa-facebook-f"></i></li>
						</ul>
					</div>`;
				$("#mem-list").prepend(mem);
				let user = `<li><a href="#"><img src="/Uploads/${response.Photo}" alt="${response.User}"></a></li>`;
				$(".avatars").prepend(user);
				$.fancybox.close();
			}
		});

	});
	//task
	$("#member-form-task").submit(function (e) {
		e.preventDefault();
		let that = $(this);
		let id = $(".head").data("id");
		let mem = that.find("input:text").val();
		$.ajax({
			url: "/task/AddMember",
			type: "post",
			dataType: "json",
			data: {
				TaskId: id,
				member: mem
			},
			success: function (response) {
				let user = `<li><a href="#"><img src="/Uploads/${response.Photo}" alt="${response.User}"></a></li>`;
				$(".avatars").prepend(user);
				$.fancybox.close();
			}
		});

	});
	//project
	$("#member-form-project").submit(function (e) {
		e.preventDefault();
		let that = $(this);
		let id = $(".head").data("id");
		let mem = that.find("input:text").val();
		$.ajax({
			url: "/project/AddMember",
			type: "post",
			dataType: "json",
			data: {
				ProjectId: id,
				member: mem
			},
			success: function (response) {
				let user = `<li><a href="#"><img src="/Uploads/${response.Photo}" alt="${response.User}"></a></li>`;
				$(".avatars").prepend(user);
				$.fancybox.close();
			}
		});

	});
	$("#files").find("input:file").change(function () {
		$("#file-upload").submit();
	});
	$("#file-upload").submit(function (e) {
		e.preventDefault();
		let form = $(this);
		let formdata = false;
		if (window.FormData) {
			formdata = new FormData(form[0]);
		}
		$.ajax({
			url: "/task/FileUpload/",
			type: "post",
			dataType: "json",
			data: formdata ? formdata : form.serialize(),
			processData: false,
			contentType: false,
			success: function (response) {
				let card = `<div id="file-card" class="file-card w-100 d-flex justify-content-start align-items-center" data-id="${response.Id}">
					<ul class="avatars mt-3">
						<li><a href=""><i class="far fa-file-alt"></i></a></li>
						<li><a href=""><img src="/Uploads/${response.Photo}" alt="${response.User}"></a></li>
					</ul>
					<div class="file-info pl-4 py-4">
						<a href="#">
							<p class="m-0">${response.Name}</p>
						</a>
						<span>${response.Weight}</span>
					</div>
					<div class="dropdown">
						<a class="nav-link" href="#" id="navbarDropdownMenuLink" role="button"
						   data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
							<i class="fas fa-ellipsis-v"></i>
						</a>
						<div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
								<a id="delete-file" class="dropdown-item" href="#">Delete</a>
						</div>
					</div>
				</div>`;
				$("#file-list").prepend(card);
			}
		});
	});
});