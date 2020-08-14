app.controller('organizationCtrl', function ($scope, organizationService) {
    organizationService.getOrganization().then(function (response) {
        $scope.OrganizationLists = response.data;
    });
    var date = new Date();
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
    $scope.onClick = function (expression, obj) {
        var index
        var orgObj = obj;
        console.log(obj)
        switch (expression) {
            case "EditOrganization":
                console.log(orgObj)
                $scope.Name = orgObj.Name
                $scope.OrgId = orgObj.Id
                $scope.Status = orgObj.Status
                break;
            case "SaveOrganization":
                var data = []
                var currentTime = date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2);
                data =
                    {
                        Name: $scope.Name,
                        CreatedDate: currentTime,
                        Id: $scope.OrgId,
                        Status: $scope.Status
                    }
                organizationService.saveOrganization(data).then(function (response) {
                    if (response.data) {
                        Message = response.data.Message
                        Swal.fire({
                            position: 'top-end',
                            type: 'success',
                            title: Message,
                            showConfirmButton: false,
                            timer: 1000
                        })
                        organizationService.getOrganization().then(function (response) {
                            $scope.OrganizationLists = response.data;
                        });
                    }
                });
                break;

            case "SaveMember":
                var data = []
                data =
             {
                 Name: obj.name,
                 Status: obj.status
             }
                memberService.saveOrganization(data).then(function (response) {

                    if (response.data) {
                        $scope.OrganizationLists.push({
                            Name: obj.name,
                            Status: obj.status
                        })
                        console.log(response.data)
                        $scope.Message = response.data.Message
                    }

                });
                break;
            case "EditMember":
                Index = obj;
                console.log(Index)
                break;

            case "Search":
                var data = []
                data =
             {
                 SelectedOrganization: obj
             }
                rolePermissionService.search(data).then(function (response) {
                    $scope.OrganizationMemberLists = response.data;
                    console.log(response.data)
                });
                console.log(obj)
                break;
            case "SetPermission":
                data =
                   {
                       MemberOrganizationId: obj.MemberOrganizationId,
                       Id: obj.Id,
                       SelectedOrganization: $scope.selectedOrganization,
                       Status: true

                   }
                rolePermissionService.savePermission(data).then(function (response) {
                    if (response.data) {
                        var data = []
                        data =
                     {
                         SelectedOrganization: $scope.selectedOrganization
                     }
                        rolePermissionService.search(data).then(function (response) {
                            $scope.OrganizationMemberLists = response.data;
                            console.log(response.data)
                        });
                    }
                });
                break;
            case "SaveRole":
                var data = []
                data =
                 {
                     User_Id: $scope.Id,
                     Name: obj.Value,
                     RoleId: $scope.RoleId
                 }
                rolePermissionService.saveRole(data).then(function (response) {
                    rolePermissionService.search(data).then(function (response) {
                        $scope.OrganizationMemberLists = response.data;
                        console.log(response.data)
                    });

                })
                break;
            case "Select":
                $scope.Id = obj.Id
                $scope.RoleId = obj.RoleId
                $scope.Role = obj.RoleName
                console.log($scope.Role)
                break;
            case "Reset":
                $scope.Name = null
                $scope.OrgId = null
                $scope.status = null
                break;
        }

    }

});