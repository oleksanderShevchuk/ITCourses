app.directive('mainGrid', function () {
    return {
        restrict: 'E',
        scope: {
            onSelectCourse: '&' 
        },
        templateUrl: 'views/mainGrid.html',
        link: function (scope) {
            scope.$on('coursesUpdated', function (event, courses) {
                scope.courses = courses;
            });

            scope.selectCourse = function (course) {
                scope.onSelectCourse({ course: course });
            };
        }
    };
});
