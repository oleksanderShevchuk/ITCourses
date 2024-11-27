﻿app.controller('detailsController', function ($scope, $http, $rootScope) {
    $scope.currentView = '';
    $scope.teacherInfo = {};
    $scope.promoCodes = []; 
    $scope.newPromoCode = { code: '', discount: 0 };
    $scope.showAddPromoCodeForm = false; 

    // Function to fetch teacher details
    $scope.loadTeacherDetails = function () {
        const teacherEmail = $rootScope.teacherEmail;
        if (!teacherEmail) {
            return;
        }

        $http.get('/api/person/by-email/' + teacherEmail)
            .then(function (response) {
                $scope.teacherInfo = response.data; 
            })
            .catch(function (error) {
                console.error('Error fetching teacher details:', error);
                alert('Could not load teacher details.');
            });
    };

    // Function to fetch promo codes
    $scope.loadPromoCodes = function () {
        const courseId = $rootScope.courseId;
        if (!courseId) {
            console.error('Course Id is missing!');
            return;
        }
        $http.get('/api/promoCode/by-courseId/' + courseId)
            .then(function (response) {
                $scope.promoCodes = response.data;
            })
            .catch(function (error) {
                console.error('Error fetching promo codes:', error);
            });
    };

    // Add promo code
    $scope.addPromoCode = function () {
        if (!confirm('Generate new promo code?')) {
            return;
        }
        $http.post('/api/promocode/' + $rootScope.courseId)
            .then(function (response) {
                $scope.promoCodes.push(response.data);
                $scope.showAddPromoCodeForm = false;
            })
            .catch(function (error) {
                console.error('Error adding promo code:', error);
            });
    };

    // Show the form to add a new promo code
    $scope.toggleAddPromoCodeForm = function () {
        $scope.showAddPromoCodeForm = !$scope.showAddPromoCodeForm;
    };

    // Edit promo code
    $scope.editPromoCode = function (promoCode) {
        promoCode.isEditing = true;
        promoCode.original = angular.copy(promoCode);
    };

    $scope.savePromoCode = function (promoCode) {
        $http.put('/api/promocode/' + promoCode.id, promoCode)
            .then(function () {
                promoCode.isEditing = false;
                delete promoCode.original;
            })
            .catch(function (error) {
                console.error('Error saving promo code:', error);
            });
    };

    $scope.cancelEditPromoCode = function (promoCode) {
        angular.copy(promoCode.original, promoCode);
        promoCode.isEditing = false;
        delete promoCode.original;
    };

    // Delete promo code
    $scope.deletePromoCode = function (promoCodeId) {
        if (!confirm('Are you sure you want to delete this promo code?')) {
            return;
        }

        $http.delete('/api/promocode/' + promoCodeId)
            .then(function () {
                $scope.promoCodes = $scope.promoCodes.filter(pc => pc.id !== promoCodeId);
            })
            .catch(function (error) {
                console.error('Error deleting promo code:', error);
            });
    };

    // Add edit functionality for teacher
    $scope.editTeacher = function () {
        $scope.teacherInfo.isEditing = true;
        $scope.teacherInfo.original = angular.copy($scope.teacherInfo);
    };

    $scope.saveTeacher = function () {
        $http.put('/api/person/' + $scope.teacherInfo.id, $scope.teacherInfo)
            .then(function () {
                $scope.teacherInfo.isEditing = false;
                delete $scope.teacherInfo.original;
            })
            .catch(function (error) {
                console.error('Error saving teacher details:', error);
            });
    };

    $scope.cancelEditTeacher = function () {
        angular.copy($scope.teacherInfo.original, $scope.teacherInfo);
        $scope.teacherInfo.isEditing = false;
        delete $scope.teacherInfo.original;
    };

    // Delete Teacher from course
    $scope.deleteTeacherFromCourse = function (teacherId) {
        if (!confirm('Are you sure you want to delete teacher from course?')) {
            return;
        }
        const courseId = $rootScope.courseId;

        $http.delete('/api/person/' + teacherId + '/' + courseId)
            .then(function () {
                $scope.loadTeacherDetails();
            })
            .catch(function (error) {
                console.error('Error deleting person from course:', error);
            });
    };

    // Function to switch views
    $scope.switchView = function (view) {
        $scope.currentView = view;

        if (view === 'teacher') {
            $scope.loadTeacherDetails();
        } else if (view === 'promocodes') {
            $scope.loadPromoCodes();
        }
    };

    // Initialize with the default view
    $scope.switchView($scope.currentView);
});
