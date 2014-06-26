(function(chatty){
    chatty.app.directive('numbersOnly', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function(scope, element, attr, ngModelCtrl) {
            ngModelCtrl.$parsers.push(function(inputValue) {
                // this next if is necessary for when using ng-required on your input.
                // In such cases, when a letter is typed first, this parser will be called
                // again, and the 2nd time, the value will be undefined
                if (inputValue == undefined) {
                    return '';
                }

                var transformedInput = inputValue.replace(/(?!^-)[^0-9.]/g, "");
                if (transformedInput !== inputValue) {
                    ngModelCtrl.$setViewValue(transformedInput);
                    ngModelCtrl.$render();
                }

                return transformedInput;  // or return Number(transformedInput)
            });
        }
    };
});
})(window.chatty || window);