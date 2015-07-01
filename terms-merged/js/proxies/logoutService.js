angular.module('TermsDataEntry')
 .factory('logoutService', function ($http, $q, apiLocation, loginService) {

     return {
         PostToken: function () {
             var deferredObject = $q.defer();

             var USERDATA = JSON.parse(window.localStorage['USERDATA'] || '{}');
             if (USERDATA) {
                 var Token = USERDATA.Token;
             }
             var deferred = $q.defer();
             var request = $http({
                 method: 'Delete',
                 headers: { 'CompagriUserToken': Token },
                 url: 'http://localhost:53702/api/Login/Delete',
             }).success(function () {
                 window.localStorage['USERDATA'] = null;
                 deferred.resolve(response);

             }).error(function (err, status) {
                 deferred.reject(err);
             });
             return request;
         }
     }
 });
