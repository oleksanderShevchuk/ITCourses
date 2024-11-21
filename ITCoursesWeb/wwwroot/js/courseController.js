app.controller('courseController', function ($scope, $http) {
    var baseUrl = '/api/course';
    $scope.courses = [];
    $scope.selectedCourse = null;
    $scope.editingCourse  = null;
    $scope.currentGrid = 'mainGrid';
    $scope.newCourse = { name: '', description: '', teacherName: '', pathToImg: '' };
    $scope.showAddCourseForm = false;
    $scope.showEditCourseForm = false; 

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
        $scope.selectedCourse = angular.copy(course);
        console.log('$scope.selectedCourse', $scope.selectedCourse);
        $scope.currentGrid = 'detailGrid';
        $scope.$broadcast('gridChanged', { grid: 'detailGrid', course: course });
    };

    $scope.$on('gridChanged', function (event, data) {
        if (data.grid === 'detailGrid') {
            $scope.selectedCourse = data.course;
            $scope.showEditCourseForm = false;
        } else if (data.grid === 'editCourse') {
            $scope.editingCourse = angular.copy(data.course);
            $scope.showEditCourseForm = true;
        } else if (data.grid === 'editCourseCancelled') {
            $scope.showEditCourseForm = false;
            $scope.editingCourse = null;
        }
    });

    // Show and broadcast the edit form
    $scope.editCourse = function (course) {
        $scope.editingCourse = angular.copy(course);
        console.log('$scope.editingCourse', $scope.editingCourse);
        $scope.$broadcast('gridChanged', { grid: 'editCourse', course: course });
    };

    // Submit a new course
    $scope.submitCourse = function () {
        $http.post(baseUrl, $scope.newCourse)
            .then(function (response) {
                $scope.courses.push(response.data);
                $scope.showAddCourseForm = false; 
                $scope.newCourse = { name: '', description: '', teacherName: '', pathToImg: '' }; 
            })
            .catch(function (error) {
                console.error('Error adding course:', error);
            });
    };

    // Cancel the add course form and reset the form fields
    $scope.cancelAddCourse = function () {
        $scope.showAddCourseForm = false;
        $scope.newCourse = { name: '', description: '', teacherName: '', pathToImg: '' }; 
    };

    // Submit the edited course
    $scope.submitEditCourse = function () {
        $http.put(baseUrl + '/' + $scope.editingCourse.id, $scope.editingCourse) 
            .then(function (response) {
                const index = $scope.courses.findIndex(course => course.id === response.data.id);
                if (index !== -1) {
                    $scope.courses[index] = response.data;
                }
                $scope.showEditCourseForm = false;
                $scope.editingCourse = null;
                $scope.selectedCourse = response.data;
                $scope.$broadcast('gridChanged', { grid: 'editCourseCancelled' });
            })
            .catch(function (error) {
                console.error('Error updating course:', error);
            });
    };

    // Cancel the Edit form and reset selected course
    $scope.cancelEditCourse = function () {
        $scope.showEditCourseForm = false;
        $scope.editingCourse = null;
        $scope.$broadcast('gridChanged', { grid: 'editCourseCancelled' });
    };

    // Confirm delete action
    $scope.confirmDelete = function (course) {
        if (confirm(`Are you sure you want to delete the course: "${course.name}"?`)) {
            $scope.deleteCourse(course.id);
        }
    };

    // Delete course
    $scope.deleteCourse = function (courseId) {
        $http.delete(baseUrl + '/' + courseId)
            .then(function (response) {
                $scope.courses = $scope.courses.filter(course => course.id !== courseId);

                if ($scope.selectedCourse && $scope.selectedCourse.id === courseId) {
                    $scope.selectedCourse = null;
                    $scope.currentGrid = 'mainGrid';
                }

                alert('Course deleted successfully.');
                $scope.getAllCourses();
            })
            .catch(function (error) {
                console.error('Error deleting course:', error);
                alert('An error occurred while deleting the course.');
            });
    };

    // Initialize by fetching courses
    $scope.getAllCourses();
});
