app.service("accountService", ['$http', function ($http) {

    this.savePassword = function (data) {
        return $http.post('/Account/SavePassword', JSON.stringify(data))
    }
    //Save
}]);