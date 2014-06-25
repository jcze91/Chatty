(function (chatty) {
    chatty.app.controller("AdminTabCtrl", ['$scope', '$rootScope', function ($scope, $rootScope) {
        $scope.tab = 0;

        $scope.setTab = function (n) {
            if ($scope.tab != n) {
                $scope.tab = n;
                if (n == 0)
                    $rootScope.$broadcast('initUsers');
                else if (n == 1)
                    $rootScope.$broadcast('initDepartments');
                else if (n == 2)
                    $rootScope.$broadcast('initConversations');
            }
        };

        $scope.getTabClass = function (n) {
            return(n == $scope.tab ? "active" : "");
        };
    }]);
})(window.chatty || window);