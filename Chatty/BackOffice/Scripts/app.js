
(function(chatty) {
    chatty.app = angular.module('chattyApp', ['compile']);

    chatty.init = function init(params) {
        angular.element($("#adminCtrl")).scope().initChat(params); //params.adminId
    };
    window.initChat = chatty.initChat;
})(window.chatty || (chatty = {}));

