angular.module('TermsDataEntry').factory('termDetailsServer', function ($http, $q, apiLocation) {
    return {
        getTermDetails: function getTermDetails(id) {
            return $http.get(apiLocation + 'TermDetails/' + id).then(function (res) {
                return res.data;
            });
        }
    };
});