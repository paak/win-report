'use strict';
app.controller('airfreightController', ['$scope', 'airfreightService', function ($scope, airfreightService) {

    $scope.awbs = [];

    $scope.pagingInfo = {
        page: 1,
        itemsPerPage: 15,
        totalItems: 0,
        agentName: "",
        country: "",
        refNumber: "",
        carrier: "",
        origin: "",
        destination: "",
        fromDate: "",
        toDate: "",
        sort: "",
        sortDir: ""
    };

    $scope.search = function () {
        $scope.pagingInfo.page = 1;
        loadUsers();
    };

    $scope.pageChanged = function () {
        console.log('Page changed to: ' + $scope.currentPage);
        $scope.pagingInfo.page = $scope.currentPage;
        loadUsers();
    };
    function loadUsers() {
        airfreightService.getAwbs($scope.pagingInfo).then(function (results) {
            $scope.awbs = results.data;
            $scope.totalItems = results.data.totalCount;
        }, function (error) {
            //alert(error.data.message);
        });
    }
}]);