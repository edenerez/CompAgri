(function () {
    angular.module('TermsDataEntry')
    .controller("UploadFileController", UploadFileController);

    function UploadFileController($scope, $http, $location, uploadFileServer, loginService) {

        if (!loginService.getUser()) {
            $location.path('/login')
        }
        else {

            $scope.alerts = [];
            $scope.addAlert = function (type, msg) {
                //Clear Alert Before adding another
                $scope.alerts = [];
                $scope.alerts.push({ type: type, msg: msg });
            };

            $scope.closeAlert = function (index) {
                $scope.alerts.splice(index, 1);
            };

            $scope.uploadFile = function () {
                var file = $scope.myFile;
                if (file) {
                    uploadFileServer.uploadXmlFile(file)
                        .success(function (data) {
                            $scope.myFile = "";
                            $scope.addAlert("success", "File Uploaded Successfully");
                        })
                        .error(function () {
                            $scope.addAlert("danger", "Error in uploading");
                        });
                }
                else {
                    $scope.addAlert("danger", "Please select file to upload");
                }
            };
        }
    }
}())