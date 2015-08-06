(function () {
    angular.module('TermsDataEntry', ['ui.tree', 'ui.bootstrap', 'ng-context-menu', 'ngRoute'])
    .constant('apiLocation', "http://localhost:53702/api/")
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: './templates/login.html',
                controller: 'loginController',
                controllerAs: 'login'
            }).when('/login', {
                redirectTo: '/'
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

    function AppController($scope, $location, loginService) {

        $scope.isLogedIn = function () {
            return loginService.getUser();
        };

        $scope.logout = function logout() {
            loginService.logout().then(function () {
                $location.path('/login');
            }, function () {
                $location.path('/login')
                $scope.error = 'Logout failure';
            });
        }

        $scope.$on('$routeChangeStart', function (event, next) {
            if ($scope.isLogedIn()) {
                if (next.$$route && next.$$route.controllerAs == 'login') {
                    event.preventDefault();
                }
            } else {
                if ((!next.$$route || next.$$route.controllerAs != 'login') && next.redirectTo != '/') {
                    event.preventDefault();
                    $location.path('/login');
                }
            }
        });
    }
}());