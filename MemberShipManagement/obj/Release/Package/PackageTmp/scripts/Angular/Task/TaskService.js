app.service("taskService", ['$http', function ($http) {
    this.getTask = function (data) {
        return $http.post("/Task/GetTaskList", JSON.stringify(data));
    }
    //Save Transaction
    this.saveTask = function (data) {
        return $http.post('/Task/SaveTask', JSON.stringify(data))
    }
    this.saveAgenda = function (data) {
        return $http.post('/Task/SaveAgenda', JSON.stringify(data))
    }
    this.searchMemberByComId = function (data) {
        return $http.post("/SetAccess/GetMemberListForSetCommittee", JSON.stringify(data));
    }
    this.assignMember = function (data) {
        return $http.post("/Task/AssignMember", JSON.stringify(data));
    }
    

}]);