(function () {

    var app = angular.module("garageApp", []);
    var garageController = function ($scope, $http) {

        var GetTypes = function () {
            var types = $scope.vehicles.filter(function (vehicle) {
                if (!$scope.vehicles.includes(vehicle.VehicleType)) {
                    $scope.types.push(vehicle.VehicleType);
                }
            });
        };
        var GetData = function () {
            $http.get("../api/garage/get")
                .then(function (response) {
                    $scope.vehicles = response.data;
                    GetTypes();
                });
        };

        $scope.vehicles = [];
        $scope.types = [];
        $scope.Vehicle = {
            VehicleID: 0,
            VehicleType: 1
        };

        $scope.initData = function () {
            GetData();
        };

        $scope.postData = function () {

            for (var i = 0; i < $scope.vehicles.length; i++) {
                if (!$scope.Vehicle.VehicleID || $scope.vehicles[i].VehicleID >= $scope.Vehicle.VehicleID) {
                    $scope.Vehicle.VehicleID = $scope.vehicles[i].VehicleID + 1;
                };
            };

            $scope.Vehicle.VehicleDate = new Date();
            $scope.Vehicle.VehicleSize = $scope.Vehicle.VehicleType;
            $http.post("../api/garage/post", $scope.Vehicle)
            .then(function (response) {
                console.log(response);
                GetData();
            });
        };

    };

    app.controller("garageController", ["$scope", "$http", garageController]);
}());