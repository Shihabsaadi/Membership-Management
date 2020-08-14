app.service("committeeService", ['$http', function ($http) {

    this.getCommittee = function (data) {
        //return $http.get("/Committee/GetCommitteeList");
    }
    this.getCommitteeByOrganizationId = function (data) {
        return $http.post('/Committee/GetCommitteeList', JSON.stringify(data))
    }
    //Save
    this.saveCommittee = function (data) {
        return $http.post('/Committee/SaveCommittee', JSON.stringify(data))
    }
}]);