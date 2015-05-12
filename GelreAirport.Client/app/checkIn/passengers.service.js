(function() {
    "use strict";

    var serviceId = "gaPassengers";
    angular.module("app").factory(serviceId, ["$http", passengerService]);

    function passengerService($http) {
        return {
            list: [],
            query: function (params) {
                var self = this;
                params.fields = "name,passengerNumber,flightNumber";
                return $http.get("/api/passengers", {
                    params: params
                }).success(function (data) {
                    _.each(data, function(passenger) {
                        self.list.push(passenger);
                    });
                });
            }
        }
    }

}());