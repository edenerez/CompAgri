(function (angular) {
    angular.module('TermsDataEntry').controller("loginController", loginController);

    function loginController($scope, $http, $location, loginService) {

        $scope.Login = function () {

            $scope.Message = "";
            $scope.ErrorShow = false;

            var LoginData = {
                Email: $scope.Username,
                Username: $scope.Username,
                Password: $scope.Password
            };

            var loginPromise = loginService.login(LoginData);

            loginPromise.then(function () {
                $location.path('/Main')
            },
              function () {
                  $scope.Message = "Username or Password is Incorrect.";
                  $scope.ErrorShow = true;
              });

        };
    }
}(this.angular))
