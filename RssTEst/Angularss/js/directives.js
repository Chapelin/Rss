'use strict';

/* Directives */
rssApp.directive('favicon', function(env)
{
	return {
		restrict : 'E',
		scope : {
			sourceid : '@'
		},
		template : '<img  height="24" width="24" ng-src="'+env+'Favicons/geticobysource/{{sourceid}}" class="source"/>'
		
	}
})