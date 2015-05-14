(function () {
    "use strict";

    var directiveId = "gaSearchResult";
    angular.module("app").directive(directiveId, function () {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app/checkIn/partials/searchResult.html",
            scope: {
               passengers: "="
            },
            require: "^gaSearchAndCheckin",
            controller: function ($scope, gaPassengers) {
                $scope.gaPassengers = gaPassengers;
                
            },
            link: function(scope, element, attrs, ctrl) {
                scope.select = function (passenger) {
                    scope.gaPassengers.findOne(passenger.passengernumber, passenger.flightnumber);
                    ctrl.setState("checkin");
                }

                scope.back = function ($event) {
                    $event.preventDefault();
                    ctrl.setState("searching");
                }
            }
        }
    });

}());