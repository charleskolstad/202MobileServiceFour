angular.module("Admin").controller("AppRequestCtrl", function ($http, $scope, dataUrl) {
    $scope.data = {};
    $scope.AppRequest = {};
    $scope.ShowEdit = true;
    $scope.ModalPage = 1;

    $http.get(dataUrl + 'Admin/AppRequestGetAll').then(function (data) {
        $scope.data.AppRequest = data.data;
    }), function (error) {
        $scope.data.error = error;
    };

    $scope.GetAllModels = function () {
        $http({
            method: "get",
            url: dataUrl + "Admin/AppRequestGetAll"
        }).then(function (response) {
            $scope.data.AppRequest = response.data;
        }, function () {
            alert("Error Occur");
        })
    }

    $scope.SetupAppRequest = function (model) {
        $scope.AppRequest = model;

        //$scope.ShowEdit = true;
        $scope.ModalPage = 1;
        angular.element("#modelAppRequest").modal('show');
    }

    $scope.ShowPage = function (page) {
        if ($scope.ModalPage == page)
            return true;
        else
            return false;
    }

    $scope.ChangePage = function (page) {
        $scope.ModalPage = page;
    }

    $scope.SaveAppRequest = function (model) {
        $http({
            method: "post",
            url: dataUrl + "",
            datatype: "json",
            data: JSON.stringify(model)
        }).then(function (response) {
            if (response.data == 'Admin/AppRequestSave') {
                alert('App Request Updated!');

                angular.element('#modelAppRequest').modal("hide");
            } else {
                alert(response.data);
            }

            $scope.GetAllModels();
        })
    }
});