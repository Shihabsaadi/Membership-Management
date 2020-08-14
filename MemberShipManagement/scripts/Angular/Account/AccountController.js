app.controller('accountCtrl', function ($scope, accountService) {
    $scope.Reset = function() {
        $scope.OldPassword = null;
        $scope.NewPassword = null;
        $scope.ConfirmPassword = null;
        $scope.PasswordValidation = null;
    }
    $scope.onClick = function (expression, obj) {
        switch (expression) {
            case "Save Password":
                data =
                  {
                      Password: $scope.OldPassword,
                      NewPassword: $scope.NewPassword,
                      ConfirmPassword: $scope.ConfirmPassword
                  }
                  accountService.savePassword(data).then(function (response) {
                    if (response.data) {
                        Swal.fire({
                            position: 'top-end',
                            title: response.data,
                            showConfirmButton: false,
                            timer: 1500
                        });
                        $scope.Reset();
                    }
                });
                  break;
            case "Confirm Password":
                if ($scope.NewPassword == $scope.ConfirmPassword)
                {
                    $scope.PasswordValidation = "Password Matched";

                }
                else
                {
                    $scope.PasswordValidation = "Password Not Matched";
                }
                break;
            default:

        }
    }

});