app.controller('transactionCtrl', function ($scope, transactionService, memberService, $filter, organizationService) {
    //var getTransactionList = function ()
    //{
    //    transactionService.getTransaction().then(function (response) {
    //        $scope.TransactionList = response.data;
    //        console.log(response.data)
    //    });
    //}
    organizationService.getOrganization().then(function (response) {
        $scope.OrganizationLists = response.data;
    });
    $scope.showPersonTransaction = false
    var ToMonths = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "MM");
            }
        }
        return value;
    }
    //getTransactionList()
    var ToDate = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "dd-MMM-yyyy");
            }
        }
        return value;
    }

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

    $scope.onClick = function (expression, obj) {
        var Message;
        var data = []
        switch (expression) {
            case "Set Transaction":
                console.log(obj)
                $scope.PayementFee = obj.PaymentFee
              
                //transactionService.getAmount().then(function (response) {
                //    $scope.AmountToPay = response.data;
                //})
                $scope.MemberName = obj.MemberName
                $scope.PaidById = obj.Member_Id
                $scope.Mail=obj.Email
                break;
            case "Save Transaction":
                var date = new Date();
                var Id = $scope.PaidById
                var Mail = $scope.Mail
                var ElectricBill = $scope.ElectricBill
                var GasBill = $scope.GasBill
                var OtherService = $scope.OtherService
                var currentTime =  date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2);
                data =
                    {
                        Id: $scope.TransactionId,
                        PaidBy_Id: Id,
                        Billing_Date:$scope.BillingDate,
                        Amount: $scope.RecievedAmount,
                        Received_Date: currentTime,
                        Email: Mail,
                        PaymentFee: $scope.PayementFee,
                        ElectricBill:ElectricBill,
                        GasBill:GasBill,
                        OtherService: OtherService

                    }
                transactionService.saveTransaction(data).then(function (response) {
                    if (response.data) {
                        Message = response.data.Message    
                        Swal.fire({
                            position: 'top-end',
                            type: 'success',
                            title: Message,
                            showConfirmButton: false,
                            timer: 1500
                        })
                        data =
                     {
                         Billing_Date: $scope.searchedMonth
                     }
                        transactionService.getTransaction(data).then(function (response) {
                            $scope.TransactionList = response.data;
                        });
                    }
                });
                break;
            case "See Transaction List":
               
                $scope.MemberName = obj.MemberName
                data =
                    {
                        PaidBy_Id: obj.PaidBy_Id
                    }
                transactionService.getPersonTransaction(data).then(function (response) {
                    if (response.data) {
                        $scope.PersonTransation = response.data
                    }
                });
                $scope.showPersonTransaction =true
                break;
            case "Close showPersonTransaction":
                $scope.showPersonTransaction = false
                break;
            case "searchMonthlyBill":
                $scope.searchedMonth = obj;
                data =
                    {
                        Organization_Id : $scope.selectedOrganization,
                        Billing_Date: obj
                    }
                transactionService.getTransaction(data).then(function (response) {
                    $scope.TransactionList = response.data;
                    console.log($scope.TransactionList)
                    });
               
                break;
            case "Save Amount To Pay":
                data =
                    {
                        Amount: $scope.AmountToPay
                    }
                transactionService.saveAmountToPay(data).then(function (response)
                {
                    if (response.data) {
                        Message = response.data.Message
                        Swal.fire({
                            position: 'top-end',
                            type: 'success',
                            title: Message,
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                })
                break;
            case "EditTransaction":
                console.log(obj)
                $scope.RecievedAmount = obj.PaymentFee
                $scope.TransactionId = obj.Id
                break;
            default:
        }
    }
});