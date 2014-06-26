(function (chatty) {
    chatty.app.directive('onBlurEnter', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function(scope, element, attr, ngModelCtrl) {
            if (attr.type === 'radio' || attr.type === 'checkbox') {
                return;
            }

            element.bind("keydown keypress", function(event) {
                if (event.which === 13) {
                    scope.$apply(function() {
                        scope.$eval(attr.bsOnBlurEnter);
                    });

                    event.preventDefault();
                }
            });

            element.bind('blur', function() {
                scope.$apply(function() {
                    scope.$eval(attr.bsOnBlurEnter);
                });

                event.preventDefault();
            });
        }
    };
});
})(window.chatty || window);