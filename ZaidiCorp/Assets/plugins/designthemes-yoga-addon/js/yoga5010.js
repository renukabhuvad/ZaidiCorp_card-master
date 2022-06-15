jQuery("document").ready(function($){

	$("form.yoga-video-sorting").submit(function(e){

		e.preventDefault();

		var $style = $(this).find("select[name='yoga-style']").val();
		var $level = $(this).find("select[name='student-level']").val();
		var $teacher = $(this).find("select[name='yoga-teacher']").val();
		var $column = $(this).parents(".dt-sc-yoga-video-listing-container").data('column');
		var $container = $(this).parents(".dt-sc-yoga-video-listing-container").find(".dt-sc-yoga-videos-container");

		var $data = {
			'action' : 'dt_sc_filter_yoga_videos',
			'data' : { 'style':$style, 'level':$level, 'teacher':$teacher, 'column':$column}
		};

		$.ajax({
			url: dttheme_urls.ajaxurl,
			data: $data,
			beforeSend: function() {
				$container.html('<div class="dt-sc-loading yoga-videos"></div>');
			},
			success: function( response ) {
				$container.html(response).fadeIn();	
			}			
		});
	});

	$("div.dt-sc-yoga-style-sorting > a").click(function(e){

		e.preventDefault();

		$sorting_container = $(this).parents(".dt-sc-yoga-style-sorting");
		$check = $sorting_container.data('content');

		if(typeof $check === 'undefined') {
			$data_container = $(this).parents(".dt-sc-yoga-style-sorting").next("div.dt-sc-yoga-style-listing-container");
		} else {
			$data_container = $(this).parents(".dt-sc-yoga-style-sorting").next("div.dt-sc-yoga-style-listing").find("div.dt-sc-yoga-style-listing-container");
		}

		$sorting_container.find("a.active-sort").removeClass('active-sort');
		$(this).addClass('active-sort');

		$filter = $(this).data('filter');
		if(typeof $filter === 'undefined'){
			$filter = $(this).html();
		}

		var $data = {
			'action' : 'dt_sc_filter_yoga_styles',
			'data'	: { 'title' : $filter, 'column': $data_container.data('column')}
		};

		jQuery.ajax({
			url: dttheme_urls.ajaxurl,
			data: $data,
			beforeSend: function() {
				$data_container.html('<div class="dt-sc-loading yoga-styles"></div>');
			},
			success: function( response ) {
				$data_container.html(response).fadeIn();	
			}
		});	
	});

	$("div.dt-sc-yoga-poses-sorting > a").click(function(e){
		e.preventDefault();

		$sorting_container = $(this).parents(".dt-sc-yoga-poses-sorting");
		$check = $sorting_container.data('content');

		if(typeof $check === 'undefined') {
			$data_container = $(this).parents(".dt-sc-yoga-poses-sorting").next("div.dt-sc-yoga-poses-listing-container");
		} else {
			$data_container = $(this).parents(".dt-sc-yoga-poses-sorting").next("div.dt-sc-yoga-poses-listing").find("div.dt-sc-yoga-poses-listing-container");
		}

		$sorting_container.find("a.active-sort").removeClass('active-sort');
		$(this).addClass('active-sort');

		$filter = $(this).data('filter');
		if(typeof $filter === 'undefined'){
			$filter = $(this).html();
		}

		var $data = {
			'action' : 'dt_sc_filter_yoga_poses',
			'data'	: { 'title' : $filter, 'column': $data_container.data('column')}
		};

		jQuery.ajax({
			url: dttheme_urls.ajaxurl,
			data: $data,
			beforeSend: function() {
				$data_container.html('<div class="dt-sc-loading yoga-poses"></div>');
			},
			success: function( response ) {
				$data_container.html(response).fadeIn();	
			}
		});		
	});

	$("div.dt-sc-yoga-teachers-sorting > a").click(function(e){
		e.preventDefault();

		$sorting_container = $(this).parents(".dt-sc-yoga-teachers-sorting");
		$check = $sorting_container.data('content');

		if(typeof $check === 'undefined') {
			$data_container = $(this).parents(".dt-sc-yoga-teachers-sorting").next("div.dt-sc-yoga-teachers-listing-container");
		} else {
			$data_container = $(this).parents(".dt-sc-yoga-teachers-sorting").next("div.dt-sc-yoga-teachers-listing").find("div.dt-sc-yoga-teachers-listing-container");
		}

		$sorting_container.find("a.active-sort").removeClass('active-sort');
		$(this).addClass('active-sort');

		$filter = $(this).data('filter');
		if(typeof $filter === 'undefined'){
			$filter = $(this).html();
		}

		var $data = {
			'action' : 'dt_sc_filter_yoga_teachers',
			'data'	: { 'title' : $filter, 'column': $data_container.data('column')}
		};

		jQuery.ajax({
			url: dttheme_urls.ajaxurl,
			data: $data,
			beforeSend: function() {
				$data_container.html('<div class="dt-sc-loading yoga-teachers"></div>');
			},
			success: function( response ) {
				$data_container.html(response).fadeIn();	
			}
		});		
	});

	$("form.yoga-program-sorting").submit(function(e){
		e.preventDefault();

		var $style = $(this).find("select[name='yoga-style']").val();
		var $level = $(this).find("select[name='student-level']").val();
		var $category = $(this).find("select[name='yoga-program-category']").val();
		var $teacher = $(this).find("select[name='yoga-teacher']").val();
		var $column = $(this).parents(".dt-sc-yoga-program-listing-container").data('column');
		var $container = $(this).parents(".dt-sc-yoga-program-listing-container").find(".dt-sc-yoga-programs-container");

		var $data = {
			'action' : 'dt_sc_filter_yoga_programs',
			'data' : { 'style':$style, 'level':$level, 'teacher':$teacher, 'category':$category, 'column':$column}
		};

		$.ajax({
			url: dttheme_urls.ajaxurl,
			data: $data,
			beforeSend: function() {				
				$container.html('<div class="dt-sc-loading yoga-programs"></div>');
			},
			success: function( response ) {
				$container.html(response).fadeIn();	
			}			
		});		
	});
});