app.controller('detailsController', function ($scope, $http, $rootScope) {
    $scope.currentView = '';
    $scope.personInfo = {};
    $scope.promoCodes = [];
    $scope.showAddPromoCodeForm = false;
    $scope.actions = false;
    $scope.generateInfo = {};

    // Function to fetch person details
    $scope.loadPersonDetails = function () {
        const courseId = $rootScope.courseId;
        if (!courseId) {
            return;
        }

        $http.get('/api/person/get-all-by-course-id/' + courseId)
            .then(function (response) {
                $scope.persons = response.data;
            })
            .catch(function (error) {
                console.error('Error fetching person details:', error);
                alert('Could not load person details.');
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
                $scope.loadPromoCodes();
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

    // Add edit functionality for person
    $scope.editPerson = function (person) {
        person.isEditing = true;
    };

    $scope.savePerson = function (person) {
        $http.put('/api/person/' + person.id, person)
            .then(function () {
                person.isEditing = false;
            })
            .catch(function (error) {
                console.error('Error saving person details:', error);
            });
    };

    $scope.cancelEditPerson = function (person) {
        angular.copy($scope.personInfo.original, $scope.personInfo);
        person.isEditing = false;
        delete $scope.personInfo.original;
    };

    // Delete Person from course
    $scope.deletePersonFromCourse = function (personId) {
        if (!confirm('Are you sure you want to delete person from course?')) {
            return;
        }
        const courseId = $rootScope.courseId;

        $http.delete('/api/person/' + personId + '/' + courseId)
            .then(function () {
                $scope.loadPersonDetails();
            })
            .catch(function (error) {
                console.error('Error deleting person from course:', error);
            });
    };

    // Load generatePromoCodes
    $scope.loadGeneratePromoCodes = function () {
        $scope.actions = true;
    };

    // Function to generate promo codes
    $scope.generatePromoCodes = function (generateInfo) {
        generateInfo.courseId = $rootScope.courseId;
        if (!generateInfo.courseId) {
            console.error('Course Id is missing!');
            return;
        }
        $http.post('/api/promocode/generate', generateInfo)
            .then(function () {
                $scope.actions = false;
                $scope.currentView = '';
            })
            .catch(function (error) {
                console.error('Error fetching promo codes:', error);
            });
    };

    // Function to switch views
    $scope.switchView = function (view) {
        $scope.currentView = view;

        if (view === 'person') {
            $scope.loadPersonDetails();
        } else if (view === 'promocodes') {
            $scope.loadPromoCodes();
        } else if (view === 'generatePromoCodes') {
            $scope.loadGeneratePromoCodes();
        }
    };

    // Listen for reset event
    $scope.$on('resetView', function () {
        $scope.actions = false;
        $scope.currentView = '';
    });

    // Listen for goToActions event
    $scope.$on('goToActions', function () {
        $scope.actions = true;
    });

    // Initialize with the default view
    $scope.switchView($scope.currentView);
});
