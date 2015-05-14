(function () {
    "use strict";

    var directiveId = "gaSearch";
    angular.module("app").directive(directiveId, function () {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/app/checkIn/partials/search.html",
            scope: {
                search: "&"
            },
            controller: function($scope) {
                $scope.minDate = new Date();
                $scope.opened = false;

                $scope.openDatePicker = function($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.opened = !$scope.opened;
                }
            }
        }
    });

}());