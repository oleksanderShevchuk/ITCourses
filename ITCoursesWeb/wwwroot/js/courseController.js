app.controller('courseController', function ($scope, $http) {
    var baseUrl = '/api/course';
    $scope.courses = [];
    $scope.selectedCourse = null;
    $scope.currentGrid = 'mainGrid';

    // Fetch all courses and broadcast updates
    $scope.getAllCourses = function () {
        $http.get(baseUrl)
            .then(function (response) {
                $scope.courses = response.data;
                $scope.$broadcast('coursesUpdated', $scope.courses);
            })
            .catch(function (error) {
                console.error('Error fetching courses:', error);
            });
    };

    // Select a course and broadcast grid change
    $scope.selectCourse = function (course) {
        $scope.selectedCourse = course;
        $scope.currentGrid = 'detailGrid';
        $scope.$broadcast('gridChanged', { grid: 'detailGrid', course: course });
        console.log('selectedCourse:', $scope.selectedCourse);
        console.log('currentGrid:', $scope.currentGrid);
    };

    // Initialize by fetching courses
    $scope.getAllCourses();
});
