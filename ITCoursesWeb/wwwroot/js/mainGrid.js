app.directive('mainGrid', function () {
    return {
        restrict: 'E',
        scope: {
            courses: '=',         
            selectedCourse: '=',  
            onSaveCourse: '&',  
        },
        templateUrl: 'views/mainGrid.html',
        link: function (scope) {
            scope.selectCourse = function (course) {
                scope.courses.forEach(c => c.isEditing = false);
                scope.selectedCourse = course;
            };

            scope.editCourse = function (course) {
                course.isEditing = true;
            };

            scope.saveCourse = function (course) {
                course.isEditing = false;
                scope.onSaveCourse({ course: course });
            };

            scope.cancelEdit = function (course) {
                course.isEditing = false;
            };
        }
    };
});
