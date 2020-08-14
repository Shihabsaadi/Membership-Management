app.service("myBillService", ['$http', function ($http) {
    this.getMyBillList = function (data) {
        return $http.post('/MyBill/GetMyBill', JSON.stringify(data))
    }
}]);