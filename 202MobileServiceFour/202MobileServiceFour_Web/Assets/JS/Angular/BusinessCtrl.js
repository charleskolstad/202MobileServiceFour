angular.module("Admin").controller("BusinessCtrl", function ($http, $scope, dataUrl) {
    $scope.data = {};
    $scope.Business = {};
    $scope.ModalPage = 1;

    $http.get(dataUrl + '').then(function (data) {
        $scope.data.Businesses = data.data;
    }), function (error) {
        $scope.data.error = error;
    };

    $scope.GetAllBusinesses = function () {
        $http({
            method: "get",
            url: dataUrl + ""
        }).then(function (response) {
            $scope.data.Businesses = response.data;
        }, function () {
            alert("Error Occur");
        })
    }

    $scope.SetupBusiness = function (model) {
        $scope.Business = model;

        $scope.ShowEdit = true;
        angular.element("#businessModal").modal('show');
    }

    $scope.ShowPage = function (page) {
        if ($scope.ModalPage == page)
            return true;
        else
            return false;
    }

    $scope.SaveModel = function (model) {
        $http({
            method: "post",
            url: dataUrl + "",
            datatype: "json",
            data: JSON.stringify(model)
        }).then(function (response) {
            if (response.data == '') {
                alert('Success!');

                angular.element('#businessModal').modal("hide");
            } else {
                alert(response.data);
            }

            $scope.GetAllBusinesses();
        })
    }
});