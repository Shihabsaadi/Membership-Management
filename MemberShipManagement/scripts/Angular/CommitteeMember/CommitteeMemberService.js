app.service("committeeMemberService", ['$http', function ($http) {
    
    this.getCommittee = function () {
        return $http.get("/Committee/GetCommitteeList");
    }

    this.searchMemberByComId = function (data) {
        return $http.post("/SetAccess/GetMemberListForSetCommittee", JSON.stringify(data));
    }
    this.searchMemberByComIdForSetAccess = function (data) {
        return $http.post("/SetAccess/GetMemberListForSetCommittee", JSON.stringify(data));
    }
    this.saveAccess = function(data)
    {
        return $http.post("/SetAccess/SaveAccess", JSON.stringify(data));
    }
    this.saveWebAccess = function (data) {
        return $http.post("/User/SaveWebAccess", JSON.stringify(data));
    }

}]);