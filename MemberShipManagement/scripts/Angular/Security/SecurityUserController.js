app.controller('securityCtrl', function ($scope, securityUserService) {
    var getSecurityUserList = function () {
        securityUserService.getSecurityUser().then(function (response) {
            $scope.securityUserList = response.data;
        })
    }
    getSecurityUserList();

    $scope.reset = function Reset() {
        $scope.Name = null;
        $scope.Id = null;
        $scope.Status = null;
        $scope.UserName = null;
        $scope.Password = null;
        $scope.editMode = false;
        $scope.validation = null;
        $scope.EmailValidation = null;
    }
    $scope.onKeyPress = function (expression, obj) {
        switch (expression) {
            case 'CheckDuplicateUserName':
                data =
                   {
                       UserName: $scope.UserName,
                       Id: $scope.Id
                   }
                securityUserService.checkDuplicate(data).then(function (response) {
                    $scope.validation = response.data;
                    if (response.data == true) {
                        $scope.EmailValidation = 'fa-check text-green';
                    }
                    else {
                        $scope.EmailValidation = 'fa-times text-red';
                    }
                });
                break;
            case "Confirm Password":
                if ($scope.Password == $scope.ConfirmPassword) {
                    $scope.PasswordValidation = "Password Matched";

                }
                else {
                    $scope.PasswordValidation = "Password Not Matched";
                }
                break;
        }
    }
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "EditUser":
                $scope.editMode = true;
                $scope.Id = obj.Id;
                $scope.Name = obj.Name;
                $scope.Status = obj.Status;
                $scope.UserName = obj.UserName;
                break;
            case "SaveUser":
                data =
                   {
                       Id: $scope.Id,
                       Name: $scope.Name,
                       UserName: $scope.UserName,
                       Password: $scope.Password,
                       Status :$scope.Status
                   }
                securityUserService.saveSecurityUser(data).then(function (response) {
                    
                    if (response.data) {
                        $scope.Message = response.data;
                        Swal.fire({
                            position: 'top-end',
                            title: $scope.Message,
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                    getSecurityUserList();
                    $scope.reset();
                })
                break;
            
        }
    }
});