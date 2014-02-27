'use strict';

/* Controllers */
var baseRssModule = angular.module('baseRssModule',[]);


baseRssModule.controller('baseRssController', function($scope,$http) {
  $scope.test = "test";
    $http.get('http://localhost:10793/api/entree/getlast').success(function(data) {
    	$scope.Rss = data;
    	console.log("reponse");
  }).error(function(data, status, headers, config){
  	console.log(data);
  		console.log(status);

console.log(headers);

console.log(config);



  	});
});
