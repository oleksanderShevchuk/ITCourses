app.controller('detailsController', function ($scope, $http, $rootScope) {
    $scope.currentView = '';
    $scope.teacherInfo = {};
    $scope.promoCodes = []; 

    // Function to fetch teacher details
    $scope.loadTeacherDetails = function () {
        const teacherEmail = $rootScope.teacherEmail;
        if (!teacherEmail) {
            console.error('Teacher email is missing!');
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
