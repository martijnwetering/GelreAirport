(function() {
    "use strict";

    var controllerId = "gaCheckInController";
    angular.module("app").controller(controllerId, ["$scope", "gaCounters", checkInController]);

    function checkInController($scope, gaCounters) {
        $scope.gaCounters = gaCounters;
        $scope.step = 0;
        $scope.errors = [];

        //$scope.counters = [];

        $scope.setStep = function(stepNr) {
            $scope.step = stepNr;
        }

        //==================================================
        // Step 1 - Selecting counter
        //==================================================

        $scope.$watch("step", function (newValue, oldValue) {
            if (newValue === 1) {
                gaCounters.get()
                    .error(function (data) {
                        addError("Er konden geen beschikbare balies worden opgehaald van de server.");
                    });
            }
        });

        //$scope.isCounterSelected = function(counterNr) {
        //    return checkInService.getSelectedCounter() === counterNr;
        //}

        //==================================================
        // Step 2 - Search passenger
        //==================================================
        $scope.searchInput = {}
        $scope.minDate = new Date();
        $scope.opened = false;

        $scope.openDatePicker = function($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = !$scope.opened;
        }

        // Utility
        function addError(message) {
            console.log(message);
            $scope.errors.push({message: message});
        }
    }
}());