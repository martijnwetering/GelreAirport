(function() {
    "use strict";

    var serviceId = "gaPassengers";
    angular.module("app").factory(serviceId, ["$http", '$filter', '$rootScope', passengersService]);

    function passengersService($http, $filter, $rootScope) {

        return {
            list: [],
            passenger: {},
            query: function (searchInput) {
                var self = this;
                var params = {
                    fields: "name,passengerNumber,flightNumber"
                };
                
                _.each(searchInput, function(value, key) {
                    if (typeof value === "string" && value.trim()) {
                        params[key] = value;
                    } else if (typeof value === "object" && value !== null) {
                        params[key] = value;
                    }
                });

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
                        self.passenger.departureDate = data.departureDate;
                        self.passenger.seat = data.seat || "";

                        if (!data.checkInDateTime) {
                            self.passenger.checkInTime = new Date();
                            self.passenger.checkInDateTime = $filter("date")(new Date(), "dd-MMM-yyyy");
                        } else {
                            self.passenger.checkInTime = new Date(data.checkInDateTime);
                            self.passenger.checkInDateTime = $filter("date")(data.checkInDateTime, "dd-MMM-yyyy");
                        }
                    });

                self.queryBaggage(passengerNumber, flightNumber);
            },

            queryBaggage: function (passengerNumber, flightNumber) {
                var self = this;
                return $http.get("/api/baggage/" + passengerNumber + "/" + flightNumber)
                    .success(function (data) {
                        self.passenger.baggage = data;
                    });
            },

            checkinPassenger: function (passenger) {
                var self = this;
                if (typeof passenger.checkInDateTime !== "object") {
                    passenger.checkInDateTime = new Date(passenger.checkInDateTime);
                }

                passenger.checkInDateTime.setHours(passenger.checkInTime.getHours());
                passenger.checkInDateTime.setMinutes(passenger.checkInTime.getMinutes());
                passenger.checkInDateTime.setSeconds(0);

                $http.post("/api/passenger/" + passenger.passengerNumber + "/" + passenger.flightNumber + "/checkin", passenger)
                    .success(function(data) {
                        self.passenger.timeStamp = data;
                        $rootScope.$broadcast("message:general", {message: "Passagier ingecheked"});
                    }).error(function (data, status) {
                        if (status >= 400 && status < 500) {
                            $rootScope.$broadcast("error:checkin", { message: data.message || "Oops, er ging iets mis op de server" });
                        } else {
                            $rootScope.$broadcast("error:checkin", { message: "Oops, er ging iets mis op de server" });
                        }
                    });
            },

            addBaggage: function (baggage) {
                var self = this;
                return $http.post("/api/baggage/" + self.passenger.passengerNumber + "/" + self.passenger.flightNumber + "/checkin", baggage)
                    .success(function(data) {
                        self.queryBaggage(self.passenger.passengerNumber, self.passenger.flightNumber);
                    }).error(function (data, status) {
                        if (status >= 400 && status < 500) {
                            $rootScope.$broadcast("error:general", { message: data.message });
                        } else {
                            $rootScope.$broadcast("error:checkin", { message: "Oops, er ging iets mis op de server" });
                        }
                    });
            },

            removeBaggage: function(id) {
                var self = this;
                return $http.delete("/api/baggage/" + self.passenger.passengerNumber + "/" + self.passenger.flightNumber + "/" + id)
                    .success(function() {
                        var baggageItem;
                        _.each(self.passenger.baggage, function(item) {
                            if (item.id === id) baggageItem = item;
                        });
                        self.passenger.baggage.splice(self.passenger.baggage.indexOf(baggageItem), 1);
                    }).error(function(data, status, headers, config) {
      
                    });
            }
        }
    }

}());