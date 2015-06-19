angular.module('TermsDataEntry')
 .factory('uploadFileServer', function ($http, $q, apiLocation) {
     return {
         uploadXmlFile: function uploadXmlFile(file) {
             var fd = new FormData();
             fd.append('file', file);

             return $http.post(apiLocation + 'UploadXml', fd, {
                 transformRequest: angular.identity,
                 headers: { 'Content-Type': undefined }
             })
         }
     };
 });