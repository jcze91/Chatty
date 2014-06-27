(function (chatty) {
    chatty.app.controller("AdminCtrl", ['$scope', '$rootScope', 'chattyService', function ($scope, $rootScope, chattyService) {
        $scope.user = null;

        $scope.init = function init(attributes) {
            $scope.user = new User({ Token: attributes.adminToken, Id: attributes.adminId, UserName: attributes.userName });
            chattyService.init(attributes.restUrl);
            $rootScope.$broadcast('initUsers');
        };
        $scope.hideMessage = function () {
            $("#response").animate({
                opacity: 0
            }, 1000, $.proxy(function () {
                $("#response").hide();
            }, this));
        };

        $scope.showMessage = function (text, duration, type) {
            if (!type) type = "info";
            $("#response").text(text).removeClass("message_info message_success message_error").addClass("message_" + type).show().animate({
                opacity: 0.9
            }, 500, $.proxy(function () {
                if (duration !== 0)
                    setTimeout($scope.hideMessage, duration ? duration : 5000);
            }, this));
        };
        $rootScope.$on('onInfoMessage', function (e, data) {
            if (data.message) {
                $scope.showMessage(data.message, data.duration, data.type);
            }
        });
    }]);

})(window.chatty || window);