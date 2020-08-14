app.controller('myBillCtrl', function ($scope, myBillService,$filter) {
    $scope.ToDate = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "yyyy-MM-dd");
            }
        }
        return value;
    }
    $scope.overlay = true;
    myBillService.getMyBillList().then(function (response) {
        $scope.MyBillList = response.data;
        $scope.overlay = false;
    });
});