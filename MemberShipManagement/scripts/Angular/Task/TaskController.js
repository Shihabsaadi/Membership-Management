app.controller('taskCtrl', function ($scope, $filter, taskService, committeeService, committeeMemberService, organizationService) {
    //var getCommitteeList = function () {
    //    committeeService.getCommittee().then(function (response) {
    //        $scope.CommitteeList = response.data;
    //    })
    //}
     organizationService.getOrganization().then(function (response) {
        $scope.OrganizationLists = response.data;
    });
    var Count = 1
    $scope.GridView = 'active'
    $scope.AgendaInputForm=[]
    $scope.GridViewColor = 'text-blue'
    $scope.view = 'Grid'
    var getTaskList = function () {
        taskService.getTask().then(function (response) {
            $scope.TaskList = response.data;
        });
    }
    var reset = function () {
        $scope.Title = null
        $scope.Description = null
        $scope.Id = null
        $scope.TaskTime = null
        $scope.Status = null
    }
    $scope.Reset = reset
    $scope.ToDate = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "yyyy-MM-dd HH:mm:ss");
            }
        }
        return value;
    }

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
    var ToDays = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "dd");
            }
        }
        return value;
    }
    var ToYears = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "yyyy");
            }
        }
        return value;
    }
    var ToHours = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "HH");
            }
        }
        return value;
    }
    var ToMinutes = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "mm");
            }
        }
        return value;
    }
    var ToSeconds = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return $filter('date')(new Date(+a[1]), "ss");
            }
        }
        return value;
    }
   
    //getCommitteeList()
    $scope.numLimit = 10;
    $scope.readMore = function () {
        $scope.numLimit = 10000;
    };

    $scope.onClick = function (expression, obj)
    {
        var data=[]
        switch (expression) 
        {
            case "SearchTask":
                data =
                    {
                        TaskStart_Date: $scope.StartDate,
                        TaskEnd_Date: $scope.EndDate,
                        Committee_Id: $scope.selectedCommittee
                    }
                taskService.getTask(data).then(function (response) {
                    $scope.TaskList = response.data;
                })
                break;
            case "EditTask":
                $scope.Title = obj.Title
                $scope.Description = obj.Remark
                $scope.Id = obj.Id
                var year = ToYears(obj.TaskDate)
                var date = ToDays(obj.TaskDate)
                var month = ToMonths(obj.TaskDate)-1
                var hours = ToHours(obj.TaskDate)
                var second = ToSeconds(obj.TaskDate)
                var minutes = ToMinutes(obj.TaskDate)
                $scope.Status = obj.Status
                $scope.TaskTime = {
                    value: new Date(year, month, date, hours, minutes)
                };
                break;
            case "SaveTask":
                data =
                                    {
                                        Title: $scope.Title,
                                        Remark: $scope.Description,
                                        Status: $scope.Status,
                                        TaskDate: $scope.TaskTime.value,
                                        Id: $scope.Id,
                                        Committee_Id:$scope.selectedCommittee
                                    }
                taskService.saveTask(data).then(function (response) {
                    data =
                    {
                        TaskStart_Date: $scope.StartDate,
                        TaskEnd_Date: $scope.EndDate,
                        Committee_Id: $scope.selectedCommittee
                    }
                    taskService.getTask(data).then(function (response) {
                        $scope.TaskList = response.data;
                    })
                })
                break;
            case "ViewType":
                if (obj == 'GridView' && $scope.GridView == 'active')
                {
                    $scope.GridView = 'active'
                    $scope.TableView = null
                    $scope.view = 'Grid'
                    $scope.TableViewColor = null
                    $scope.GridViewColor = 'text-blue'
                   
                }
                else if(obj == 'GridView' && $scope.GridView != 'active')
                {
                    $scope.GridView = 'active'
                    $scope.TableView = null
                    $scope.view = 'Grid'
                    $scope.TableViewColor = null
                    $scope.GridViewColor = 'text-blue'
                }
                else 
                {
                    $scope.TableView = 'active'
                    $scope.GridView = null
                    $scope.view = 'Table'
                    $scope.TableViewColor = 'text-blue'
                    $scope.GridViewColor = null
                }
                break;
            case "AddInputForAgenda":
                $scope.AgendaInputForm.push({ Count: Count })
                Count++
                break;
            case "RemoveInputFromAgenda":
                $scope.AgendaInputForm.splice(obj,1)
                break;
            case "SaveAgenda":
                data =
                    {
                        Id:$scope.Id ,
                        AgendaTitle:obj.title,
                        AgendaRemark: obj.description
                    }
                taskService.saveAgenda(data).then(function (response) {
                    data =
                     {
                         TaskStart_Date: $scope.StartDate,
                         TaskEnd_Date: $scope.EndDate,
                         Committee_Id: $scope.selectedCommittee
                     }
                    taskService.getTask(data).then(function (response) {
                        $scope.TaskList = response.data;
                    })
                })
                obj.title = null
                obj.description = null
                break;
            case "AddAgendaStatus":
                data =
                    {
                        AgendaId: obj.AgendaId,
                        AgendaStatus: obj.AgendaStatus
                    }
                taskService.saveAgenda(data).then(function (response) {
                    
                })
                break;
            case "Catch":
                break;
            case "FindMember":
                $scope.SelectedTaskId = obj.Id
                data =
                    {
                      
                        SelectedCommittee: $scope.selectedCommittee,
                        Status: true
                    
                    }
                committeeMemberService.searchMemberByComId(data).then(function (response)
                {
                    $scope.MemberToAssignList = response.data
                })
                break;
            case "ResetMember":
                $scope.MemberToAssignList=null
                break;
            case "AssignMemberSave":
                data=
                    {
                        Id: $scope.SelectedTaskId,
                        AssignedMemberId: obj.Id
                    }
                taskService.assignMember(data).then(function (response)
                {
                })
                break;
            case "OrganizationChange":
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
})