
angular.module("eduqApp")
    .controller("SideMenuController",
    [
        "$rootScope", "$scope", "appService", "sideMenuItems",
        function ($rootScope, $scope, appService, sideMenuItems) {

            /*Variables - START*/

            $scope.sideMenuItems = sideMenuItems.data;

            /*Variable - END*/

            /*Functions - START*/

            this.init = function () {

            };

            $scope.toggleSideBar = function () {

                $("#sideBar").toggleClass("side-bar-out");
                //$("#content").toggleClass("content-pushed");
                //$("#navBar").toggleClass("navBar-out");

            };

            $scope.expandCollapseSubMenuItems = function () {

                if (arguments.length === 0) return null;
                if (_.isNull(arguments[0]) || _.isUndefined(arguments[0])) return null;

                var item = arguments[0];

                if (_.isNull(item.Collapsed) || _.isUndefined(item.Collapsed) || _.isEqual(item.Collapsed, false)) {
                    item.Collapsed = true;
                }
                else if (_.isEqual(item.Collapsed, true)) {
                    item.Collapsed = false;
                }

                return item;

            };

            $scope.getSideMenuItems = function () {
                return _.filter($scope.sideMenuItems, function (x) { return _.isEqual(x.IsParent, "True"); });
            };

            $scope.getSubSideMenuItems = function () {
                if (arguments.length === 0) return null;
                var menuItemName = arguments[0] == null ? null : arguments[0];
                return _.filter($scope.sideMenuItems,
                    function (x) { return _.isEqual(x.ParentName, menuItemName); });
            };

            $scope.navigateTo = function () {

                if (arguments.length === 0) return null;
                if (_.isNull(arguments[0]) || _.isUndefined(arguments[0])) return null;

                var item = arguments[0];

                var viewName = _.endsWith(item.Name, "y") ? ("read" + (_.replace(item.Name, "y", "ie")) + "s") : ("read" + item.Name + "s");
                appService.NavigateTo(viewName);

                return null;

            };

            /*Functions - END*/

            this.init();

        }
    ]);
