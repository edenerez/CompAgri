angular.module('TermsDataEntry').factory('nodeServer', function ($http, $q, apiLocation) {
    return {
        addNode: function (xmlFileId, name, parentId, data) {
            return $http.post(apiLocation + 'Node/AddNode', data, {
                params: {
                    xmlFileId: xmlFileId,
                    name: name,
                    parentId: parentId || 0
                }
            });
        },

        deleteNode: function (xmlFileId, nodeId, parentId) {
            return $http.post(apiLocation + 'Node/DeleteNode', {
                params: {
                    xmlFileId: xmlFileId,
                    nodeId: nodeId,
                    parentId: parentId
                }
            });
        },

        moveNode: function (xmlFileId, nodeId, oldParentId, newParentid) {
            return $http.get(apiLocation + 'Node/MoveNode', {
                params: {
                    xmlFileId: xmlFileId,
                    nodeId: nodeId,
                    oldParentId: oldParentId,
                    newParentid: newParentid
                }
            });
        },
    };
});