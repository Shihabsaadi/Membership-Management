app.controller('memberCtrl', function ($scope, memberService, organizationService) {
    organizationService.getOrganization().then(function (response) {
        $scope.OrganizationLists = response.data;
    });
    //memberService.getMember().then(function (response) {
    //    $scope.MemberLists = response.data;
    //    console.log(response.data)
    //});
    memberService.getDivision().then(function (response) {
        $scope.DivisionLists = response.data;
        console.log(response.data)
    });
    $scope.reset = function () {
        $scope.name = null;
        $scope.username = null;
        $scope.address = null;
        $scope.phone = null;
        $scope.gender = null;
        $scope.status = null;
        $scope.email = null;
        $scope.id = null;
        $scope.paymentFee = null;
    }
    $scope.onClick = function (expression, obj) {
        $scope.Editable = false;
        var data = []
        switch (expression) {
            case "SearchByOrganization":
                $scope.selectedOrganization = obj
                data =
                    {
                        OrganizationId: $scope.selectedOrganization
                    }
                memberService.getMemberByOrgnization(data).then(function (response) {
                    $scope.MemberLists = response.data;
                    console.log(response.data)
                });
                break;
            case "SaveMember":

                if ($scope.upazilla != null) {
                    $scope.UpazilaId = $scope.upazilla
                }
                else {
                    $scope.UpazilaId = null
                }
                data =
                {
                    Id: $scope.id,
                    Name: $scope.name,
                    UserName: $scope.username,
                    DivisionId: $scope.division,
                    DistrictId: $scope.district,
                    UpazillaId: $scope.UpazilaId,
                    Address: $scope.address,
                    Phone: $scope.phone,
                    Gender: $scope.gender,
                    Status: $scope.status,
                    Email: $scope.email,
                    OrganizationId: $scope.selectedOrganization,
                    PaymentFee: $scope.paymentFee
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
                        //swal("Updated!", $scope.name + " Updated Successfully!", "success");
                        $scope.Message = response.data.Message
                        $scope.reset()
                        data =
                         {
                             OrganizationId: $scope.selectedOrganization
                         }
                        memberService.getMemberByOrgnization(data).then(function (response) {
                            $scope.MemberLists = response.data;
                            console.log(response.data)
                        });
                    }

                });


                //else {
                //    data =
                //       {
                //           Name: $scope.name,
                //           UserName: $scope.username,
                //           Address: $scope.address,
                //           Phone: $scope.phone,
                //           Gender: $scope.gender,
                //           Status: $scope.status,
                //           Email: $scope.email,
                //           DivisionId: $scope.DivisionId,
                //           DistrictId: $scope.DistrictId,
                //           UpazillaId: $scope.upazilla.UpazilaId,
                //       }


                //    memberService.saveMember(data).then(function (response) {

                //        if (response.data) {
                //            Swal.fire({
                //                position: 'top-end',
                //                type: 'success',
                //                title: 'Added!,' +$scope.name +' Added Successfully!',
                //                showConfirmButton: false,
                //                timer: 1500
                //            })
                //            //swal("Added!", $scope.name + " Added Successfully!", "success");
                //            $scope.MemberLists.push({
                //                Name: $scope.name,
                //                Password: $scope.password,
                //                UserName: $scope.username,
                //                Address: $scope.address,
                //                Phone: $scope.phone,
                //                Gender: $scope.gender,
                //                Status: $scope.status,
                //                Email: $scope.email
                //            })
                //            $scope.Message = response.data.Message
                //            $scope.reset()
                //        }

                //    });
                //}

                break;
            case "EditMember":
                console.log(obj)
                data = { DivisionId: obj.DivisionId }
                memberService.getDistrict(data).then(function (response) {
                    if (response.data) {
                        $scope.DistrictLists = response.data
                    }

                })
                data = { DistrictId: obj.DistrictId }
                memberService.getUpazila(data).then(function (response) {
                    if (response.data) {
                        $scope.UpazillaLists = response.data
                    }
                    $scope.Editable = true;
                    $scope.name = obj.Name
                    $scope.username = obj.UserName
                    $scope.address = obj.Address
                    $scope.phone = obj.Phone
                    $scope.gender = obj.Gender
                    $scope.status = obj.Status
                    $scope.email = obj.Email
                    $scope.id = obj.Id
                    $scope.division = obj.DivisionId
                    $scope.district = obj.DistrictId
                    $scope.upazilla = obj.UpazillaId
                    $scope.paymentFee = obj.PaymentFee
                })

                break;

            case "ChangeStatus":
                swal.fire({
                    title: 'Are you sure?',
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes'
                }).then(function (response) {
                    if (response.value == true && obj.Status == true) {


                        data = { Id: obj.Id, Status: 0 }
                        memberService.inActiveMember(data).then(function (response) {
                            $scope.Message = response.data
                            swal.fire(
                            $scope.Message,
                            obj.Name,
                            'success'
                            )
                            if (response.data) {
                                data =
                                    {
                                        OrganizationId:$scope.selectedOrganization
                                    }
                                memberService.getMemberByOrgnization(data).then(function (response) {
                                    $scope.MemberLists = response.data
                                });
                            }
                        });

                    }
                    else if (response.dismiss != "cancel") {
                        swal.fire(
                           'Activated!',
                            obj.Name + ' has been activated',
                            'success'
                           )
                        data = { Id: obj.Id, Status: 1 }
                        memberService.inActiveMember(data).then(function (response) {
                            if (response.data) {
                                console.log($scope.selectedOrganization)
                                data =
                                    {
                                        OrganizationId: $scope.selectedOrganization
                                    }
                                memberService.getMemberByOrgnization(data).then(function (response) {
                                    $scope.MemberLists = response.data
                                });
                            }
                        });
                    }
                });
                break;
            case "ChangeDivison":
                $scope.division = obj;
                data = { DivisionId: $scope.division }
                memberService.getDistrict(data).then(function (response) {
                    if (response.data) {
                        $scope.DistrictLists = response.data
                        console.log(response.data)
                    }
                })
                break;
            case "ChangeDistrict":
                $scope.district = obj;
                data = { DistrictId: $scope.district }
                memberService.getUpazila(data).then(function (response) {
                    if (response.data) {
                        $scope.UpazillaLists = response.data
                        console.log(response.data)
                    }
                })
                break;

        }
    }

});