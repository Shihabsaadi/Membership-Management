app.service("serviceService", ['$http', function ($http) {
    //this.getService = function () {
    //    return $http.get("/Service/GetServiceList");
    //}
    this.getService = function (data) {
        return $http.post("/Service/GetServiceList", JSON.stringify(data));
    }
    this.getServiceToAccess = function(data)
    {
        return $http.post("/Service/GetServiceListToAccess",JSON.stringify(data));
    }
    this.getMemberByOrgnization = function (data) {
        return $http.post('/Service/GetMemberList', JSON.stringify(data))
    }
    this.saveService = function (data) {
        return $http.post('/Service/SaveService', JSON.stringify(data))
    }
    this.saveServiceForMember = function (data) {
        return $http.post('/Service/SaveAccessForMember', JSON.stringify(data))
    }
}]);