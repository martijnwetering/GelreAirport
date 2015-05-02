(function() {
    "use strict";

    var controllerId = "checkInController";
    angular.module("app").controller(controllerId, ["$scope", "checkInService", checkInController]);

    function checkInController($scope, checkInService) {
        $scope.step = 0;
        $scope.errors = [];

        $scope.counters = [];

        $scope.$watch("step", function(newValue, oldValue) {
            if (newValue === 1) {
                checkInService.getCounters()
                    .success(function(data) {
                        _.each(data, function(c) {
                            $scope.counters.push(c);
                        });
                    })
                    .error(function(data) {
                        addError("Er konden geen beschikbare balies worden opgehaald van de server.");
                });
            }
        });

        $scope.setStep = function(stepNr) {
            $scope.step = stepNr;
        }

        $scope.selectCounter = function (counterNr) {
            checkInService.setSelectCounter(counterNr);
        }

        $scope.isCounterSelected = function(counterNr) {
            return checkInService.getSelectedCounter() === counterNr;
        }

        function addError(message) {
            console.log(message);
            $scope.errors.push({message: message});
        }
    }
}());