(function (chatty) {
    chatty.app.controller("AdminUsersCtrl", ['$scope', '$rootScope', 'chattyService', function ($scope, $rootScope, chattyService) {
        $scope.currentPage = 0;
        $scope.pageSize = 15;
        $scope.usersOrder = 0;
        $scope.totalPageCount = 1;
        $scope.filterUser = "";
        $scope.loadingUsers = true;
        $scope.users = [];

        $scope.$on('initUsers', function (e, scope) {
            $scope.getUsers();
        });

        $scope.numberOfPages = function () {
            return Math.ceil($scope.Users.length / $scope.pageSize);
        };

        $scope.getUsers = function (e) {
            $scope.loadingUsers = true;
            chattyService.getUsers($scope.user.id, $scope.user.token, $scope.currentPage, $scope.pageSize, $scope.usersOrder, $scope.filterUser, $.proxy(function (data) {
                $scope.users = [];
                for (var i = 0; i < data.Items.length; i++)
                    $scope.users.push(new User(data.Items[i]));
                $scope.totalPageCount = Math.ceil(data.TotalCount / $scope.pageSize);
                $scope.loadingUsers = false;

                $scope.$apply();
            }, this));
        };

        $scope.nextUsers = function (e) {
            $scope.currentPage++;
            $scope.getUsers();
        };

        $scope.previousUsers = function (e) {
            $scope.currentPage--;
            $scope.getUsers();
        };

        $scope.checkEmptyFilter = function (query) {
            if (!query.length && $scope.filterUser != "") {
                $scope.filterUser = "";
                $scope.currentPage = 0;
                $scope.getUsers();
            }
        };

        $scope.enterFilter = function (query) {
            if ($scope.filterUser != query) {
                $scope.filterUser = query;
                $scope.currentPage = 0;
                $scope.getUsers();
            }
        };
    }]);

})(window.chatty || window);