﻿app.controller('courseController', function ($scope, $http) {
    var baseUrl = '/api/course';
    $scope.courses = [];
    $scope.allCourses = [];
    $scope.selectedCourse = null;
    $scope.newCourse = { name: '', description: '', teacherName: '', pathToImg: '' };
    $scope.showAddCourseForm = false;
    $scope.showEditCourseForm = false; 
    $scope.currentPage = 1;
    $scope.pageSize = 2;
    $scope.totalPages = 1; 
    $scope.addingNewCourse = false;

    // Fetch all courses and broadcast updates
    $scope.getAllCourses = function () {
        $http.get(baseUrl)
            .then(function (response) {
                $scope.allCourses = response.data; 
                $scope.totalPages = Math.ceil($scope.allCourses.length / $scope.pageSize);
                $scope.updateCoursesForPage();
            })
            .catch(function (error) {
                console.error('Error fetching courses:', error);
            });
    };

    // Update courses for the current page
    $scope.updateCoursesForPage = function () {
        const startIndex = ($scope.currentPage - 1) * $scope.pageSize;
        const endIndex = startIndex + $scope.pageSize;
        $scope.courses = $scope.allCourses.slice(startIndex, endIndex);
    };

    // Generate page numbers for pagination
    $scope.getPageNumbers = function () {
        const maxPagesToShow = 5; 
        const pages = [];

        if ($scope.totalPages > maxPagesToShow && $scope.currentPage > Math.ceil(maxPagesToShow / 2)) {
            pages.push(1, '...');
        }

        const startPage = Math.max(1, $scope.currentPage - 2);
        const endPage = Math.min($scope.totalPages, $scope.currentPage + 2);
        for (let i = startPage; i <= endPage; i++) {
            pages.push(i);
        }

        if ($scope.totalPages > maxPagesToShow && $scope.currentPage < $scope.totalPages - Math.floor(maxPagesToShow / 2)) {
            pages.push('...', $scope.totalPages);
        }

        return pages;
    };

    // Go to the next page
    $scope.nextPage = function () {
        if ($scope.currentPage * $scope.pageSize < $scope.allCourses.length) {
            $scope.currentPage++;
            $scope.updatePaginatedCourses();
        }
    };

    // Go to the previous page
    $scope.prevPage = function () {
        if ($scope.currentPage > 1) {
            $scope.currentPage--;
            $scope.updatePaginatedCourses();
        }
    };

    // Set a specific page
    $scope.setPage = function (page) {
        if (page === '...') return; 
        $scope.currentPage = page;
        $scope.updateCoursesForPage();
    };

    // Select a course and broadcast grid change
    $scope.selectCourse = function (course) {
        course.isEditing = false;
        $scope.addingNewCourse = false;
        $scope.selectedCourse = course;
    };

    $scope.editCourse = function (course) {
        course.isEditing = true;
        course.original = angular.copy(course);
    };

    // Cancel the editing and revert changes
    $scope.cancelEdit = function (course) {
        angular.copy(course.original, course);
        course.isEditing = false; 
        delete course.original; 
    };

    // Save course changes
    $scope.saveCourse = function (course) {
        $http.put(baseUrl + '/' + course.id, course)
            .then(function (response) {
                const index = $scope.courses.findIndex(c => c.id === response.data.id);
                if (index !== -1) {
                    $scope.courses[index] = response.data;
                }
                course.isEditing = false; 
            })
            .catch(function (error) {
                console.error('Error saving course:', error);
            });
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

    // Toggle the "Add New Course" form
    $scope.toggleAddNewCourse = function () {
        $scope.addingNewCourse = true;
        $scope.newCourse = { name: '', description: '', teacherName: '', pathToImg: '' };
    };

    // Cancel the "Add New Course" form
    $scope.cancelNewCourse = function () {
        $scope.addingNewCourse = false;
        $scope.newCourse = {};
    };

    // Create a new course
    $scope.createNewCourse = function () {
        if (!$scope.newCourse.name || !$scope.newCourse.description || !$scope.newCourse.teacherName) {
            alert('Please fill out all fields before creating a course.');
            return;
        }
        $http.post(baseUrl, $scope.newCourse).then(function (response) {
            $scope.courses.push(response.data); 
            $scope.addingNewCourse = false; 
            $scope.newCourse = {};
        }).catch(function (error) {
            console.error('Error creating new course:', error);
            alert('An error occurred while creating the course.');
        });
    };

    // Initialize by fetching courses
    $scope.getAllCourses();
});
