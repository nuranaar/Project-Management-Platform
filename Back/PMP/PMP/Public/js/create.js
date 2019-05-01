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

		$.ajax({
			url: "/task/taskcreate",
			type: "post",
			dataType: "json",
			data: $(this).serialize(),
			success: function (response) {
				console.log(response);
				//var url = `/task?slug=${response.Slug}`;
				//window.location.href = url;
			}
		});

	});


});