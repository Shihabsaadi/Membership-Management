app.service("transactionService", ['$http', function ($http) {
    this.getTransaction = function (data) {
        return $http.post("/Transaction/GetTransactionList", JSON.stringify(data));
    }
    this.getServiceBill = function (data) {
        return $http.post("/Transaction/GetServiceBillList", JSON.stringify(data))
    }
    this.saveServiceBill = function (data) {
        return $http.post("/Transaction/SaveServiceBill", JSON.stringify(data))
    }
    //Save Transaction
    this.saveTransaction = function (data) {
        return $http.post('/Transaction/SaveTransaction', JSON.stringify(data));
    }
    this.getPersonTransaction = function(data)
    {
        return $http.post('/Transaction/GetPersonTransaction', JSON.stringify(data));
    }
    this.saveAmountToPay = function(data)
    {
        return $http.post('/Transaction/SaveAmountTo', JSON.stringify(data));

    }
    this.getAmount = function () {
        return $http.get("/Transaction/GetAmount");
    }
    
}]);