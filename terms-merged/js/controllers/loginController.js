(function () {
    angular.module('TermsDataEntry')
     .controller("loginController", loginController);
     
    function loginController($scope, $http, $location, loginService) {

        $scope.Login = function () {

            $scope.Message = "";
            $scope.ErrorShow = false;
            
            var LoginData = {
                Email: $scope.Username,
                Password: $scope.Password
            };

            
            var promisePost = loginService.getUser(LoginData);
            
            promisePost.then(function (pl) {
                $location.path('/uploadXml')
            },
              function (errorPl) {
                  $scope.Message = "Username or Password is InCorrect.";
                  $scope.ErrorShow = true;
              });

        };
    }
}())
