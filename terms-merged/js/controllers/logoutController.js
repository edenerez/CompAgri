(function () {
    angular.module('TermsDataEntry')
     .controller("logoutController", logoutController);
    function logoutController($scope, $http, $location, logoutService, loginService) {

        logout();
        function logout() {
            var promisePost = logoutService.PostToken();
            $scope.logout = true;
            promisePost.then(function () {
                //$location.path('/login')
                $location.path('/')
                
            },
              function (errorPl) {
                  $scope.error = 'failure loading Employee', errorPl;
                  
              });
        }
        //};
    }
}())
