$(document).ready(function () {
	String.prototype.removeAcento = function () {
		var text = this.toLowerCase();
		text = text.replace(new RegExp('[ÁÀÂÃ]', 'gi'), 'a');
		text = text.replace(new RegExp('[ÉÈÊ]', 'gi'), 'e');
		text = text.replace(new RegExp('[ÍÌÎ]', 'gi'), 'i');
		text = text.replace(new RegExp('[ÓÒÔÕ]', 'gi'), 'o');
		text = text.replace(new RegExp('[ÚÙÛÜ]', 'gi'), 'u');
		text = text.replace(new RegExp('[Ç]', 'gi'), 'c');
		text = text.replace(new RegExp('[Ə]', 'gi'), 'e');
		text = text.replace(new RegExp('[I]', 'gi'), 'i');
		text = text.replace(new RegExp('[ı]', 'gi'), 'i');
		return text;
	};

	String.prototype.slugify = function () {
		return this.toString().toLowerCase().removeAcento().trim()
			.replace(/\s+/g, '-')           // Replace spaces with -
			.replace(/[^\w\-]+/g, '')       // Remove all non-word chars
			.replace(/\-\-+/g, '-')         // Replace multiple - with single -
			.replace(/^-+/, '')             // Trim - from start of text
			.replace(/-+$/, '');            // Trim - from end of text
	};
	$("input[data-type='slug-source']").blur(function () {
		$("input[data-type='slug-input']").val($(this).val().slugify());
	});
});