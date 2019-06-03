$(function () {
	// Declare a proxy to reference the hub.
	var chat = $.connection.chatHub;
	// Create a function that the hub can call to broadcast messages.
	chat.client.addMessage = function (photo, date, content, userId) {
		// Add the message to the page.
		let mess = `<div class="message ${userId == $("#chat-form").data("user") ? "user-message" : ""} my-2 d-flex align-items-end">
											<ul class="avatars mb-2 px-3">
												<li> <a href="#"> <img src="/Uploads/${photo}" alt="avatar"></a></li>
											</ul>
											<div class="text">
												<p>
											${content}
												</p>
										${new Date(Date.parse(date)).getHours()+`:`+new Date(Date.parse(date)).getMinutes()}
											</div>
										</div>`;
		$('.inbox-messages').append(mess);
		$(".inbox-body").scrollTop($('.inbox-messages').prop("scrollHeight"));
	};
	// Get the user name and store it to prepend to messages.
	$('#displayname').val($(".chat-form").data("id"));
	// Set initial focus to message input box.
	$('#message').focus();
	// Start the connection.

	$.connection.hub.start().done(function () {
		chat.server.addMember($("#chat-form").data("chat"));
		$('#sendmessage').click(function (e) {
			e.preventDefault();
			var userId = $("#chat-form").data("user");
			var chatId = $("#chat-form").data("chat");
			var message = $('#message').val();
			// Call the Send method on the hub.
				chat.server.send(userId, chatId, message);
			
			// Clear text box and reset focus for next comment.
			$('#message').val('').focus();

		});
	});

	$(".inbox-body").scrollTop($(".inbox-body").prop("scrollHeight"));




});