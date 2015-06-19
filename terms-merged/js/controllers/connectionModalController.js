angular.module('TermsDataEntry')
 .controller('ConnectionModalCtrl', function ($scope, $modalInstance, connectionServer, data) {
     $scope.pvalues = {
         "Names": [],
         "Synonyms": [],
         "TimeLimitation": [],
         "PositionLimitation": [],
         "AmountLimitation": [],
         "ClimateLimitation": [],
         "SeasonLimitation": [],
         "Measurement": []
     }

     $scope.term1 = data.term1.scope().$nodeScope.$modelValue;
     $scope.term2 = data.term2.scope().$nodeScope.$modelValue;

     connectionServer.getConnectionsPosibleValues().then(function (pvalues) {
         $scope.pvalues = pvalues;

         $scope.errors = function (prop) {
             if ($scope.validate && prop === 'Connection_Name') {
                 if (!$scope.Connection_Name)
                     $scope.errors.Connection_Name = "The Connection Name is required";
                 else
                     $scope.errors.Connection_Name = undefined;

                 return $scope.errors.Connection_Name;
             } else if ($scope.validate && prop === 'Connection__Amount_Limitation') {
                 if ($scope.connectionForm.$error && $scope.connectionForm.$error.number)
                     $scope.errors.Connection__Amount_Limitation = "The amount limitation must be a number";
                 else
                     $scope.errors.Connection__Amount_Limitation = undefined;

                 return $scope.errors.Connection__Amount_Limitation;
             }
         };


         $scope.saveConnection = function () {
             $scope.validate = true;
             if (!$scope.errors('Connection_Name') && !$scope.errors('Connection__Amount_Limitation')) {
                 $modalInstance.close({
                     Connection_Name: $scope.Connection_Name,
                     Connection_Synonym: $scope.Connection_Synonym,
                     Connection_Time_Limitation: $scope.Connection_Time_Limitation,
                     Connection_Position_Limitation: $scope.Connection_Position_Limitation,
                     Connection__Amount_Limitation: $scope.Connection__Amount_Limitation,
                     Connection_Climate_Limitation: $scope.Connection_Climate_Limitation,
                     Connection_Season_Limitation: $scope.Connection_Season_Limitation,
                     Connection_Measurement: $scope.Connection_Measurement
                 });
             };
         };

         $scope.cancel = function () {
             $modalInstance.dismiss("cancel");
         };

     }, function (e) {
         $modalInstance.dismiss(e);
     });
 });