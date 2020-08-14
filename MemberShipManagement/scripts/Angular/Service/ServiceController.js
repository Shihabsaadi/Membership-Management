app.controller('serviceCtrl', function ($scope, serviceService, organizationService, memberService) {
    var getServiceList = function () {
        serviceService.getService().then(function (response) {
            $scope.ServiceList = response.data;
           
        })
    }
    organizationService.getOrganization().then(function (response) {
        $scope.OrganizationLists = response.data;
    });
    getServiceList()
    $scope.reset = function Reset()
    {
        $scope.Name = null;
        $scope.Id = null;
        $scope.Status = null;
    }
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "EditService":
                $scope.Name = obj.Name
                $scope.Id = obj.Id
                $scope.Status = obj.Status
                break;
            case "SaveService":
                data =
                   {
                       Id: $scope.Id,
                       Name: $scope.Name,
                       Status: $scope.Status
                   }
                serviceService.saveService(data).then(function (response) {
                    if (response.data) {
                        getServiceList()
                        $scope.reset();
                    }
                });
                break;
            case "SearchMemberByOrganization":
                data =
                    {
                        OrganizationId: $scope.selectedOrganization
                    }
                   serviceService.getMemberByOrgnization(data).then(function (response) {
                       $scope.MemberLists = response.data;
                });
                break;
            case "ChangeOrganization":
                
                break;
            case "AccessServiceToMember":
                $scope.ServiceHolderName = obj.Name
                $scope.ServiceHolderId = obj.Id
                data =
                    {
                        MemberId:obj.Id
                    }
                    serviceService.getServiceToAccess(data).then(function (response) {
                        $scope.ServiceListToAccess = response.data;
                    })
                    break;
            case "SaveAccessForMember":
                $scope.ServiceId = obj.Id;
                var SetAccess;
                if (obj.HasAccess == true)
                {
                    SetAccess = false;
                }
                else
                {
                    SetAccess = true;
                }
                data =
                    {
                        MemberId: $scope.ServiceHolderId,
                        Id: obj.Id,
                        Status: SetAccess
                    }
                serviceService.saveServiceForMember(data).then(function (response) {
                    if (response.data) {
                      
                    }
                });
                break;
        }
    }
});
