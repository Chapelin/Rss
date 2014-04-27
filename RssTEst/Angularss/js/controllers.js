'use strict';

/* Controllers */
var baseRssController = angular.module('baseRssController',[]);

var rssContr = function($scope, GetFromServer) {
  $scope.test = "RssController";
  this.SuccessFind = function(data)
  {
    $scope.Rss = data;
  };

  this.ErrorFind = function(data, status, headers, config)
  {
    console.log("error : "+data + " " + status);
  }

  GetFromServer("Entrees/getlastX", this.SuccessFind, this.ErrorFind);

  
};

var cateContr = function($scope,GetFromServer) {
 this.ErrorFind = function(data, status, headers, config)
 {
   console.log("error : "+data + " " + status);
}

this.SuccessFind = function(data)
{
  $scope.Categories = data;
};
$scope.test = "CategoriesController";
GetFromServer('Categories/GetAll', this.SuccessFind, this.ErrorFind);


};

//SourcesController

var sourceContr = function($scope, GetFromServer) {
  $scope.test = "SourcesController";

  this.ErrorFind = function(data, status, headers, config)
  {
     console.log("error : "+data + " " + status);
  }

  this.SuccessFindCategories = function(data)
  {
    $scope.Categories = data;
    $scope.CategoriesClassees = {};
    for (var i = 0; i < $scope.Categories.length; i++) {
      $scope.CategoriesClassees[$scope.Categories[i]._id] = $scope.Categories[i];
    };
  };

  this.SuccessFindSources = function(data)
  {
    $scope.Sources = data;
  }

  GetFromServer('Categories/GetAll', this.SuccessFindCategories, this.ErrorFind);

  GetFromServer('Sources/GetAll', this.SuccessFindSources, this.ErrorFind);
  
};

//SpecificSourceController

var specificSourceContr = function($scope,$routeParams, GetFromServer) {
	$scope.test = "SpecificSourceController";
	$scope.sourceId = $routeParams.sourceId;
  this.SuccessFindSources = function(data)
  {
    $scope.Rss = data;
  }
  this.ErrorFind = function(data, status, headers, config)
  {
    console.log("error : "+data + " " + status);
  }
  GetFromServer('Entrees/GetBySourceIdLastX/'+$scope.sourceId,  this.SuccessFindSources, this.ErrorFind);


}



//SpecificCategorieController
var specificCategorieContr = function($scope,$routeParams, GetFromServer) {
  $scope.test = "SpecificCategorieController";
  $scope.catId = $routeParams.catId;

  this.SuccessFindCat = function(data)
  {
    $scope.Rss = data;
  }
  this.ErrorFind = function(data, status, headers, config)
  {
   console.log("error : "+data + " " + status);
  }

  GetFromServer('Entrees/GetByCatIdLastX/'+$scope.catId, this.SuccessFindCat, this.ErrorFind);

}


baseRssController.controller('RssController', rssContr);
baseRssController.controller('CategoriesController', cateContr);
baseRssController.controller('SourcesController', sourceContr);
baseRssController.controller('SpecificSourceController', specificSourceContr);
baseRssController.controller('SpecificCategorieController', specificCategorieContr);
