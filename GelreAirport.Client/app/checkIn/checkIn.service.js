(function() {
    "use strict";

    var serviceId = "checkInService";
    angular.module("app").factory(serviceId, ["$http", checkInService]);

    function checkInService($http) {
        var baseUrl = "/api/checkin";
        var selectedCounter = undefined;

        return {
            getCounters: function() {
                return $http.get(baseUrl + "/counters");
            },

            setSelectCounter: function(counterNr) {
                selectedCounter = counterNr;
            },

            getSelectedCounter: function() {
                return selectedCounter;
            }
        };
    }
}());