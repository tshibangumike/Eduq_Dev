
angular.module("eduqApp")
    .controller("CreateAppUserController",
    [
        "$rootScope", "$scope", "appService", "genders",
        function ($rootScope, $scope, appService, genders) {

            /*Variables - START*/
            $scope.appUser = {};
            $scope.genders = genders.data;
            $scope.isCollapsed = true;
            /*Variable - END*/

            /*Functions - START*/
            $scope.processForm = function () {

                var keyValues = tca.Functions.SplitObjectIntoArray($scope.appUser);
                appService.PostForm("/AppUser/CreateAppUser", { keyValues: keyValues })
                    .then(function successCallback(response) {

                        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                            tca.Notifications.Toastr.CreateErrorNotification(response.message);
                            return;
                        }

                        tca.Notifications.Toastr.CreateSuccessNotification();
                        var appUserId = response.data.message;
                        return appService.NavigateTo("updatevehicleowner", { appuserid: appUserId });

                    }, function errorCallback(response) {
                    });

            };
            /*Functions - END*/

        }
    ])
    .controller("ReadAppUsersController",
    [
        "$rootScope", "$scope", "appService", "appUsers",
        function ($rootScope, $scope, appService, appUsers) {

            /*Variables - START*/

            $scope.appUsers = appUsers.data;
            $scope.selectedAppUserIds = [];
            /*Variable - END*/

            /*Functions - START*/
            $scope.selectRecord = function () {

                if (arguments.length === 0) return null;
                var _appUser = arguments[0] == null ? null : arguments[0];

                if (_vehicle.Selected) {
                    //add id in array
                    $scope.selectedAppUserIds.push(_appUser.Id);
                }
                else {
                    //remove id from array
                    var removedAppUsers = _.remove($scope.selectedAppUserIds, function (x) {
                        return _.isEqual(x, _appUser.Id);
                    });
                }

            };
            $scope.create = function () {

                appService.NavigateTo("createappuser");

            };
            $scope.read = function () {

                var _appUser = null;

                if (arguments.length === 1)
                    _appUser = _.isEqual(arguments.length, 1) ? arguments[0] : null;

                if (_.isNull(_vehicle))
                    appService.NavigateTo("updateappuser", { appUserId: $scope.selectedAppUserIds[0] });
                else
                    appService.NavigateTo("updateappuser", { appUserId: _appUser.Id });

            };
            /*Functions - END*/

        }
    ])
    .controller("UpdateAppUserController",
    [
        "$rootScope", "$scope", "$filter", "appService", "appUser", "genders",
        function ($rootScope, $scope, $filter, appService, appUser, genders) {

            /*Variables - START*/
            $scope.appUser = appUser.data;
            $scope.genders = genders.data;
            /*Variable - END*/

            /*Functions - START*/
            this.Init = function () {
                $scope.appUser["DateOfBirth"] = $scope.appUser["DateOfBirth"] ?
                    $filter("date")(parseInt($scope.appUser["DateOfBirth"].substr(6)), "dd/MM/yyyy") :
                    "";
            };
            $scope.create = function () {

                appService.NavigateTo("createappuser");

            };
            $scope.processForm = function () {

                var keyValues = tca.Functions.SplitObjectIntoArray($scope.appUser);
                appService.PostForm("/AppUser/UpdateAppUser", { keyValues: keyValues })
                    .then(function successCallback(response) {

                        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                            alert("error");
                            return;
                        }

                        var appUserId = response.data.message;
                        return appService.RefreshCurrentState();

                    }, function errorCallback(response) {
                    });

            };
            /*Functions - END*/

            this.Init();
        }
    ]);
