(function() {
    "use strict";

    var directiveId = "gaHoverClass";
    angular.module("app").directive(directiveId, function() {
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