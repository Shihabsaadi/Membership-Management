app.controller('committeeMemberCtrl', function ($scope, $filter, committeeService, committeeMemberService, roleService, organizationService) {
    //var getCommitteeList = function ()
    //{
    //    committeeService.getCommittee().then(function (response) {
    //        $scope.CommitteeList = response.data;
    //    })
    //}
    organizationService.getOrganization().then(function (response) {
        $scope.OrganizationLists = response.data;
    });
      
    var roleList=[]
    var getRoleList = function () {
        roleService.getRole().then(function (response) {
            $scope.RoleList = response.data;
            console.log(response.data)
        })

    }
    $scope.takenCommittee = function ()
    {
        $scope.selectedCommittee;
    }
    $scope.takenRole=function(obj)
    {
        $scope.ChangeCommitteeRole = obj;
    }
    $scope.remove = function()
    {
        $scope.removeMemberFromCommittee = true;
    }
    $scope.assignedMembers = true;
    getRoleList()
    //getCommitteeList();
    roleService.getRole().then(function (response) {
        $scope.RoleList = response.data;
    })
    var SelectedRole = $scope.ChangeCommitteeRole
    var s = $scope.selectedCommittee
    $scope.onClick = function (expression, obj) {
        var data = []
        var SelectedCommittee = obj
        //var SelectedRole = obj1;
        var assignedMembers = $scope.assignedMembers
        switch (expression) {
            case "SearchMember":
                
                data =
                    {
                        SelectedCommittee: SelectedCommittee,
                        Status: assignedMembers,
                        OrganizationId: $scope.SelectedOriganization
                    }
                committeeMemberService.searchMemberByComId(data).then(function(response)
                    {
                    var memberList = response.data;
                    
                    $scope.MemberList = memberList
                    
                    })
               
                break;

            case "ChangeCommittee":
                $scope.MemberList=null
                break;

            case "editMemberForCommittee":
                $scope.editable=true
                $scope.setRole = obj
                $scope.setMember = obj.Name
                $scope.setStatus=obj.Status
                break;
            case "ChangeCommitteeRole":
                console.log(obj)
                data =
                   {
                       RId: obj.RId,
                       Id: obj.Id,
                       RoleId: $scope.ChangeCommitteeRole,
                       SelectedCommittee: $scope.selectedCommittee,
                       CommitteMemberId: obj.CommitteMemberId,
                       Validation:$scope.removeMemberFromCommittee
                   }
                {
                    committeeMemberService.saveAccess(data).then(function (response) {
                        if (response.data) {
                            Swal.fire({
                                position: 'top-end',
                                type: 'success',
                                title: response.data.Message,
                                showConfirmButton: false,
                                timer: 1500
                            })

                            data =
                                {
                                    SelectedCommittee: $scope.selectedCommittee,
                                    Status: assignedMembers,
                                    OrganizationId: $scope.SelectedOriganization
                                }
                            committeeMemberService.searchMemberByComId(data).then(function (response) {
                                var memberList = response.data;

                                $scope.MemberList = memberList
                                $scope.removeMemberFromCommittee = false
                                $scope.ChangeCommitteeRole=null
                            })

                            console.log(data)
                            //start user create
                           
                            //ENds
                          
                        }
                    })
                }
                break;
            case "Save Web Access":
                data =
                    {
                        Id: obj.Id,
                        Name : obj.Name,
                        UserName : obj.Email,
                        Password : obj.Phone,
                        Member_Id : obj.Id,
                        Committee_Id: $scope.selectedCommittee,
                        Organization_Id:$scope.SelectedOriganization
                    }
                committeeMemberService.saveWebAccess(data).then(function(response)
                {
                  
                })
                break;
            case "ChangeOrganization":
                $scope.SelectedOriganization = obj
                data =
                    {
                        OrganizationId: obj
                    }
                committeeService.getCommitteeByOrganizationId(data).then(function (response) {
                    $scope.CommitteeList = response.data;
                })
                break;
            case "ChangeButton":
                $scope.MemberList[obj].UserCommitteeId = $scope.selectedCommittee
                console.log($scope.MemberList[obj].UserCommitteeId)
                break;
        }
    }
})