angular.module("Admin", ["ngRoute"]).config(function ($routeProvider) {
    $routeProvider.when("/allUsers", {
        templateUrl: "/Admin/AllUsers"
    }).when("/myProfile", {
        templateUrl: "/Admin/UserProfile"
    }).when("/allFeatures", {
        templateUrl: "/Admin/AllFeatures"
    }).otherwise({

    });
}).constant("dataUrl", "http://localhost:61856/");