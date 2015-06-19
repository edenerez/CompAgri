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

            details.properties.forEach(function (item) {
                item.title = propertiesNames[item.Property_Key] || item.Property_Key;
            });
        });
    };

    $scope.loadTermDetails(term.id);

    $scope.close = function () {
        $modalInstance.dismiss("close");
    };
})
