'use strict';

/* Controllers */
var baseRssController = angular.module('baseRssController',[]);



var rssContr = function($scope,$http) {
  $scope.test = "RssController";
    $http.get('http://localhost:5555/Entrees/getlastX').success(function(data) {
    	console.log("reponse : "+data);
    	$scope.Rss = data;
  }).error(function(data, status, headers, config){
  	console.log("error : "+data);
  		console.log(status);
  	});
};

var cateContr = function($scope,$http) {
  $scope.test = "CategoriesController";
    $http.get('http://localhost:5555//Categories/GetAll').success(function(data) {
    	console.log("reponse : "+data);
    	$scope.Categories = data;
  }).error(function(data, status, headers, config){
  	console.log("error : "+data);
  		console.log(status);
  	});
};


baseRssController.controller('CategoriesController', cateContr);


baseRssController.controller('RssController', rssContr);
