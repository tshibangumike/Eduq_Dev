
angular.module("eduqApp")
    .config([
        "$stateProvider", "$urlRouterProvider", "$httpProvider",
        function ($stateProvider, $urlRouterProvider, $httpProvider) {

            $httpProvider.interceptors.push("httpLoader");
            $urlRouterProvider.otherwise("/Home");

            var states = [
                eduq.Routes.SetRoutes("Home", "Home"),
                eduq.Routes.SetRoutes("Entity", "Create"),
                eduq.Routes.SetRoutes("Entity", "Read"),
                eduq.Routes.SetRoutes("Entity", "Update"),
                eduq.Routes.SetRoutes("Field", "Create"),
                eduq.Routes.SetRoutes("Field", "Read"),
                eduq.Routes.SetRoutes("Field", "Update"),
            ];

            states.forEach(function (state) {
                $stateProvider.state(state);
            });

        }
    ]);
