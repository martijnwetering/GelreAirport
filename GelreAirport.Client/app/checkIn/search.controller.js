(function() {
    "use strict";

    var controllerId = "gaSearchController";
    angular.module("app").controller(controllerId, ["$scope", "gaPassengers", searchController]);

    function searchController($scope, gaPassengers) {
        $scope.gaPassengers = gaPassengers;
        $scope.searchMode = true;

        $scope.search = function(searchInput) {
            gaPassengers.query(searchInput)
                .success(function() {
                    $scope.searchMode = false;
                }).error(function() {
                    console.log("error");
                });
        }
    }
}())