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
					 templateUrl: 'partials/entrees.html',
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
						 templateUrl: 'partials/entrees.html',
						controller: 'SpecificSourceController'
				}).
				when('/Categorie/:catId',
				{
						 templateUrl: 'partials/entrees.html',
						controller: 'SpecificCategorieController'
				}).
			otherwise({ redirectTo : "/Entrees"});
	}]);
