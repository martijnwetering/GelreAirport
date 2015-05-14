(function() {
    "use strict";

    var serviceId = "gaPassengers";
    angular.module("app").factory(serviceId, ["$http", '$filter', '$rootScope', passengerService]);

    function passengerService($http, $filter, $rootScope) {

        return {
            list: [],
            passenger: {},
            query: function (params) {
                var self = this;
                params.fields = "name,passengerNumber,flightNumber";
                return $http.get("/api/passengers", {
                    params: params
                }).success(function (data) {
                    self.list = [];
                    _.each(data, function (passenger) {
                        self.list.push(passenger);
                    });
                });
            },

            findOne: function (passengerNumber, flightNumber) {
                var self = this;
                $http.get("/api/passenger/" + passengerNumber + "/" + flightNumber)
                    .success(function (data) {
                        self.passenger.name = data.name;
                        self.passenger.gender = data.gender;
                        self.passenger.birthDate = data.birthDate;
                        self.passenger.passengerNumber = data.passengerNumber;
                        self.passenger.flightNumber = data.flightNumber;
                        self.passenger.timeStamp = data.timeStamp;
                        self.passenger.seat = data.seat || "";

                        if (!data.checkInDateTime) {
                            self.passenger.checkInTime = new Date();
                            self.passenger.checkInDateTime = $filter("date")(new Date(), "dd-MMM-yyyy");
                        } else {
                            self.passenger.checkInTime = new Date(data.checkInDateTime);
                            self.passenger.checkInDateTime = $filter("date")(data.checkInDateTime, "dd-MMM-yyyy");
                        }
                    });
                $http.get("/api/baggage/" + passengerNumber + "/" + flightNumber)
                    .success(function(data) {
                        self.passenger.baggage = data;
                    });
            },

            checkinPassenger: function (passenger) {
                if (typeof passenger.checkInDateTime !== "object") {
                    passenger.checkInDateTime = new Date(passenger.checkInDateTime);
                }

                passenger.checkInDateTime.setHours(passenger.checkInTime.getHours());
                passenger.checkInDateTime.setMinutes(passenger.checkInTime.getMinutes());
                passenger.checkInDateTime.setSeconds(0);

                $http.post("/api/passenger/" + passenger.passengerNumber + "/" + passenger.flightNumber + "/checkin", passenger)
                    .success(function() {

                    }).error(function() {
                        $rootScope.$broadcast("error:checkin", { message: "Passagier kon niet ingechecked worden. Zoekt u a.u.b. opnieuw." });
                    });
            }
        }
    }

}());