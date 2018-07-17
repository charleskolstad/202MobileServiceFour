angular.module("Admin").controller("FeatureCtrl", function ($http, $scope, dataUrl) {
    $scope.data = {};
    $scope.Feature = {};
    $scope.Edit = true;
    $scope.SelectedPage = 1;
    $scope.PageSize = 12;
    
    $scope.data.Features = [{FeatureID: 1, FeatureName: "Feature", MainFeature: true, FeatureDescription: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eveniet fuga adipisci corporis deleniti debitis error officiis voluptates maxime eos, vel excepturi reprehenderit fugiat esse nobis quo voluptatem dolore assumenda qui.", Active: true}];
    
    $scope.SetupFeature = function(feature){
        $scope.Feature = feature;
        
        $scope.Edit = true;
        angular.element("#featureModal").modal('show');
    }
    
    $scope.NewFeature = function(){
        $scope.Feature.FeatureID = -1;
        $scope.Feature.FeatureName = '';
        $scope.Feature.MainFeature = false;
        $scope.Feature.FeatureDescription = '';
        $scope.Active = false;
        
        $scope.Edit = true;
        angular.element("#featureModal").modal('show');
    }
    
    $scope.SaveFeature = function(feature){
        
    }
    
    $scope.DeleteFeature = function(feature){
        $scope.Feature = feature;
        
        $scope.Edit = false;
        angular.element("#featureModal").modal('show');
    }
    
    $scope.ConfirmDelete = function(){
        
    }
    
    //pagination    
    $scope.SelectPage = function(newPage){
        $scope.SelectedPage = newPage;
    }
})