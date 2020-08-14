app.controller('committeeCtrl', function ($scope, $filter, committeeService, organizationService) {
    //var getCommitteeList = function ()
    //{
    //    committeeService.getCommittee().then(function (response) {
    //        $scope.CommitteeList = response.data;
    //        console.log($scope.CommitteeList)
    //    })
    //}
    organizationService.getOrganization().then(function (response) {
        $scope.OrganizationLists = response.data;
    });
    $scope.ToDate = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "dd-MMM-yyyy");
            }
        }
        return value;
    }
    //getCommitteeList()

    $scope.Reset = function () {
        $scope.Name = null
        $scope.Id = null
        $scope.Status = null
        $scope.DurationDate = null
    }

    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "EditCommittee":
                $scope.Name = obj.Name
                $scope.Id = obj.Id
                $scope.Status = obj.Status
                $scope.DurationDate = $scope.ToDate(obj.Duration_Date)
                break;
            case "SaveCommittee":
                if ($scope.Id == null) {
                    $scope.DurationDate = $scope.ToDate($scope.DurationDate)
                }
                data =
                    {
                        Id: $scope.Id,
                        Name: $scope.Name,
                        Status: $scope.Status,
                        Duration_Date: $scope.DurationDate,
                        OrganizationId: $scope.SelectedOriganization
                    }
                committeeService.saveCommittee(data).then(function (response) {

                    if (response.data) {
                        Swal.fire({
                            position: 'top-end',
                            type: 'success',
                            title: response.data.Message,
                            showConfirmButton: false,
                            timer: 1500
                        })
                        //getCommitteeList();
                        data =
                         {
                             OrganizationId: $scope.SelectedOriganization
                         }
                        committeeService.getCommitteeByOrganizationId(data).then(function (response) {
                            $scope.CommitteeList = response.data;
                        })
                        $scope.Reset()
                    }

                });
                break;
            case "OrganizationChange":

                break;
            case "SearchByOrganization":
                $scope.SelectedOriganization = obj
                data =
                    {
                        OrganizationId: obj
                    }
                committeeService.getCommitteeByOrganizationId(data).then(function (response) {
                    $scope.CommitteeList = response.data;
                })
                break;
            default:
        }
    }
});