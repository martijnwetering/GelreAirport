(function () {
    "use strict";

    var directiveId = "gaCheckinPassenger";
    angular.module("app").directive(directiveId, function () {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app/checkIn/partials/checkinPassenger.html",
            scope: {
                passenger: "="
            },
            require: "^gaSearchAndCheckin",
            controller: "gaCheckinPassengerController",
            link: function (scope, element, attrs, ctrl) {
                scope.back = function ($event) {
                    $event.preventDefault();
                    ctrl.setState("select");
                }
            }
        }
    });

}());
