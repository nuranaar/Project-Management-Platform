$(document).ready(function () {
	$("#project-form").submit(function (e) {
		e.preventDefault();
		$.ajax({
			url: "/project/projectcreate",
			type: "post",
			dataType: "json",
			data: $(this).serialize(),
			success: function (response) {
				//console.log(response.Slug);
				const url = `/project?slug=${response.Slug}`;
				window.location.href = url;
				//let item = `<li class="nav-item">
				//				<div class="project">
				//					<div class="curColor">
				//						<i class="fas fa-circle"></i>
				//					</div>
				//					<a class="nav-link" href="@Url.Action("index", "project", new { slug = ${response.Slug})">${response.Name}</a>
				//				</div>
				//			</li>`;
				//$(".pr-list").after(item);
				//$(".menu").removeClass("act");
				//$.fancybox.close();
			}
		});
	});
	$("#team-form").submit(function (e) {
		e.preventDefault();
		$.ajax({
			url: "/team/teamcreate",
			type: "post",
			dataType: "json",
			data: $(this).serialize(),
			success: function (response) {

				const url = `/team?slug=${response.Slug}`;
				window.location.href = url;
			}
		});

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
		letformdata = false;
		if (window.FormData) {
			formdata = new FormData(form[0]);
		}

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
	});
	$("#add-check").click(function () {
		let form = `<div id="form-create-click" class="file-card  w-100 d-flex justify-content-start align-items-center">
					<div class="file-info pl-4 py-4 w-100">
						<div class="input-group pl-5 mb-3">
							<div class="bar">
								<i class="fas fa-bars"></i>
							</div>
							<form id="check-form" data-type="create"  method="post">
<input id="checkbox" class="check" type="checkbox" />
								<input id="text" type="text" class="form-control" placeholder="Enter Text">
								<button type="submit" action="@Url.Action("ChecklistCreate", "task" , new={Model.Task.Id})" class="createBtn btn btn-primary my-3">
						Create
						checklist
					</button>
							</form>
						</div>
					</div>
				</div>`;
		$("#check-body").prepend(form);
		$(".check").click(function (e) {
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
			let done;
			if (that.find(".check").attr("checked") != "checked") {
				done = false;
			}
			else {
				done = true;
			}
			let id = $(".head").data("id");
			let text = that.find("input:text").val();

			$.ajax({
				url: "/task/ChecklistCreate",
				type: "POST",
				dataType: "json",
				data: {
					Check: done,
					TaskId: id,
					Text: text
				},
				success: function (response) {
					let card = `<div  class="file-card  w-100 d-flex justify-content-start align-items-center">
						<div class="file-info pl-4 py-4 w-100">
							<div class="input-group pl-5 mb-3">
								<div class="bar">
									<i class="fas fa-bars"></i>
								</div>
								<form id="check-form" data-type="edit" method="post">
									<input class="check" type="checkbox" ${done ? 'checked = "checked"' : ''}>
									<input id="text" type="text" class="form-control" placeholder="Enter Text" value="${response.Text}">
								</form>
							</div>
						</div>
						<div class="dropdown">
							<a class="nav-link" href="#" id="navbarDropdownMenuLink" role="button"
							   data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
								<i class="fas fa-ellipsis-v"></i>
							</a>
							<div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
								<a class="dropdown-item" href="#">Edit</a>
								<a class="dropdown-item" href="#">Share</a>
								<a class="dropdown-item" href="#">Delete</a>
							</div>
						</div>
					</div>`;
					$("#check-body").prepend(card);
					$("#check-body").find("#form-create-click").empty();
					$("input:checked").next().css({
						"text-decoration-line": "line-through"
					});
				}
			});
		});
	});
	$("#note-form").submit(function (e) {
		e.preventDefault();
		let taskid = $(".head").data("id");
		let title = $("#title").val();
		let description = $("#description").val();
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
				let note = `<div class="note-card p-4">
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
								<a class="dropdown-item" href="#">Edit</a>
								<a class="dropdown-item" href="#">Share</a>
								<a class="dropdown-item" href="#">Delete</a>
							</div>
						</div>
					</div>`;
				$("#note-body").prepend(note);
				$.fancybox.close();
			}
		});
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
				let mem = `<div class="member-card text-center">
					<div class="member-photo mt-5">
						<img src="/Public/img/${response.Photo}" alt="${response.User}">
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
				let user = `<li><a href="#"><img src="/Public/img/${response.Photo}" alt="${response.User}"></a></li>`;
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
				let user = `<li><a href="#"><img src="/Public/img/${response.Photo}" alt="${response.User}"></a></li>`;
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
				let user = `<li><a href="#"><img src="/Public/img/${response.Photo}" alt="${response.User}"></a></li>`;
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
			url: "/task/FileUpload",
			type: "post",
			dataType: "json",
			data:formdata ? formdata : form.serialize(),
			processData: false,
			contentType: false,
			success: function (response) {
				let card = `<div class="file-card w-100 d-flex justify-content-start align-items-center">
					<ul class="avatars mt-3">
						<li><a href=""><i class="far fa-file-alt"></i></a></li>
						<li><a href=""><img src="/Public/img/${response.Photo}" alt="${response.User}"></a></li>
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
							<a class="dropdown-item" href="#">Edit</a>
							<a class="dropdown-item" href="#">Share</a>
							<a class="dropdown-item" href="#">Delete</a>
						</div>
					</div>
				</div>`;
				$("#file-list").prepend(card);
			}
		});
	});
});