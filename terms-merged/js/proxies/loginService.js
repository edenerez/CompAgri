angular.module('TermsDataEntry')
 .factory('loginService', function ($http, $q, apiLocation) {

     return {
         getUser: function (user) {

             var deferred = $q.defer();
             var request = $http({
                 method: 'POST',
                 contentType: 'application/json',
                 url: 'http://localhost:53702/api/Login/Post',
                 data: user
             }).success(function (response) {
                 deferred.resolve(response);
                 window.localStorage['USERDATA'] = JSON.stringify(response);
                 return response;
             }).error(function (err, status) {
                 deferred.reject(err);
             });
             return deferred.promise;
         },
     }

 });
