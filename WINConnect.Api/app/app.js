
var app = angular.module('app', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap']);

app.config(function ($routeProvider) {

    //$routeProvider.when("/home", {
    //    controller: "homeController",
    //    templateUrl: "app/views/home.html"
    //});

    //$routeProvider.when("/login", {
    //    controller: "loginController",
    //    templateUrl: "app/views/login.html"
    //});

    //$routeProvider.when("/signup", {
    //    controller: "signupController",
    //    templateUrl: "app/views/signup.html"
    //});

    $routeProvider.when("/airfreight", {
        controller: "airfreightController",
        templateUrl: "app/views/airfreight.html"
    });

    //$routeProvider.when("/refresh", {
    //    controller: "refreshController",
    //    templateUrl: "app/views/refresh.html"
    //});

    //$routeProvider.when("/tokens", {
    //    controller: "tokensManagerController",
    //    templateUrl: "app/views/tokens.html"
    //});

    //$routeProvider.when("/associate", {
    //    controller: "associateController",
    //    templateUrl: "app/views/associate.html"
    //});

    $routeProvider.otherwise({ redirectTo: "/airfreight" });

});

var serviceBase = 'http://localhost:49374/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});


