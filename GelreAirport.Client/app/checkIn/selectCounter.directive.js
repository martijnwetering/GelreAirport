(function() {
    "use strict";

    var directiveId = "gaSelectCounter";
    angular.module("app").directive(directiveId, function() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app/checkIn/partials/counters.html",
            scope: {
                step: "="
            },
            controller: function($scope, gaCounters) {
                $scope.gaCounters = gaCounters;
            },
            link: function(scope, element, attrs) {
                
            }
        }
    });

}())