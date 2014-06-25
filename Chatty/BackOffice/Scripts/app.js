
(function(chatty) {
    chatty.app = angular.module('chattyApp', ['compile']).factory('chattyService', function(){
        return chatty.chattyService;
    });

    chatty.init = function init(params) {
        //angular.element($("#adminCtrl")).scope().initChat(params);
        $(document).tooltip({
            track: true,
            position: {
                my: "center bottom-5 left-5",
                at: "center top"
            }
        });
    };
    window.init = chatty.init;
})(window.chatty || (chatty = {}));

