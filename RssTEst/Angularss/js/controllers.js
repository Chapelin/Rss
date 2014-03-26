'use strict';

/* Controllers */
var baseRssController = angular.module('baseRssController',[]);



var rssContr = function($scope,$http) {
  $scope.test = "RssController";
    $http.get('http://localhost:5555/Entrees/getlastX').success(function(data) {
    	$scope.Rss = data;
  }).error(function(data, status, headers, config){
  	console.log("error : "+data);
  	});
};

var cateContr = function($scope,$http) {
  $scope.test = "CategoriesController";
    $http.get('http://localhost:5555/Categories/GetAll').success(function(data) {
    	$scope.Categories = data;
  }).error(function(data, status, headers, config){
  	console.log("error : "+data);
  	});
};


//SourcesController

var sourceContr = function($scope,$http) {
  $scope.test = "SourcesController";
   $http.get('http://localhost:5555/Categories/GetAll').success(function(data) {
      $scope.Categories = data;
      $scope.CategoriesClassees = {};
      for (var i = 0; i < $scope.Categories.length; i++) {
        $scope.CategoriesClassees[$scope.Categories[i]._id] = $scope.Categories[i];
        };
      
  }).error(function(data, status, headers, config){
    console.log("error : "+data);
    });

    $http.get('http://localhost:5555/Sources/GetAll').success(function(data) {
    	$scope.Sources = data;
  }).error(function(data, status, headers, config){
  	console.log("error : "+data);
  	});
};

//SpecificSourceController

var specificSourceContr = function($scope,$http,$routeParams) {
	$scope.test = "SpecificSourceController";
	$scope.sourceId = $routeParams.sourceId;
	$http.get('http://localhost:5555/Entrees/GetBySourceIdLastX/'+$scope.sourceId).success(function(data) {
    	$scope.Rss = data;
  }).error(function(data, status, headers, config){
  	console.log("error : "+data);
  	});
}



//SpecificCategorieController
var specificCategorieContr = function($scope,$http,$routeParams) {
  $scope.test = "SpecificCategorieController";
  $scope.catId = $routeParams.catId;
  $http.get('http://localhost:5555/Entrees/GetByCatIdLastX/'+$scope.catId).success(function(data) {
      $scope.Rss = data;
  }).error(function(data, status, headers, config){
    console.log("error : "+data);
    });
}


baseRssController.controller('RssController', rssContr);
baseRssController.controller('CategoriesController', cateContr);
baseRssController.controller('SourcesController', sourceContr);
baseRssController.controller('SpecificSourceController', specificSourceContr);
baseRssController.controller('SpecificCategorieController', specificCategorieContr);
