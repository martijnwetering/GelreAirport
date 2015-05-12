(function() {
    "use strict";

    angular.module("app").directive("gaHoverClass", function() {
        return {
            restrict: "A",
            scope: {
                gaHoverClass: "@"
            },
            link: function(scope, element) {
                element.on("mouseenter", function() {
                    element.addClass(scope.gaHoverClass);
                });
                element.on("mouseleave", function() {
                    element.removeClass(scope.gaHoverClass);
                });
            }
        };
    });
}());