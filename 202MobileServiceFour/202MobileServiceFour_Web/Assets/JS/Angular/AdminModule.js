angular.module("Admin", ["ngRoute"]).config(function ($routeProvider) {
    $routeProvider.when("/allUsers", {
        templateUrl: "/Admin/AllUsers"
    }).when("/myProfile", {
        templateUrl: "/Admin/UserProfile"
    }).when("/allFeatures", {
        templateUrl: "/Admin/AllFeatures"
    }).when("/allRequests", {
        templateUrl: "/Admin/AppRequests"
    }).otherwise({
        
    });
}).constant("dataUrl", "http://localhost:61856/");