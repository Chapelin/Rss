'use strict';

/* Services */
rssApp.service('GetFromServer',  function($http){
	return function(chemin, sucess, fail){
		$http.get("http://localhost:5555/"+chemin).success(function(data) {
			sucess(data);
		}).error(function(data, status, headers, config){
			fail(data, status, headers, config);
		})
	}
});