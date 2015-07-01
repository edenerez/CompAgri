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
                //templateUrl: './templates/uploadFile.html',
                templateUrl: './templates/login.html',
                controller: 'logoutController',
                //controllerAs: 'upload'
            })
        .otherwise({
            redirectTo: '/'
        });
    })
    .controller('AppController', AppController);

    function AppController() { }
}());