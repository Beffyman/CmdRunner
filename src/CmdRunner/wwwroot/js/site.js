$(document).ready(function () {
	$("#configurations").change(function (e) {
		$.ajax({
			type: "GET",
			url: "/Index?handler=List",
			contentType: "text/html",
			dataType: "html",
			success: function (response) {
				$("#fileList").load(response);
			}
		});
	})
});