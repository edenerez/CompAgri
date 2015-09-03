angular.module('TermsDataEntry')
 .factory('logServer', function ($http, $q, apiLocation) {

     return {
         downloadLog: function (user) {
             return $http.get(apiLocation + 'Log', user).then(function (token) {
                 location.href=apiLocation + 'Log/?id=' + token.data;
             });
         }
     }

 });
