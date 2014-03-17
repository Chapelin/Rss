'use strict';

/* App Module */
var rssApp = angular.module('rssApp',
['ngRoute',
  'baseRssController']);


rssApp.config(['$routeProvider',
	function($routeProvider)
	{
		$routeProvider.
			when('/index',
				{
					 templateUrl: 'index.html',
						controller: 'RssController'
				}).
			when('/Categories',
				{
						 templateUrl: 'partial/categories.html',
						controller: 'CategoriesController'
				}).
			when('/Sources',
				{
						 templateUrl: 'partial/sources.html',
						controller: 'SourcesController'
				}).
			when('/Source/:sourceId',
				{
						 templateUrl: 'partial/source.html',
						controller: 'SpecificSourceController'
				}).
			otherwise({ redirectTo : "/index"});
	}]);
