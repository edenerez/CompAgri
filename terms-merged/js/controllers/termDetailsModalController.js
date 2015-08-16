angular.module('TermsDataEntry')
.controller("TermDetailsModalCtrl", function ($scope, $modalInstance, termServer, nodeServer, treeService, term) {

    $scope.term = term;

    var propertiesNames = {
        DESCRIPTOR: "English Term",
        ES: "Hebrew Term",
        SO: "Source of Term",
        UF: "Used For(synonym)",
        DF: "Definition",
        DS: "Definition Source"
    };

    $scope.loadTermDetails = function loadTermDetails(termId) {
        termServer.getTermDetails(termId).then(function (details) {
            $scope.details = details;

            var termDictionary = {};

            termDictionary[details.term.Term_Id] = details.term;
            details.connectedTerms.forEach(function (item) {
                termDictionary[item.Term_Id] = item;
            });

            details.properties.forEach(function (item) {
                item.title = propertiesNames[item.Property_Key] || item.Property_Key;
            });

            details.connections.forEach(function (item) {
                item.term1 = termDictionary[item.Connection_Left_Term_Id];
                item.term2 = termDictionary[item.Connection_Right_Term_Id];
                item.otherTermId = item.Connection_Left_Term_Id == details.term.Term_Id ? item.Connection_Right_Term_Id : item.Connection_Left_Term_Id;
            });
        });
    };

    $scope.getPosibleParents = function getPosibleParents(toMatch) {
        $scope.writting = true;
        if ($scope.details) {
            return termServer.getMatchingTerms(toMatch, $scope.details.term.Term_XmlFile_id)
        }
    };

    $scope.addParent = function addParent() {
        if ($scope.newParent) {
            nodeServer.moveNode($scope.details.term.Term_XmlFile_id, $scope.details.term.Term_Id, 0, $scope.newParent.Term_Id).then(function (response) {
                //var leftParent = treeService.getNode($scope.newParent.Term_XmlFile_id + ':' + $scope.newParent.Term_Id, 'left');
                //var rightParent = treeService.getNode($scope.newParent.Term_XmlFile_id + ':' + $scope.newParent.Term_Id, 'right');

                //var leftChild = treeService.getNode($scope.details.term.Term_XmlFile_id + ':' + $scope.details.term.Term_Id, 'left');
                //var rightChild = treeService.getNode($scope.details.term.Term_XmlFile_id + ':' + $scope.details.term.Term_Id, 'right');

                //leftParent.scope().$nodeScope.$modelValue.items.push(leftChild.scope().$nodeScope.$modelValue);
                //rightParent.scope().$nodeScope.$modelValue.items.push(rightChild.scope().$nodeScope.$modelValue);
                $scope.details.parents.push($scope.newParent);
            }).then(function () {
                $scope.writting = false;
                $scope.newParent = undefined;
                $scope.isAddingParent = false;
            });
        } else {
            $scope.writting = false;
            $scope.newParent = undefined;
            $scope.isAddingParent = false;
        }
    }

    $scope.loadTermDetails(term.id);

    $scope.close = function () {
        $modalInstance.dismiss("close");
    };
})
