function please_wait(a)
{
	if(a=='show')
	{
		$('body').append('<div class="pls_wait" >Please&nbsp;wait...</div>');
	}
	else
	{
		$('.pls_wait').remove();
	}
}

function form_success_msg(m)
{
	$('body').append('<div class="form_success_msg" >'+m+'</div>');
	$('.form_success_msg').fadeOut(5000);
}

function form_error_msg(m)
{
	$('body').append('<div class="form_error_msg" >'+m+'</div>');
	$('.form_error_msg').fadeOut(5000);
}

function Confirm(title, msg, $true, $false, action) 
{
	var $content =  "<div class='dialog-ovelay fadeIn'>" +
			"<div class='dialog zoomIn'><header>" +
			 " <h3> " + title + " </h3> " +
			"<span class='close_btn close_con_popup'>Close</span>" +
		 "</header>" +
		 "<div class='dialog-msg'>" +
			 " <p> " + msg + " </p> " +
		 "</div>" +
		 "<footer>" +
			 "<div class='controls'>" +
				 " <button class='btn doAction'>" + $true + "</button> " +
				 " <button class='btn cancelAction'>" + $false + "</button> " +
			 "</div>" +
		 "</footer>" +
	  "</div>" +
	"</div>";
	
	$('body').prepend($content);
	
	$('body').off('click', '.doAction');
	$('body').on('click', '.doAction', function () {
		$(this).parents('.dialog-ovelay').find('.dialog').removeClass('zoomIn').addClass('zoomOut');
		$(this).parents('.dialog-ovelay').fadeOut(function () {
			$(this).remove();
		});
		action($(this).text());
	});
	
	$('body').on('click', '.cancelAction', function () {
		$(this).parents('.dialog-ovelay').find('.dialog').removeClass('zoomIn').addClass('zoomOut');
		$(this).parents('.dialog-ovelay').fadeOut(function () {
			$(this).remove();
		});
		action($(this).text());
	});
	
	$('.close_con_popup').click(function () {
		$(this).parents('.dialog-ovelay').find('.dialog').removeClass('zoomIn').addClass('zoomOut');
		$(this).parents('.dialog-ovelay').fadeOut(function () {
			$(this).remove();
		});
	});
}

function popup_video(title, url) 
{
	var $content =  "<div class='dialog-ovelay fadeIn'>" +
			"<div class='dialog zoomIn'><header>" +
			 "<h3> " + title + " </h3> " +
			 "<span class='close_btn close_video_popup'>Close</span>" +
		 "</header>" +
		 "<div class='dialog-msg video_popup'>" +
			 "<iframe width='100%' height='auto' src='"+url+"' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen=''></iframe>" +
		 "</div>" +
	  "</div>" +
	"</div>";
	
	$('body').prepend($content);
	
	$('.close_video_popup').click(function () {
		$(this).parents('.dialog-ovelay').find('.dialog').removeClass('zoomIn').addClass('zoomOut');
		$(this).parents('.dialog-ovelay').fadeOut(function () {
			$(this).remove();
		});
	});
}

function popup_alert(title, msg, $true, action) 
{
	var $content =  "<div class='dialog-ovelay fadeIn'>" +
			"<div class='dialog zoomIn'><header>" +
			 " <h3> " + title + " </h3> " +
		 "</header>" +
		 "<div class='dialog-msg'>" +
			 " <p> " + msg + " </p> " +
		 "</div>" +
		 "<footer>" +
			 "<div class='controls'>" +
				 " <button class='btn popup_alert_close_btn'>" + $true + "</button> " +
			 "</div>" +
		 "</footer>" +
	  "</div>" +
	"</div>";
	
	$('body').prepend($content);
	
	$('body').on('click', '.popup_alert_close_btn', function () {
		$(this).parents('.dialog-ovelay').find('.dialog').removeClass('zoomIn').addClass('zoomOut');
		$(this).parents('.dialog-ovelay').fadeOut(function () {
			$(this).remove();
		});
		action($(this).text());
	});
}

$(document).ready(function(){
    $('#nav-icon4').click(function(){
        $(this).toggleClass('open');
    });		
	
	$('.see_video').click(function(e){
		e.preventDefault();
		var url = $(this).attr('href');
		if(url!='')
		{
			popup_video('Video', url);
		}
	});
});
