(function (chatty) {
    chatty.app.controller("AdminDepartmentsCtrl", ['$scope', '$rootScope', 'chattyService', function ($scope, $rootScope, chattyService) {
        $scope.currentPage = 0;
        $scope.pageSize = 15;
        $scope.departmentsOrder = 0;
        $scope.totalPageCount = 1;
        $scope.filterDepartmentName = "";
        $scope.loadingDepartments = true;
        $scope.departments = [];

        $scope.$on('initDepartments', function (e, scope) {
            $scope.getDepartments();
        });

        $scope.numberOfPages = function () {
            return Math.ceil($scope.Departments.length / $scope.pageSize);
        };

        $scope.getDepartments = function (e) {
            $scope.loadingDepartments = true;
            chattyService.getDepartments($scope.user.id, $scope.user.token, $scope.currentPage, $scope.pageSize, $scope.departmentsOrder, $scope.filterUser, $.proxy(function (data) {
                $scope.departments = [];
                for (var i = 0; i < data.Items.length; i++)
                    $scope.departments.push(new User(data.Items[i]));
                $scope.totalPageCount = Math.ceil(data.TotalCount / $scope.pageSize);
                $scope.loadingDepartments = false;

                $scope.$apply();
            }, this));
        };

        $scope.nextDepartments = function (e) {
            $scope.currentPage++;
            $scope.getDepartments();
        };

        $scope.previousDepartments = function (e) {
            $scope.currentPage--;
            $scope.getDepartments();
        };

        $scope.checkEmptyFilter = function (query) {
            if (!query.length && $scope.filterUser != "") {
                $scope.filterUser = "";
                $scope.currentPage = 0;
                $scope.getDepartments();
            }
        };

        $scope.enterFilter = function (query) {
            if ($scope.filterUser != query) {
                $scope.filterUser = query;
                $scope.currentPage = 0;
                $scope.getDepartments();
            }
        };
    }]);

})(window.chatty || window);