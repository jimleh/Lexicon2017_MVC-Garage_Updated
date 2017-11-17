(function () {

    var app = angular.module("garageApp", []);
    var garageController = function ($scope, $http, $compile) {

        var getData = function () {
            $http.get("../api/garage/getgarage")
                .then(function (response) {
                    $scope.garage = response.data.ParkingSpots;
                    $scope.vehicles = response.data.Vehicles;
                });
        };
        var postData = function () {
            $http.post("../api/garage/post", $scope.vehicle)
                .then(function (response) {
                    console.log(response.data);
                    getData();
                    $scope.vehicle = {
                        Type: "Car"
                    };
                });
        };
        var editData = function () {
            $http.put("../api/garage/put/" + $scope.vehicle.ID, $scope.vehicle)
                .then(function (response) {
                    console.log(response.data);
                    $scope.vehicle = null;
                });
        };
        var getTypes = function () {
            $http.get("../api/garage/gettypes")
                .then(function (response) {
                    $scope.types = response.data;
                });
        };

        $scope.edit = false;
        $scope.vehicle = { ID: 0 };
        $scope.vehicles;

        $scope.initData = function () {
            getData();
            getTypes();
        };

        $scope.deleteData = function (id) {
            $http.delete("../api/garage/delete/" + id)
                .then(function (response) {
                    console.log(response.data);
                    getData();
                });
        };

        $scope.editing = function (vehicle) {
            var input = document.getElementById("input");
            var submitBtn = document.getElementById("submitBtn");
            if ($scope.edit) {
                $scope.edit = false;
                input.classList.remove("alert-success");
                submitBtn.value = "Add!"
                $scope.vehicle = {
                    Type: "Car"
                };
                // remove abort button
                var d = document.getElementById("abortBtn");
                d.parentNode.removeChild(d);
            }
            else {
                $scope.edit = true;
                $scope.vehicle = vehicle;
                input.classList.add("alert-success");
                submitBtn.value = "Edit!"
                // Add a new button to abort editing
                var div = angular.element(input.children.item(0));
                div.append(
                    $compile('<input id="abortBtn" ng-click="editing()" class="btn btn-warning" type="button" value="Abort!"/>')($scope)
                );
            }
        };

        $scope.getDetails = function (vehicle) {
            var input = document.getElementById("input");
            if (!$scope.details) {
                $scope.details = true;
                var div = angular.element(input);
                div.append(
                    $compile('<div id="detailsDiv" class="container form-group">'
                                + '<dl class="dl-horizontal">'
                                    + '<dt>ID</dt>'
                                    + '<dd>' + vehicle.ID + '</dd>'
                                    + '<dt>Reg</dt>'
                                    + '<dd>' + vehicle.Reg + '</dd>'
                                    + '<dt>Type</dt>'
                                    + '<dd>' + vehicle.Type + '</dd>'
                                    + '<dt>Parking Spot</dt>'
                                    + '<dd>' + vehicle.Spot + '</dd>'
                                    + '<dt>Date</dt>'
                                    + '<dd>' + vehicle.Date + '</dd>'
                                + '</dl>'
                                + '<div class="container alert-info">'
                                    + '<input ng-click="getDetails()" class="btn btn-success" type="button" value="Return!" />'
                                + '</div>'
                            + '</div>')($scope));     
            }
            else {
                $scope.details = false;
                var detailsDiv = document.getElementById("detailsDiv");
                detailsDiv.parentNode.removeChild(detailsDiv);
            }
        };

        $scope.checkForm = function () {
            if ($scope.edit) {
                editData();
                document.getElementById("input").classList.remove("alert-success");
                document.getElementById("submitBtn").value = "Add!"
            }
            else {
                $scope.vehicle.ID = 0;
                $scope.vehicle.Spot = 0;
                postData();
            }
            $scope.edit = false;
        };

        $scope.getGarage = function () {
            var garageBtn = document.getElementById("garageBtn");
            var garageTableDiv = document.getElementById("garageTableDiv");
            if (!$scope.showGarage) {
                $scope.showGarage = true;
                garageBtn.value = "Hide Garage";
                garageTableDiv.classList.remove("hidden");
                garageTableDiv.classList.remove("disabled");
            }
            else {
                $scope.showGarage = false;
                garageBtn.value = "Show Garage";
                garageTableDiv.classList.add("hidden");
                garageTableDiv.classList.add("disabled");
                //var gargaeTableDiv = document.getElementById("garageTableDiv");
                //gargaeTableDiv.parentNode.removeChild(gargaeTableDiv);
            }
        };
    };

    app.controller("garageController", ["$scope", "$http", "$compile", garageController]);
}());