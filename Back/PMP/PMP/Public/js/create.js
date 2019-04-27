$(document).ready(function () {

	$("#project-form").submit(function (e) {
		e.preventDefault();
		
			$.ajax({
				url: "/project/projectcreate",
				type: "post",
				dataType: "json",
				data: $(this).serialize(),
				success: function (response) {
					var item = `<li class="nav-item">
									<div class="project">
										<div class="curColor">
											<i class="fas fa-circle"></i>
										</div>
										<a class="nav-link" href="@Url.Action("index", "project", new { slug = project.Slug })">${response.Name}</a>
									</div>
								</li>`;
					$(".project").after(item);
					$(this).parent("#hidden-content").css({ "display": "none" });
				}
			});
		
	})


});