(function (chatty) {
    chatty.app.controller("AdminDepartmentsCtrl", ['$scope', '$rootScope', 'chattyService', function ($scope, $rootScope, chattyService) {
        $scope.currentPage = 0;
        $scope.pageSize = 15;
        $scope.departmentsOrder = 0;
        $scope.totalPageCount = 1;
        $scope.filterDepartment = null;
        $scope.loadingDepartments = true;
        $scope.departments = [];

        $scope.$on('initDepartments', function (e, scope) {
            $scope.getDepartments();
        });

        $scope.numberOfPages = function () {
            return Math.ceil($scope.departments.length / $scope.pageSize);
        };

        $scope.getDepartments = function (e) {
            $scope.loadingDepartments = true;
            chattyService.getUsers($scope.user.id, $scope.user.token, $scope.currentPage, $scope.pageSize, $scope.DepartmentsOrder, $scope.filterDepartment, $.proxy(function (data) {
                $scope.people = data.items;
                $scope.totalPageCount = Math.ceil(data.totalCount / $scope.pageSize);
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
            if (!query.length && $scope.filterDepartment != "") {
                $scope.filterDepartment = "";
                $scope.currentPage = 0;
                $scope.getDepartments();
            }
        };

        $scope.enterFilter = function (query) {
            if ($scope.filterDepartment != query) {
                $scope.filterDepartment = query;
                $scope.currentPage = 0;
                $scope.getDepartments();
            }
        };
    }]);

})(window.chatty || window);