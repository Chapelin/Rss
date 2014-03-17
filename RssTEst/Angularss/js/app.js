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
						 templateUrl: 'partials/source.html',
						controller: 'SpecificSourceController'
				}).
			otherwise({ redirectTo : "/Entrees"});
	}]);
