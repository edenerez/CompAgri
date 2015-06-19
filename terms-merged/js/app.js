(function () {
    angular.module('TermsDataEntry', ['ui.tree', 'ui.bootstrap', 'ng-context-menu', 'ngRoute'])
    .constant('apiLocation', "http://localhost:53702/api/")
    .config(function ($routeProvider) {
        $routeProvider
      .when('/', {
          templateUrl: './templates/main.html',
          controller: 'MainController',
          controllerAs: 'main'
      })
      .when('/uploadXml', {
          templateUrl: './templates/uploadFile.html',
          controller: 'UploadFileController',
          controllerAs: 'upload'
      });
    })
    .controller('AppController', AppController);

    function AppController() { }
}());