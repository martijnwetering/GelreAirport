(function () {
    "use strict";

    var controllerId = "gaCheckinPassengerController";
    angular.module("app").controller(controllerId, ["$scope", "gaPassengers", checkinPassengerController]);

    function checkinPassengerController($scope, gaPassengers) {
        $scope.gaPassengers = gaPassengers;
        $scope.passenger = gaPassengers.passenger;

        $scope.minDate = new Date();
        $scope.opened = false;

        $scope.openDatePicker = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = !$scope.opened;
        }

    }
}())