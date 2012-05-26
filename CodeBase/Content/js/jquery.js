var menu=false;
$(document).click(function(e) {
	var $target=$(e.target);
	if(menu) {
		if(!$target.is(".user-menu ul li")) {
			$(".user-menu").css("visibility","hidden");
			$(".user-name").removeClass("user-name-active");
			menu=false;
		}
	} else {
		if($target.is(".user-name")||$target.is(".user-name img")||$target.is(".user-name span")) {
			$(".user-menu").css("visibility","visible");
			$(".user-name").addClass("user-name-active");
			menu=true;
		}
	}
});