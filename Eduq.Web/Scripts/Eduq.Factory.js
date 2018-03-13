angular.module("eduqApp")
    .factory("httpLoader", [
        "$rootScope", "$q", function ($rootScope, $q) {
            var pendingRequests = 0;
            return {
                request: function (config) {
                    pendingRequests++;
                    eduq.Loading.Start($rootScope.LoadingText);
                    return config || $q.when(config);
                },
                response: function (response) {
                    if ((--pendingRequests) === 0) {
                        eduq.Loading.Stop();
                    }
                    return response || $q.when(response);
                },
                responseError: function (response) {
                    if ((--pendingRequests) === 0) {
                        eduq.Loading.Stop();
                    }
                    return $q.reject(response);
                }
            };
        }
    ]);

