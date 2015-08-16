angular.module('TermsDataEntry').factory('termServer', function ($http, $q, apiLocation) {
    return {
        getTermDetails: function getTermDetails(id) {
            return $http.get(apiLocation + 'Term/Details/' + id).then(function (res) {
                return res.data;
            });
        },
        getMatchingTerms: function getMatchingTerms(toMatch, treeId) {
            return $http.get(apiLocation + 'Term/GetMatchingTerms/', {
                params: {
                    toMatch: toMatch,
                    treeId: treeId
                }
            }).then(function (res) {
                return res.data;
            });
        }

    };
});