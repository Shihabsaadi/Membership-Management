app.service("organizationService", ['$http', function ($http) {
    //Get all memberlist
    this.getOrganization = function () {
        return $http.get("/Organization/GetOrganizationList");
    }
    //Save member
    this.saveOrganization = function (data) {
        return $http.post('/Organization/SaveOrganization', JSON.stringify(data))
    }
}]);