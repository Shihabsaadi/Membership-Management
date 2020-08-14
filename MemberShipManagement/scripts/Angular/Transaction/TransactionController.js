app.controller('transactionCtrl', function ($scope, transactionService, memberService, $filter, organizationService, serviceService) {
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
    $scope.print = function printData(printTable) {
        var divToPrint = document.getElementById("printTable");
        newWin = window.open("");
        newWin.document.write(divToPrint.outerHTML);
        newWin.print();
        newWin.close();
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
            case "Select Service Bill":
                $scope.GetMemberServiceId = obj.Id;
                $scope.ServicePaymentAmount = obj.GetDueAmount;
                $scope.ServiceBill = obj.GetAmountToPay;
                break;
            case "Save Service Bill":
                
                data=
                    {
                        Id: $scope.GetMemberServiceId,
                        Billing_Date: $scope.searchedMonth,
                        GetDueAmount: $scope.ServicePaymentAmount,
                        Amount: $scope.ServiceBill
                    }
                transactionService.saveServiceBill(data).then(function (response) {
                    data =
               {
                   Organization_Id: $scope.selectedOrganization,
                   Billing_Date: $scope.searchedMonth,
                   ServiceId: $scope.selectedService,
                   Status: true,
                   MemberStatus: true
               }
                    transactionService.getServiceBill(data).then(function (response) {
                        $scope.ServiceListBill = response.data;
                    });
                });
                break;
            case "ChangeService":
                data =
                {
                    Organization_Id: $scope.selectedOrganization,
                    Billing_Date: $scope.searchedMonth,
                    ServiceId: obj,
                    Status: true,
                    MemberStatus:true
                }
                transactionService.getServiceBill(data).then(function (response) {
                    $scope.ServiceListBill = response.data;
                });
                break;
            case "Set Transaction":

                $scope.BillingToDate = $scope.monthBill;
                $scope.PayementFee = obj.PaymentFee
                $scope.MemberName = obj.MemberName
                $scope.PaidById = obj.Member_Id
                $scope.Mail = obj.Email
                break;
            case "Save Transaction":
                var date = new Date();
                var Id = $scope.PaidById
                var Mail = $scope.Mail
                var currentTime = date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2);
                data =
                    {
                        Id: $scope.TransactionId,
                        PaidBy_Id: Id,
                        //Billing_Date: $scope.BillingDate,
                        BillingFromDate: $scope.monthBill,
                        BillingToDate:$scope.BillingToDate,
                        Amount: $scope.RecievedAmount,
                        Received_Date: currentTime,
                        Email: Mail,
                        PaymentFee: $scope.PayementFee,
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
                         Organization_Id: $scope.selectedOrganization,
                         Billing_Date: $scope.searchedMonth
                     }
                        transactionService.getTransaction(data).then(function (response) {
                            $scope.TransactionList = response.data;
                        });
                    }
                });
                break;
            case "See Transaction List":
                $scope.overlay = true;
                $scope.MemberName = obj.MemberName
                data =
                    {
                        PaidBy_Id: obj.PaidBy_Id
                    }
                transactionService.getPersonTransaction(data).then(function (response) {
                    if (response.data) {
                        $scope.PersonTransation = response.data;
                        
                    }
                    else
                    {
                        alert("Something Went Wrong");
                    }
                });
                $scope.overlay = false;
                $scope.showPersonTransaction = true

                transactionService.getServiceBill
                break;
            case "Close showPersonTransaction":
                $scope.showPersonTransaction = false
                break;
            case "searchMonthlyBill":
                $scope.overlay = true;
                $scope.searchedMonth = obj;
                data =
                    {
                        Organization_Id: $scope.selectedOrganization,
                        Billing_Date: obj
                    }
                transactionService.getTransaction(data).then(function (response) {
                    $scope.TransactionList = response.data;
                    data =
                            {
                                Status: true
                            }
                    serviceService.getService(data).then(function (response) {
                        if (response.data) {
                            $scope.ServiceList = response.data;
                        }
                    });
                });
                $scope.overlay = false;
                break;
            case "EditTransaction":
                $scope.PayementFee = obj.PaymentFee
                $scope.TransactionId = obj.Id
                break;
            default:
        }
    }
});