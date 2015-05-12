(function() {
    "use strict";

    var serviceId = "gaCounters";
    angular.module("app").factory(serviceId, ["$http", countersService]);

    function countersService($http) {
        var baseUrl = "/api/checkin";
        var selectedCounter = undefined;

        return {
            selectedCounter: null,
            list: [],
            get: function() {
                var self = this;
                return $http.get("/api/counters")
                    .success(function(data) {
                        _.each(data, function(counterNr) {
                            self.list.push(counterNr);
                        });
                    });
            }
        };
    }
}());