angular.module('TermsDataEntry').factory('treeServer', function ($http, $q, apiLocation) {
    return {
        getXMLFiles: function getXMLFiles() {
            return $http.get(apiLocation + "Tree").then(function (res) {
                return res.data;
            });
        },

        getTrees: function getTrees() {
            return this.getXMLFiles().then(function (list) {
                return list.map(function (item) {
                    return {
                        id: item.XmlFile_Id,
                        name: item.XmlFile_Name,
                        list: []
                    };
                });
            });
        },

        getTree: function getTree(id) {
            return $http.get(apiLocation + 'Tree/' + id).then(function (res) {
                return res.data;
            });
        }
    };
});