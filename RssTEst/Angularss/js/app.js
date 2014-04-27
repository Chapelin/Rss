'use strict';

/* App Module */
var rssApp = angular.module('rssApp',
	['ngRoute',
	'baseRssController']);


rssApp.config(['$routeProvider',
	function($routeProvider)
	{
		$routeProvider.
		when('/Entrees',
		{
			templateUrl: 'partials/Entrees.html',
			controller: 'RssController'
		}).
		when('/Categories',
		{
			templateUrl: 'partials/categories.html',
			controller: 'CategoriesController'
		}).
		when('/Sources',
		{
			templateUrl: 'partials/sources.html',
			controller: 'SourcesController'
		}).
		when('/Source/:sourceId',
		{
			templateUrl: 'partials/Entrees.html',
			controller: 'SpecificSourceController'
		}).
		when('/Categorie/:catId',
		{
			templateUrl: 'partials/Entrees.html',
			controller: 'SpecificCategorieController'
		}).
		otherwise({ redirectTo : "/Entrees"});
	}]);



rssApp.service('GetFromServer',  function($http){
	return function(chemin, sucess, fail){
		$http.get("http://localhost:5555/"+chemin).success(function(data) {
			sucess(data);
		}).error(function(data, status, headers, config){
			fail(data, status, headers, config);
		})
	}
});