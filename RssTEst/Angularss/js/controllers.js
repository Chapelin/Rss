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
    $http.get('http://localhost:5555/Categories/GetAll').success(function(data) {
    	console.log("reponse : "+data);
    	$scope.Categories = data;
  }).error(function(data, status, headers, config){
  	console.log("error : "+data);
  		console.log(status);
  	});
};


//SourcesController

var sourceContr = function($scope,$http) {
  $scope.test = "SourcesController";
    $http.get('http://localhost:5555/Sources/GetAll').success(function(data) {
    	console.log("reponse : "+data);
    	$scope.Sources = data;
      $scope.SourcesDecoupe = [];
      var temp = [];
      for(var i=0;i<data.length;i++)
      {
          temp.push(data[i]);
          if(i%3==2)
          {
            $scope.SourcesDecoupe.push(temp);
            temp = [];
          }
      }
      if(temp.length>0)
        $scope.SourcesDecoupe.push(temp);
      
  }).error(function(data, status, headers, config){
  	console.log("error : "+data);
  		console.log(status);
  	});
};


baseRssController.controller('RssController', rssContr)
baseRssController.controller('CategoriesController', cateContr)
baseRssController.controller('SourcesController', sourceContr)
