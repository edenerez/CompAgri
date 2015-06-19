angular.module('TermsDataEntry')
.controller("AddNodeModalCtrl", function ($scope, $modalInstance) {

    $scope.errors = function (prop) {
        if ($scope.validate && prop === 'descriptor') {
            if (!$scope.descriptor)
                $scope.errors.descriptor = "The english term is required";
            else
                $scope.errors.descriptor = undefined;

            return $scope.errors.descriptor;
        }
    };

    $scope.continueAdding = function () {
        $scope.validate = true;
        if (!$scope.errors('descriptor')) {
            $modalInstance.close({
                DESCRIPTOR: $scope.descriptor,
                ES: $scope.es,
                SO: $scope.so,
                UF: $scope.uf,
                DF: $scope.df,
                DS: $scope.ds
            });
        };
    };

    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
})