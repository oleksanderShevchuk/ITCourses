angular.module('ITCoursesApp')
    .directive('editCourse', function () {
        return {
            restrict: 'E',
            scope: {},
            templateUrl: 'views/editCourse.html',
            link: function (scope) {
                scope.$on('gridChanged', function (event, data) {
                    if (data.grid === 'editCourse') {
                        if (data.course) {
                            scope.editingCourse = data.course;
                            scope.showEditCourseForm = true;
                        }
                        else if (data.grid === 'editCourseCancelled') {
                            scope.showEditCourseForm = false;
                        }
                    }
                });

                scope.$on('editCourseCancelled', function () {
                    scope.showEditCourseForm = false;
                });
            }
        };
    });
