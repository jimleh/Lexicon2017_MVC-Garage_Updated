(function () {

    var app = angular.module("garageApp", []);
    var garageController = function ($scope, $http, $compile) {

        var getData = function () {
            $http.get("../api/garage/get")
                .then(function (response) {
                    $scope.vehicles = response.data;
                    console.log(response.data);
                });
        };
        var postData = function () {
            $http.post("../api/garage/post", $scope.vehicle)
                .then(function (response) {
                    console.log(response.data);
                    getData();
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
                $scope.vehicle = null;
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

        $scope.getDetails = function (id) {
            var input = document.getElementById("input");
            var vehicle = $scope.vehicles[id];
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
                                    + '<dt>Date</dt>'
                                    + '<dd>' + vehicle.Date + '</dd>'
                                + '</dl>'
                                + '<div class="container alert-info">'
                                    + '<input ng-click="getDetails()" class="btn btn-success" type="button" value="Return!" />'
                                    + ' | ' 
                                    + '<input ng-click="editing()" class="btn btn-info" type="button" value="Edit!" />'
                                    + ' | ' 
                                    + '<input ng-click="deleteData()" class="btn btn-danger" type="button" value="Delete!" />'
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
                postData();
            }
            $scope.edit = false;
        };
    };

    app.controller("garageController", ["$scope", "$http", "$compile", garageController]);
}());