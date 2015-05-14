(function() {
    "use strict";

    var directiveId = "gaSearchAndCheckin";
    angular.module("app").directive(directiveId, function() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app/checkIn/partials/searchAndCheckin.html",
            scope: {
                step: "="
            },
            controller: "gaSearchAndCheckinController"
        }
    });

}());