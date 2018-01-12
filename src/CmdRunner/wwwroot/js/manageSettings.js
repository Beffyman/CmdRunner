$(document).ready(function () {
	$("#lookupFolder:file").change(function (e) {

		debugger;
		var path = e.target;
		//var files = e.target.files;
		//var path = files[0].webkitRelativePath;
		//var Folder = path.split("/");
		//alert(Folder[0]);

		//var path = e.val();
		$("#Settings_Location").val(path);
	});

});