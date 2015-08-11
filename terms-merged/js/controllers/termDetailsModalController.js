angular.module('TermsDataEntry')
.controller("TermDetailsModalCtrl", function ($scope, $modalInstance, termDetailsServer, term) {

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
        termDetailsServer.getTermDetails(termId).then(function (details) {
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

    $scope.loadTermDetails(term.id);

    $scope.close = function () {
        $modalInstance.dismiss("close");
    };
})
