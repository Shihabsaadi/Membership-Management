app.service("securityUserService", ['$http', function ($http) {
    this.getSecurityUser = function () {
        return $http.get("/Security/GetSecurityUser");
    }
    this.saveSecurityUser = function (data) {
        return $http.post('/Security/SaveSecurityUser', JSON.stringify(data))
    }
    this.checkDuplicate = function (data) {
        return $http.post('/Security/GetDuplicateUserNameValidation', JSON.stringify(data))
    }
}]);