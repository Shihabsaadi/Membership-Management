app.controller('insightsCtrl', function ($scope, memberService, organizationService, committeeService, serviceService) {
    //memberService.getAllMembers().then(function (response) {
    //    $scope.allMembers = response.data;
    //    console.log($scope.allMembers)
    //});
    memberService.getAllMembers().then(function (response) {
        $scope.allMembers = response.data;
    });
    organizationService.getOrganization().then(function (response) {
        $scope.OrganizationLists = response.data;
    });
    committeeService.getCommitteeByOrganizationId().then(function (response) {
        $scope.committeList = response.data;
    });
    serviceService.getService().then(function (response) {
        $scope.ServiceList = response.data;
    });
    //committeeCtrl.organizationService.getOrganization().then(function (response) {
    //    alert();
    //});
 
});