(function (chatty) {
    chatty.app.controller("AdminDepartmentsCtrl", ['$scope', '$rootScope', 'chattyService', function ($scope, $rootScope, chattyService) {
        $scope.currentPage = 0;
        $scope.pageSize = 12;
        $scope.departmentsOrder = 0;
        $scope.totalPageCount = 1;
        $scope.filterDepartmentName = "";
        $scope.loadingDepartments = true;
        $scope.departments = [];
        $scope.selectedDepartment = null;
        $scope.newDepartmentName = null;

        $scope.addDepartment = function () {
            chattyService.addDepartment($scope.user.id, $scope.user.token, $scope.newDepartmentName, $.proxy(function (data) {
                if (data == "ERROR")
                    $rootScope.$broadcast('onInfoMessage', {
                        type: "error",
                        message: $scope.newDepartmentName + " already exists."
                    });
                else
                    $rootScope.$broadcast('onInfoMessage', {
                        type: "success",
                        message: $scope.newDepartmentName + " well added."
                    });
                $scope.getDepartments();
                $scope.$apply();
            }, this));
        };

        $scope.selectDepartment = function (department) {
            chattyService.getDepartment($scope.user.id, $scope.user.token, department.id, $.proxy(function (data) {
                $scope.selectedDepartment = new Department(data);
                $scope.$apply();
            }, this));
        };

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
                    $scope.departments.push(new Department(data.Items[i]));
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