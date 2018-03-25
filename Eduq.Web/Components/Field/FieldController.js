
angular.module("eduqApp")
    .controller("CreateFieldController",
    [
        "$rootScope", "$scope", "appService", "entity", "fieldTypes", "entities",
        function ($rootScope, $scope, appService, entity, fieldTypes, entities) {

            /*Variables - START*/
            $scope.field = {};
            $scope.entity = entity;
            $scope.fieldTypes = fieldTypes.data;
            $scope.entities = entities.data;
            $scope.formHasBeenSubmitted = false;
            $scope.selectedFieldType;
            /*Variable - END*/

            /*Functions - START*/
            this.init = function () {

                $scope.field["EntityId"] = $scope.entity.Id;
                $scope.field["EntityName"] = $scope.entity.Name;

            };
            $scope.OnChange_FieldType = function () {

                if (arguments.length === 0) return null;
                var fieldTypeId = arguments[0] == null ? null : arguments[0];

                $scope.selectedFieldType = _.find($scope.fieldTypes, function (x) { return _.isEqual(x.Id, _.toInteger(fieldTypeId)); });
                $scope.field["FieldTypeName"] = $scope.selectedFieldType.SqlDataTypeName;
            };
            $scope.processForm = function () {

                if (arguments.length === 0) return null;
                var form = arguments[0] == null ? null : arguments[0];

                $scope.formHasBeenSubmitted = true;

                if (!form.$valid) {
                    return null;
                }

                var keyValues = eduq.Functions.SplitObjectIntoArray($scope.field);
                appService.PostForm("/Field/CreateField", { keyValues: keyValues })
                    .then(function successCallback(response) {

                        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                            eduq.Notifications.Toastr.CreateErrorNotification(response.message);
                            return;
                        }

                        eduq.Notifications.Toastr.CreateSuccessNotification();
                        var fieldId = response.data.message;
                        return appService.NavigateTo("updateentity", { entityid: $scope.entity.Id });

                    }, function errorCallback(response) {
                    });

            };
            /*Functions - END*/

            this.init();

        }
    ])
    .controller("ReadFieldsController",
    [
        "$rootScope", "$scope", "appService", "fields",
        function ($rootScope, $scope, appService, fields) {

            /*Variables - START*/
            $scope.fields = fields.data;
            $scope.fieldTypes = fieldTypes.data;
            $scope.selectedFieldIds = [];
            /*Variable - END*/

            /*Functions - START*/
            this.init = function () {
                //$("#table").DataTable({
                //    "serverSide": true,
                //    "processing": true,
                //    "fixedHeader": true,
                //    "ajax": {
                //        "url": "/Field/GetEntities",
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
                return $scope.selectedFieldIds
            };
            $scope.selectRecord = function () {

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
            $scope.create = function () {

                appService.NavigateTo("createfield");

            };
            $scope.read = function () {

                var _field = null;

                if (arguments.length === 1)
                    _field = _.isEqual(arguments.length, 1) ? arguments[0] : null;

                if (_.isNull(_field))
                    appService.NavigateTo("updatefield", { fieldid: $scope.selectedFieldIds[0] });
                else
                    appService.NavigateTo("updatefield", { fieldid: _field.Id });

            };
            /*Functions - END*/

            this.init();

        }
    ])
    .controller("UpdateFieldController",
    [
        "$rootScope", "$scope", "appService", "field", "fieldTypes",
        function ($rootScope, $scope, appService, field, fieldTypes) {

            /*Variables - START*/
            $scope.field = field.data;
            $scope.fieldTypes = fieldTypes.data;
            /*Variable - END*/

            /*Functions - START*/
            this.init = function () {

            };
            $scope.create = function () {

                appService.NavigateTo("createfield");

            };
            $scope.processForm = function () {

                var keyValues = tca.Functions.SplitObjectIntoArray($scope.field);

                appService.PostForm("/Field/UpdateField", { keyValues: keyValues })
                    .then(function successCallback(response) {

                        if (_.isNull(response) || _.isNull(response.data) || !_.isEqual(response.data.type, "success")) {
                            alert("error");
                            return;
                        }

                        var fieldId = response.data.message;
                        return appService.RefreshCurrentState();

                    }, function errorCallback(response) {
                    });

            };
            /*Functions - END*/

            this.init();
        }
    ]);
