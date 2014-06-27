(function (chatty) {
    chatty.app.controller("AdminDiscussionsCtrl", ['$scope', '$rootScope', 'chattyService', function ($scope, $rootScope, chattyService) {
        $scope.currentPage = 0;
        $scope.pageSize = 10;
        $scope.totalPageCount = 1;
        $scope.loadingDiscussions = true;
        $scope.discussions = [];
        $scope.selectedDiscussion = null;
        $scope.discussionType = "simple";

        $scope.selectDiscussion = function (discussion) {
            if ($scope.discussionType === "simple")
                chattyService.getSimpleDiscussion($scope.user.id, $scope.user.token, discussion.userFromId, discussion.userToId, $.proxy(function (data) {
                    $scope.selectedDiscussion = new Discussion(data);
                    $scope.$apply();
                }, this));
            else if ($scope.discussionType === "group")
                chattyService.getGroupDiscussion($scope.user.id, $scope.user.token, discussion.groupId, $.proxy(function (data) {
                    $scope.selectedDiscussion = new Discussion(data);
                    $scope.$apply();
                }, this));
        };

        $scope.$on('initDiscussions', function (e, scope) {
            $scope.getDiscussions();
        });

        $scope.numberOfPages = function () {
            return Math.ceil($scope.Discussions.length / $scope.pageSize);
        };

        $scope.getDiscussions = function (e) {
            $scope.loadingDiscussions = true;
            if ($scope.discussionType === "simple")
                chattyService.getSimpleDiscussions($scope.user.id, $scope.user.token, $scope.currentPage, $scope.pageSize, $.proxy(function (data) {
                    $scope.getDiscussionsCallback(data);
                }, this));
            else if ($scope.discussionType === "group")
                chattyService.getGroupDiscussions($scope.user.id, $scope.user.token, $scope.currentPage, $scope.pageSize, $.proxy(function (data) {
                    $scope.getDiscussionsCallback(data);
                }, this));
        };

        $scope.getDiscussionsCallback = function (data) {
            $scope.discussions = [];
            for (var i = 0; i < data.Items.length; i++)
                $scope.discussions.push(new Discussion(data.Items[i]));
            $scope.totalPageCount = Math.ceil(data.TotalCount / $scope.pageSize);
            $scope.loadingDiscussions = false;

            $scope.$apply();
        };

        $scope.nextDiscussions = function (e) {
            $scope.currentPage++;
            $scope.getDiscussions();
        };

        $scope.previousDiscussions = function (e) {
            $scope.currentPage--;
            $scope.getDiscussions();
        };
    }
    ]);
})(window.chatty || window);