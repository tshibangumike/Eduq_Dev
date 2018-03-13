
angular.module("eduqApp")
    .controller("CreateEntityController",
    [
        "$rootScope", "$scope", "appService", "genders",
        function ($rootScope, $scope, appService, genders) {

            /*Variables - START*/
            $scope.entity = {};
            $scope.genders = genders.data;
            $scope.isCollapsed = true;
            /*Variable - END*/

            /*Functions - START*/
            $scope.processForm = function () {

                var keyValues = tca.Functions.SplitObjectIntoArray($scope.entity);
                appService.PostForm("/Entity/CreateEntity", { keyValues: keyValues })
                    .then(function successCallback(response) {

                        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                            tca.Notifications.Toastr.CreateErrorNotification(response.message);
                            return;
                        }

                        tca.Notifications.Toastr.CreateSuccessNotification();
                        var entityId = response.data.message;
                        return appService.NavigateTo("updatevehicleowner", { appuserid: entityId });

                    }, function errorCallback(response) {
                    });

            };
            /*Functions - END*/

        }
    ])
    .controller("ReadEntitiesController",
    [
        "$rootScope", "$scope", "appService", "entities",
        function ($rootScope, $scope, appService, entities) {

            /*Variables - START*/

            $scope.entities = entities.data;
            $scope.selectedEntityIds = [];
            /*Variable - END*/

            /*Functions - START*/
            $scope.selectRecord = function () {

                if (arguments.length === 0) return null;
                var _entity = arguments[0] == null ? null : arguments[0];

                if (_entity.Selected) {
                    //add id in array
                    $scope.selectedEntityIds.push(_entity.Id);
                }
                else {
                    //remove id from array
                    var removedEntities = _.remove($scope.selectedEntityIds, function (x) {
                        return _.isEqual(x, _entity.Id);
                    });
                }

            };
            $scope.create = function () {

                appService.NavigateTo("createentity");

            };
            $scope.read = function () {

                var _entity = null;

                if (arguments.length === 1)
                    _entity = _.isEqual(arguments.length, 1) ? arguments[0] : null;

                if (_.isNull(_entity))
                    appService.NavigateTo("updateentity", { entityId: $scope.selectedEntityIds[0] });
                else
                    appService.NavigateTo("updateentity", { entityId: _entity.Id });

            };
            /*Functions - END*/

        }
    ])
    .controller("UpdateEntityController",
    [
        "$rootScope", "$scope", "$filter", "appService", "entity",
        function ($rootScope, $scope, $filter, appService, entity) {

            /*Variables - START*/
            $scope.entity = entity.data;
            $scope.genders = genders.data;
            /*Variable - END*/

            /*Functions - START*/
            this.init = function () {

            };
            $scope.create = function () {

                appService.NavigateTo("createappuser");

            };
            $scope.processForm = function () {

                var keyValues = tca.Functions.SplitObjectIntoArray($scope.entity);
                appService.PostForm("/Entity/UpdateEntity", { keyValues: keyValues })
                    .then(function successCallback(response) {

                        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                            alert("error");
                            return;
                        }

                        var entityId = response.data.message;
                        return appService.RefreshCurrentState();

                    }, function errorCallback(response) {
                    });

            };
            /*Functions - END*/

            this.init();
        }
    ]);
