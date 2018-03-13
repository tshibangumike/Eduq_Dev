
angular.module("eduqApp")
    .controller("ToolbarController",
    [
        "$rootScope", "$scope",
        function ($rootScope, $scope) {

            $scope.toggleSideBar = function () {

                $("#sideBar").toggleClass("side-bar-out");
                //$("#content").toggleClass("content-pushed");
                //$("#navBar").toggleClass("navBar-out");

            };

        }
    ]);
