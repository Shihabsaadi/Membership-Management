app.service("memberService", ['$http', function ($http) {
    //Get all memberlist
    this.getMember = function () {
        return $http.get("/Member/GetMemberList");
    }
    this.getMemberByOrgnization = function(data)
    {
        return $http.post('/Member/GetMemberList', JSON.stringify(data))
    }
    this.getDivision = function () {
        return $http.get("/Member/GetDivision");
    }
    this.getDistrict = function (data) {
        return $http.post('/Member/GetDistrict', JSON.stringify(data))
    }
    //Save 
    this.getUpazila = function (data) {
        return $http.post('/Member/GetUpazila', JSON.stringify(data))
    }
    this.saveMember = function (data) {
        return $http.post('/Member/SaveMember', JSON.stringify(data))
    }

    this.inActiveMember = function (data) {
        return $http.post('/Member/InActiveMember', JSON.stringify(data))
    }
}]);

