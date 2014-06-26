(function (chatty) {
    chatty.app.directive('onEnter', function () {
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
                        scope.$eval(attr.bsOnEnter);
                    });

                    event.preventDefault();
                }
            });
        }
    };
});
})(window.chatty||window);