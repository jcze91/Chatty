(function (chatty) {
    chatty.app.directive('onBlur', function () {
    return {
        restrict: 'A',
        link: function(scope, element, attr, ngModelCtrl) {
            if (attr.type === 'radio' || attr.type === 'checkbox') {
                return;
            }

            element.bind('blur', function() {
                setTimeout(function() {
                scope.$apply(function() {
                    scope.$eval(attr.onBlur);
                });
            }, 0.05);

                event.preventDefault();
                event.stopPropagation();
            });
        }
    };
});
})(window.chatty || window);