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
				var url = `/project?slug=${response.Slug}`;
				window.location.href = url;
				//var item = `<li class="nav-item">
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

				var url = `/team?slug=${response.Slug}`;
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
				var def = `<option value="0">Select stage</option>`;
				$("select[name='TaskStageId']").append(def);
				$.each(resp, function (key, stage) {
					var opt = `<option value="${stage.Id}">${stage.Name}</option>`;
					$("select[name='TaskStageId']").append(opt);
				});
			}

		})
	}
	$("#task-form").submit(function (e) {
		e.preventDefault();

		var form = $(this);
		var formdata = false;
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
				var url = `/task?slug=${response.Slug}`;
				window.location.href = url;
			}
		});
	});
	$("#add-check").click(function () {
		var form = `<div id="form-create-click" class="file-card  w-100 d-flex justify-content-start align-items-center">
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
			var that = $(this);
			if (that.find(".check").attr("checked") != "checked") {
				var done = false;
			}
			else {
				done = true;
			}
			var id = $("#check-body").data("id");
			var text = that.find("input:text").val();
			
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
					console.log(done);
						var card = `<div  class="file-card  w-100 d-flex justify-content-start align-items-center">
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


});