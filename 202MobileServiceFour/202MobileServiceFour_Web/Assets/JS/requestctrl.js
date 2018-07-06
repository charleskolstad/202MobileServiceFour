angular.module("requestApp").controller("requestCtrl", function($scope){
    $scope.WizardStep = 1;
    
    $scope.ShowWizardStep = function(step){
        if(step == $scope.WizardStep)
            return true;
        else
            return false;
    }
    
    $scope.MoveWizard = function(step){
        $scope.WizardStep = step;
    }
});