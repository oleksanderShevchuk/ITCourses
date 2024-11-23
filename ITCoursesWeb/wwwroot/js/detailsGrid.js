angular.module('ITCoursesApp')
    .directive('detailsGrid', function () {
        return {
            restrict: 'E',
            scope: {
                onHide: '&'
            },
            templateUrl: 'views/detailsGrid.html',
            link: function (scope) {
                scope.$on('gridChanged', function (event, data) {
                    if (data.grid === 'detailsGrid') {
                        scope.course = data.course;
                    } else if (data.grid === 'mainGrid') {
                        scope.course = null;
                    }
                });
            }
        };
    });
