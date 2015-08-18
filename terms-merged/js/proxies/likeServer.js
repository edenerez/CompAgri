angular.module('TermsDataEntry').factory('likeServer', function ($http, $q, apiLocation) {
    return {
        like: function like(connection) {
            return $http.get(apiLocation + "Like/Like/" + connection).then(function (res) {
                return res.data;
            });
        },

        unlike: function unlike(connection) {
            return $http.get(apiLocation + "Like/Unlike/" + connection).then(function (res) {
                return res.data;
            });
        }
    };
});