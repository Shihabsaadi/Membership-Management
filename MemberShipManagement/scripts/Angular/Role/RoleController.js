app.controller('roleCtrl', function ($scope, roleService) {
    var getRoleList = function ()
    {
        roleService.getRole().then(function (response) {
            $scope.RoleList = response.data;
        })
    }
    getRoleList()
   
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "EditRole":
                $scope.Name = obj.Name
                $scope.Id = obj.Id
                $scope.Status = obj.Status
                break;
            case "SaveRole":
                data=
                    {
                        Id: $scope.Id,
                        Name: $scope.Name,
                        Status:$scope.Status
                    }
                roleService.saveRole(data).then(function (response) {

                    if (response.data) {
                        Swal.fire({
                            position: 'top-end',
                            type: 'success',
                            title: response.data.Message,
                            showConfirmButton: false,
                            timer: 1500
                        })
                        getRoleList();
                    }

                });
                break;
            case "SetRole":
                if ($scope.id != null) {
                    data =
                    {
                        Id: $scope.id,
                        Organization_Id: $scope.selectedOrganization.Id,
                        
                    }
                    memberService.saveMember(data).then(function (response) {

                        if (response.data) {
                            memberService.getMember().then(function (response) {
                                $scope.MemberLists = response.data;
                            });
                        }

                    });

                }
                else {
                    data =
                       {
                           Name: $scope.name,
                           Password: $scope.password,
                           UserName: $scope.username,
                           Address: $scope.address,
                           Phone: $scope.phone,
                           Gender: $scope.gender,
                           Status: $scope.status,
                           Email: $scope.email
                       }


                    memberService.saveMember(data).then(function (response) {

                        if (response.data) {
                            Swal.fire({
                                position: 'top-end',
                                type: 'success',
                                title: 'Added!,' + $scope.name + ' Added Successfully!',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            //swal("Added!", $scope.name + " Added Successfully!", "success");
                            $scope.MemberLists.push({
                                Name: $scope.name,
                                Password: $scope.password,
                                UserName: $scope.username,
                                Address: $scope.address,
                                Phone: $scope.phone,
                                Gender: $scope.gender,
                                Status: $scope.status,
                                Email: $scope.email
                            })
                            $scope.Message = response.data.Message
                            $scope.reset()
                        }

                    });
                }

                break;
            case "Search":
                rolePermissionService.search().then(function (response) {
                    $scope.OrganizationMemberLists = response.data;
                });
                break;
             default:
        }
    }
});