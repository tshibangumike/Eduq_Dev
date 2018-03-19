
angular.module("eduqApp")
    .controller("CreateEntityController",
    [
        "$rootScope", "$scope", "appService",
        function ($rootScope, $scope, appService) {

            /*Variables - START*/
            $scope.entity = {};
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
            this.init = function () {
                //$("#table").DataTable({
                //    "serverSide": true,
                //    "processing": true,
                //    "fixedHeader": true,
                //    "ajax": {
                //        "url": "/Entity/GetEntities",
                //        "type": "get",
                //        "datatype": "json"
                //    },
                //    "columns": [
                //        { "data": "Name", "autoWidth": true },
                //        { "data": "DisplayName", "autoWidth": true },
                //        { "data": "PluralName", "autoWidth": true }
                //    ]
                //});
            };
            $scope.getSelectedRecords = function () {
                return $scope.sele;ctedEntityIds
            };
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
                    appService.NavigateTo("updateentity", { entityid: $scope.selectedEntityIds[0] });
                else
                    appService.NavigateTo("updateentity", { entityid: _entity.Id });

            };
            /*Functions - END*/

            this.init();

        }
    ])
    .controller("UpdateEntityController",
    [
        "$rootScope", "$scope", "appService", "entity",
        function ($rootScope, $scope, appService, entity) {

            /*Variables - START*/
            $scope.entity = entity.data;
            /*Variable - END*/

            /*Functions - START*/
            this.init = function () {

            };
            $scope.create = function () {

                appService.NavigateTo("createentity");

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
