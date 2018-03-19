if (typeof (eduq) == "undefined") {
    eduq = {};
};

eduq.Routes = {
    GetSideMenuRouteView: function() {
        return {
            templateUrl: "/Components/Sidemenu/Sidemenu.html",
            controller: "SideMenuController",
            resolve: {
                sideMenuItems: [
                    "$stateParams", "appService", function($stateParams, appService) {
                        return appService.GetData("/SideMenu/GetSideMenuItems");
                    }
                ]
            }
        };
    },
    GetToolbarRouteView: function() {
        return {
            templateUrl: "/Components/Toolbar/Toolbar.html",
            controller: "ToolbarController",
            resolve: {}
        };
    },
    GetBaseRouteView: function() {
        return {
            templateUrl: "/Components/Base/Base.html",
            controller: "BaseController",
            resolve: {}
        };
    },
    SetRoutes: function() {

        if (arguments.length === 0) return null;

        var entityName = arguments[0] == null ? null : arguments[0];
        var routeName = arguments[1] == null ? null : arguments[1];

        switch (entityName) {
        case "Home":
            return {
                name: "Home",
                url: "/Home",
                views: {
                    "": eduq.Routes.GetBaseRouteView(),
                    "toolbar@Home": eduq.Routes.GetToolbarRouteView(),
                    "sidemenu@Home": eduq.Routes.GetSideMenuRouteView(),
                    "body@Home": {
                        templateUrl: "/Components/Home/Home.html",
                        controller: "HomeController"
                    }
                }
                };
        case "Entity":
            switch (routeName) {
                case "Create":
                    return {
                        name: "createentity",
                        url: "/createentity",
                        views: {
                            "": eduq.Routes.GetBaseRouteView(),
                            "toolbar@createentity": eduq.Routes.GetToolbarRouteView(),
                            "sidemenu@createentity": eduq.Routes.GetSideMenuRouteView(),
                            "body@createentity": {
                                templateUrl: "/Components/Entity/CreateEntity.html",
                                controller: "CreateEntityController",
                                resolve: {
                                }
                            }
                        }
                    };
                case "Read":
                    return {
                        name: "readentities",
                        url: "/readentities",
                        views: {
                            "": eduq.Routes.GetBaseRouteView(),
                            "toolbar@readentities": eduq.Routes.GetToolbarRouteView(),
                            "sidemenu@readentities": eduq.Routes.GetSideMenuRouteView(),
                            "body@readentities": {
                                templateUrl: "/Components/Entity/ReadEntities.html",
                                controller: "ReadEntitiesController",
                                resolve: {
                                    entities: [
                                        "appService", function (appService) {
                                            return appService.GetData("/Entity/GetEntities");
                                        }
                                    ]
                                }
                            }
                        }
                    };
                case "Update":
                    return {
                        name: "updateentity",
                        url: "/updateentity?entityid",
                        views: {
                            "": eduq.Routes.GetBaseRouteView(),
                            "toolbar@updateentity": eduq.Routes.GetToolbarRouteView(),
                            "sidemenu@updateentity": eduq.Routes.GetSideMenuRouteView(),
                            "body@updateentity": {
                                templateUrl: "/Components/Entity/UpdateEntity.html",
                                controller: "UpdateEntityController",
                                resolve: {
                                    entity: [
                                        "$stateParams", "appService", function ($stateParams, appService) {
                                            return appService.GetData("/Entity/GetEntityById", { entityId: $stateParams.entityid });
                                        }
                                    ]
                                }
                            }
                        }
                    };
                default:
                    return null;
            }
        default:
        }
        return null;

    }
};

eduq.Loading = {
    Start: function (message) {
        if (message == null)
            return $("body").loading({
                message: "Opération en cours..."
            });
        else
            return $("body").loading({
                message: message
            });
    },
    Stop: function () {
        return $("body").loading("stop");
    }
};
