angular.module('TermsDataEntry').factory('connectionServer', function ($http, $q, apiLocation) {
    return {
        getConnectionsPosibleValues: function getConnectionsPosibleValues() {
            return $http.get(apiLocation + "Connection/PosibleValues").then(function (res) {
                return res.data;
            });
        },

        addConnection: function addConnection(data) {
            return $http.post(apiLocation + "Connection", data).then(function (res) {
                return res.data;
            });
        },

        deleteConnection: function deleteConnection(id) {
            return $http.delete(apiLocation + "Connection/" + id).then(function (res) {
                return res.data;
            });
        },

        getConnectionsForTerms: function getConnectionsForTerms(termIds) {
            return $http.get(apiLocation + "Connection/ForTerms?termIds=" + termIds.join(',')).then(function (res) {
                return res.data;
            });
        }
    };
});