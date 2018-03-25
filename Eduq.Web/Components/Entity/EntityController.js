
angular.module("eduqApp")
    .controller("CreateEntityController",
    [
        "$rootScope", "$scope", "appService",
        function ($rootScope, $scope, appService) {

            /*Variables - START*/
            $scope.entity = {};
            $scope.formHasBeenSubmitted = false;
            /*Variable - END*/

            /*Functions - START*/
            $scope.processForm = function () {

                if (arguments.length === 0) return null;
                var form = arguments[0] == null ? null : arguments[0];

                $scope.formHasBeenSubmitted = true;

                if (!form.$valid) {
                    return null;
                }

                var keyValues = eduq.Functions.SplitObjectIntoArray($scope.entity);
                appService.PostForm("/Entity/CreateEntity", { keyValues: keyValues })
                    .then(function successCallback(response) {

                        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                            eduq.Notifications.Toastr.CreateErrorNotification(response.data.message);
                            return;
                        }

                        eduq.Notifications.Toastr.CreateSuccessNotification();
                        var entityId = response.data.message;

                        return appService.NavigateTo("updateentity", { entityid: entityId });

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
                return $scope.selectedEntityIds
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
        "$rootScope", "$scope", "dialogs", "appService", "entity",
        function ($rootScope, $scope, dialogs, appService, entity) {

            /*Variables - START*/
            $scope.entity = entity.data;
            $scope.formHasBeenSubmitted = false;
            $scope.fields = null;
            $scope.parentSideMenuItems = null;
            $scope.Tabs = [
                { Name: "SideBar", Show: false },
                { Name: "Attributs", Show: false },
                { Name: "Forms", Show: false }
            ];
            $scope.htmlBinding = eduq.HtmlBinding;
            $scope.selectedFieldIds = [];
            $scope.selectedFormIds = [];
            $scope.selectedViewIds = [];
            /*Variable - END*/

            /*Functions - START*/
            this.init = function () {

            };
            $scope.collapseExpand = function () {

                if (arguments.length === 0) return null;
                var elementId = arguments[0] == null ? null : arguments[0];

                var tab = _.find($scope.Tabs, function (x) { return _.isEqual(x.Name, elementId); });
                if (_.isUndefined(tab)) return null;
                tab.Show = !tab.Show;

                if (tab.Show) {
                    if (_.isEqual(tab.Name, "SideBar")) {
                        $scope.getParentSideMenuItems();
                    }
                    else if (_.isEqual(tab.Name, "Attributs")) {
                        $scope.getAttributs();
                    }
                }
            };
            $scope.create = function () {

                appService.NavigateTo("createentity");

            };
            $scope.delete = function () {

                dialogs.confirm("Confirmation", 'Do you want really do this action?')
                    .result
                    .then(function () { console.log("Yes"); })
                    .then(function () { console.log("No"); });

                //appService.GetData("/Entity/DeleteEntity", { entityId: $scope.entity.Id })
                //    .then(function successCallback(response) {

                //        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                //            eduq.Notifications.Toastr.UpdateErrorNotification(response.data.message);
                //            return;
                //        }

                //        eduq.Notifications.Toastr.UpdateSuccessNotification();
                //        appService.NavigateTo("readentities");

                //    }, function errorCallback(response) {
                //    });

            };
            $scope.createField = function () {

                appService.NavigateTo("createfield", { entityid: $scope.entity.Id, entityname: $scope.entity.Name });

            };
            $scope.readField = function () {

                var _field = null;

                if (arguments.length === 1)
                    _field = _.isEqual(arguments.length, 1) ? arguments[0] : null;

                if (_.isNull(_field))
                    appService.NavigateTo("updatefield", { fieldid: $scope.selectedFieldIds[0] });
                else
                    appService.NavigateTo("updatefield", { fieldid: _field.Id });

            };
            $scope.deleteField = function () {

                var _field = null;

                if (arguments.length === 1)
                    _field = _.isEqual(arguments.length, 1) ? arguments[0] : null;

                if ($scope.selectedFieldIds.length > 1) {
                    toastr.warning("Vous ne pouvez que supprimer un enregistrement à la fois!", "Attention");
                    return;
                }

                appService.GetData("/Field/DeleteField", { fieldId: $scope.selectedFieldIds[0] })
                    .then(function successCallback(response) {

                        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                            eduq.Notifications.Toastr.UpdateErrorNotification(response.data.message);
                            return;
                        }

                        eduq.Notifications.Toastr.UpdateSuccessNotification();
                        appService.RefreshCurrentState();

                    }, function errorCallback(response) {
                    });

            };
            $scope.getSelectedFieldRecords = function () {
                return $scope.selectedFieldIds
            };
            $scope.selectFieldRecord = function () {

                if (arguments.length === 0) return null;
                var _field = arguments[0] == null ? null : arguments[0];

                if (_field.Selected) {
                    //add id in array
                    $scope.selectedFieldIds.push(_field.Id);
                }
                else {
                    //remove id from array
                    var removedEntities = _.remove($scope.selectedFieldIds, function (x) {
                        return _.isEqual(x, _field.Id);
                    });
                }

            };
            $scope.getAttributs = function () {

                appService.GetData("/Field/GetFieldsByEntityId", { entityId: $scope.entity.Id })
                    .then(function successCallback(response) {

                        $scope.fields = response.data;

                    }, function errorCallback(response) {
                    });

            };
            $scope.getParentSideMenuItems = function () {

                appService.GetData("/SideMenu/GetParentSideMenuItems")
                    .then(function successCallback(response) {

                        $scope.parentSideMenuItems = response.data;

                    }, function errorCallback(response) {
                    });

            };
            $scope.processForm = function () {

                if (arguments.length === 0) return null;
                var form = arguments[0] == null ? null : arguments[0];

                $scope.formHasBeenSubmitted = true;

                if (!form.$valid) {
                    return null;
                }

                var keyValues = eduq.Functions.SplitObjectIntoArray($scope.entity);
                appService.PostForm("/Entity/UpdateEntity", { keyValues: keyValues })
                    .then(function successCallback(response) {

                        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                            eduq.Notifications.Toastr.UpdateErrorNotification(response.data.message);
                            return;
                        }

                        eduq.Notifications.Toastr.UpdateSuccessNotification();
                        var entityId = response.data.message;

                        return appService.NavigateTo("updateentity", { entityid: entityId });

                    }, function errorCallback(response) {
                    });

            };
            /*Functions - END*/

            this.init();
        }
    ]);
