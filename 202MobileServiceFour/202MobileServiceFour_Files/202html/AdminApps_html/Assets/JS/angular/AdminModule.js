angular.module("Admin", ["ngRoute"]).config(function ($routeProvider) {
    $routeProvider.when("/allUsers", {
        templateUrl: "allusers.html"
    }).when("/userProfile", {
        templateUrl: "profile.html"
    }).when("/allFeatures", {
        templateUrl: "features.html"
    }).when("/appRequests", {
        templateUrl: "apprequest.html"
    }).when("/allBusiness", {
        templateUrl: "business.html"
    }).otherwise({

    });
}).filter("range", function($filter){
  return function(data, page, size){
      if(angular.isArray(data) && angular.isNumber(page) && angular.isNumber(size)){
          let start_index = (page - 1) * size;
          if(data.length < start_index){
              return [];
          }else{
              return $filter("limitTo")(data.splice(start_index), size);
          }
      }else{
          return data;
      }
  }  
}).filter("pageCount", function(){
    return function(data, size){
        if(angular.isArray(data)){
            let result = [];
            for (let i = 0; i < Math.ceil(data.length / size); i++){
                result.push(i);
            }
            return result;
        }else{
            return data;
        }
    }
}).constant("dataUrl", "http://localhost:52055/");