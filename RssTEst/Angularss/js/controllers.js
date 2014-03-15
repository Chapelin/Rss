'use strict';

/* Controllers */
var baseRssModule = angular.module('baseRssModule',[]);


baseRssModule.controller('baseRssController', function($scope,$http) {
  $scope.test = "test";
    $http.get('http://localhost:5555/Entrees/getlastX').success(function(data) {
    	console.log("reponse : "+data);
    	$scope.Rss = data;
    	
  }).error(function(data, status, headers, config){
  	console.log("error : "+data);
  		console.log(status);

console.log(headers);

console.log(config);



  	});
});
