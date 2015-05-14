(function() {
    "use strict";

    var controllerId = "gaMainController";
    angular.module("app").controller(controllerId, ["$scope", "gaCounters", mainController]);

    function mainController($scope, gaCounters) {
        $scope.gaCounters = gaCounters;
        $scope.step = 0;
        $scope.errors = [];

        $scope.setStep = function(stepNr) {
            $scope.step = stepNr;
        }

        $scope.$watch("step", function (newValue, oldValue) {
            if (newValue === 1) {
                gaCounters.get()
                    .error(function (data) {
                        addError("Er konden geen beschikbare balies worden opgehaald van de server.");
                    });
            }
        });

        $scope.$on("error:checkin", function (event, args) {
            addError(args.message);
        });

        $scope.closeError = function() {
            $scope.errors = [];
        }

        function addError(message) {
            $scope.errors.push({ message: message });
        }
    }
}());