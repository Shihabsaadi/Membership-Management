using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Nullable<DateTime>  TaskStart_Date { get; set; }
        public Nullable<DateTime> TaskEnd_Date { get; set; }
        public System.DateTime Created_Date { get; set; }
        public int AssignedBy_Id { get; set; }
        public string Remark { get; set; }
        public string AppointedTo { get; set; }
        public Nullable<bool> Status { get; set; }
        public int Committee_Id { get; set; }
        public DateTime TaskDate { get; set; }
        public String AgendaTitle { get; set; }
        public String AgendaRemark { get; set; }
        public int AgendaId { get; set; }
        public bool ? AgendaStatus { get; set; }
        public List<int> AgendaIdList { get; set; }
        public List<String> AgendaTitleList { get; set; }
        public List<String> AgendaRemarkList { get; set; }
        public List<bool ?>  AgendaStatusList { get; set; }
        public List<TaskModel> AgendaList { get; set; }
        public List<TaskModel> AssignedMemberList { get; set; }
        public int AssignedMemberId { get; set; }
        public string AssignedMemberName { get; set; }


    }
}