'use strict';
app.factory('airfreightService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var airfreightServiceFactory = {};

    var _getAwbs = function (pagingInfo) {

        return $http.get(serviceBase + 'api/airfreight', { params: pagingInfo })
            .then(function (results) {
                return results;
            });
    };

    airfreightServiceFactory.getAwbs = _getAwbs;

    return airfreightServiceFactory;

}]);