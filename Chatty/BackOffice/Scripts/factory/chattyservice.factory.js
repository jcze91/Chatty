(function (chatty) {
    chatty.app.factory('chattyService', ['$rootScope', function ($rootScope) {
        var urlSanitizeRequestRegex = new RegExp("[&~#'{[\\-|`_\\\\^@)\\]°}=+$£¤¨ù%*µ!§:/;\\.,\\?<>]", 'ig');

        return {
            restUrl: '',
            init: function (restUrl) {
                this.restUrl = restUrl;
            },
            _stringify: function(str) {
                return str.replace(urlSanitizeRequestRegex, '');
            },

            _request: function(service, uri, json_callback, success) {
                var self = this;
                var error = function(err, url, json_callback, success) {
                 
                    $rootScope.$broadcast('serviceCallError', err);
                    self._deleteCallback(json_callback);
                };
                this._execRequest("/Services/" + service + "Service.svc/" + uri, this._stringify(json_callback), success, error, {
                    json: true
                });
            },
            _execRequest: function(url, json_callback, success, error, options) {
                var self = this;
                options = options || {};
                $.ajax({
                    url: this.restUrl + url,
                    success: function (json, textStatus, xoptions) {
                        json = json || {};
                        if (json.StatusCode && json.StatusCode == 400) {
                            error(json.StatusDescription, url);
                        } else {
                            success && success(json);
                        }
                    },
                    error: function (xoptions, textStatus) {
                        error("INVALIDEXECUTION|" + textStatus);
                    },
                    scriptCharset: "utf-8",
                    contentType: options.json ? "application/json; charset=utf-8" : "application/x-www-form-urlencoded; charset=UTF-8",
                    dataType: options.json ? "json" : "html"
                });
            },

            _deleteCallback: function(callback) { //for IE < 8
                try {
                    window[callback] && delete window[callback];
                } catch (e) {
                    window[callback] = null;
                }
            },
            _buildUri: function(params) {
                var uri = "?";
                var oneparams = false;
                if (params)
                    try {

                        for (param in params) {
                            try {
                                var key = param;
                                var value = params[param];

                                if (typeof value == "number")
                                    value = "" + value + "";

                                if (!value)
                                    value = "";

                                if (key && typeof value !== "undefined" && value != null) {
                                    if (oneparams)
                                        uri += "&";
                                    uri += key + "=" + encodeURIComponent(value);
                                    oneparams = true;
                                }
                            } catch (e) {
                                this.debug("warn", "Build URI exception");
                            }
                        };
                    } catch (e) {
                        this.debug("warn", "Build URI exception master");
                    }
                return oneparams ? uri : "";
            },
            reportError: function(data) {
                data.liveId = this.liveId;
                this.debug("error", data);
            },
            debug: function(level, msg) {
                if (typeof console == "undefined") {
                    window.console = {
                        log: function() {},
                        debug: function() {},
                        error: function() {},
                        warn: function() {}
                    };
                }
                if ((((level && !msg) || level == "debug") && chatConfig.debug) || (level == "warn" && chatConfig.warn) || (level == "error" && chatConfig.error)) {
                    var message = level && !msg ? level : msg;
                    console && console[msg ? level : "debug"] && console[msg ? level : "debug"]($translate(message));
                }
            },
            getUsers: function (adminId, adminToken, page, pageSize, order, filter, callback_ret) {
                var callback = "getUsers_" + new Date().getTime();
                this._request("User", "GetFilteredUsers/" + adminId + "/" + adminToken
                    + "/" + page + "/" + pageSize + "/" + order + this._buildUri({filter:filter}),
                    callback,
                    $.proxy(function (data) {
                        callback_ret && callback_ret(data);
                        this._deleteCallback(callback);
                    }, this));
            },
        };
    }
    ]);
})(window.chatty || window);