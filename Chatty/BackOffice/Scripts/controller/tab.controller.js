(function (chatty) {
    chatty.app.controller("AdminTabCtrl", ['$scope', '$rootScope', function ($scope, $rootScope) {
        $scope.tab = 0;

        $scope.setTab = function (n) {
            $scope.tab = n;
        };

        $scope.getTabClass = function (n) {
            return(n == $scope.tab ? "active" : "");
        };
    }]);
})(window.chatty || window);