(function() {
    "use strict";

    var app = angular.module("app", ["ngRoute", "ui.bootstrap"]);

    app.run(["$route", function ($route) {
        // Include $route to kick start the router.
    }]);
}());