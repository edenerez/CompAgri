(function () {
    angular.module('TermsDataEntry', ['ui.tree', 'ui.bootstrap', 'ng-context-menu', 'ngRoute'])
    .constant('apiLocation', "http://localhost:53702/api/")
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: './templates/login.html',
                controller: 'loginController',
                controllerAs: 'login'
            })
      .when('/Main', {
          templateUrl: './templates/main.html',
          controller: 'MainController',
          controllerAs: 'main'
      })
      .when('/uploadXml', {
          templateUrl: './templates/uploadFile.html',
          controller: 'UploadFileController',
          controllerAs: 'upload'
      })
            .when('/logout', {
                templateUrl: './templates/login.html',
                controller: 'logoutController'
            })
        .otherwise({
            redirectTo: '/'
        });

    })
    .run(function ($http, loginService) {
        $http.defaults.headers.common.CompagriUserToken = function () {
            return loginService.getUser() && loginService.getUser().Token;
        };
    })
    .controller('AppController', AppController);

    function AppController($scope, loginService) {

        $scope.isLogedIn = function () {
            return loginService.getUser();
        };

        $scope.logout = function logout() {
            loginService.logout().then(function () {
                $location.path('/')
            }, function () {
                $scope.error = 'Logout failure';
            });
        }
    }
}());