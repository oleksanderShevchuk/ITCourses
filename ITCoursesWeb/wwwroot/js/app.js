var app = angular.module('ITCoursesApp', ['ngRoute']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/mainGrid', {
            templateUrl: 'views/mainGrid.html',
            controller: 'courseController',
        })

        .when('/', {
            templateUrl: '/courses',
            controller: 'courseController',
        })

        .otherwise({ redirectTo: '/' });
}]);