angular.module("Admin").controller("FeatureCtrl", function ($http, $scope, dataUrl) {
    $scope.data = {};
    $scope.Feature = {};
    $scope.Edit = true;

    $http.get(dataUrl + 'Admin/GetAllFeatures').then(function (data) {
        $scope.data.Features = data.data;
    }), function (error) {
        $scope.data.error = error;
    };
    
    $scope.GetAllFeatures = function () {
        $http({
            method: "get",
            url: dataUrl + "Admin/GetAllFeatures"
        }).then(function (response) {
            $scope.data.Features = response.data;
        }, function () {
            alert("Error Occur");
        })
    }

    $scope.SetupFeature = function (feature) {
        $scope.Feature = feature;

        $scope.Edit = true;
        angular.element("#featureModal").modal('show');
    }

    $scope.NewFeature = function () {
        $scope.Feature.FeatureID = -1;
        $scope.Feature.FeatureName = '';
        $scope.Feature.MainFeature = false;
        $scope.Feature.FeatureDescription = '';
        $scope.Active = false;

        $scope.Edit = true;
        angular.element("#featureModal").modal('show');
    }

    $scope.SaveFeature = function (feature) {
        let save = (feature.FeatureID == undefined);
        let dataDestination =(save)? 'SaveFeature':'UpdateFeature';
        
        $http({
            method: "post",
            url: dataUrl + "Admin/" + dataDestination,
            datatype: "json",
            data: JSON.stringify(feature)
        }).then(function (response) {
            if (response.data == '') {
                if (save)
                    alert('Feature saved successfully!');
                else
                    alert('Feature updated successfully!');

                angular.element('#featureModal').modal("hide");
            } else {
                alert(response.data);
            }
            $scope.GetAllFeatures();
        })
    }

    $scope.DeleteFeature = function (feature) {
        $scope.Feature = feature;

        $scope.Edit = false;
        angular.element("#featureModal").modal('show');
    }

    $scope.ConfirmDelete = function () {
        $http({
            method: "post",
            url: dataUrl + "Admin/DeleteFeature",
            datatype: "json",
            data: JSON.stringify({featureID: $scope.Feature.FeatureID})
        }).then(function (response) {
            if (response.data == '') {
                alert('Feature removed successfully.');
                angular.element('#featureModal').modal("hide");
            } else {
                alert(response.data);
            }

            $scope.GetAllFeatures();
        })
    }
})