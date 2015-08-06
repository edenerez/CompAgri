angular.module('TermsDataEntry')
 .factory('loginService', function ($http, $q, apiLocation) {

     var HasSession = window.Storage,
         SESSION_KEY = 'CompagriUser';

     return {
         login: function (user) {
             return $http.post(apiLocation + 'Login', user).then(this.saveUser);
         },

         saveUser: function saveUser(response) {
             this.user = response.data;
             if (HasSession) {
                 window.sessionStorage.setItem(SESSION_KEY, JSON.stringify(this.user));
             }
             return this.user;
         },

         getUser: function getUser() {
             if (!this.user && HasSession) {
                 if (window.sessionStorage.getItem(SESSION_KEY)) {
                     this.user = JSON.parse(window.sessionStorage.getItem(SESSION_KEY));
                 }
             }

             return this.user;
         },

         logout: function () {
             if (this.getUser()) {
                 return $http.delete(apiLocation + 'Login').then(this.cleanUserData.bind(this));
             } else {
                 return $q.defer().resolve().promise;
             }
         },

         cleanUserData: function cleanUserData() {
             window.sessionStorage.removeItem(SESSION_KEY);
             delete this.user;
         }
     }

 });
