angular.module("Admin", ["ngRoute"]).config(function ($routeProvider) {
    $routeProvider.when("/allUsers", {
        templateUrl: "allusers.html"
    }).when("/userProfile", {
        templateUrl: "profile.html"
    }).when("/allFeatures", {
        templateUrl: "features.html"
    }).otherwise({

    });
}).constant("dataUrl", "http://localhost:52055/");