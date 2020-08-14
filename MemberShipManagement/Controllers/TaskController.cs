using MemberShipManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberShipManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TaskController : Controller
    {
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetTaskList(TaskModel model)
        {
            bool startDate = true;
            if (model.TaskStart_Date == null && model.TaskEnd_Date==null)
            {
                startDate = false;
            }
            List<Task> taskList = db.Tasks.Where(x => x.Committee_Id == model.Committee_Id && startDate == false || x.Committee_Id==model.Committee_Id && x.Task_Date >= model.TaskStart_Date && x.Task_Date <= model.TaskEnd_Date).OrderByDescending(s=>s.Id).ToList();
            List<TaskModel> vm = taskList.Select(x => new TaskModel
            {
                Id = x.Id,
                AppointedTo = x.AppointedTo,
                AssignedBy_Id = x.AssignedBy_Id,
                Committee_Id = x.Committee_Id,
                Created_Date = x.Created_Date,
                Remark = x.Remark,
                Status = x.Status,
                Title = x.Title,
                TaskDate = x.Task_Date,
                //AgendaIdList = GetAgendaIdList(x.Id),
                //AgendaTitleList = GetAgendaTitleList(x.Id),
                //AgendaRemarkList = GetAgendaRemarkList(x.Id),
                //AgendaStatusList = GetAgendaStatusList(x.Id),
                AssignedMemberList = GetAssignedMemberList(x.Id),
                AgendaList = GetAgendaList(x.Id)
            }).ToList();
            return Json(vm,JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveTask(TaskModel model)
        {
            var Message = "Task addition Failed";
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (model.Id>0)
            {
                Task updateTaskDB = db.Tasks.Find(model.Id);
                updateTaskDB.Title = model.Title;
                updateTaskDB.Task_Date = model.TaskDate;
                updateTaskDB.Created_Date = DateTime.Now;
                updateTaskDB.AppointedTo = model.AppointedTo;
                updateTaskDB.AssignedBy_Id = obj.Id;
                updateTaskDB.Remark = model.Remark;
                updateTaskDB.Committee_Id = model.Committee_Id;
                updateTaskDB.Status = model.Status;
                db.Entry(updateTaskDB).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            else
            {
                Task taskDB = new Task();
                taskDB.Title = model.Title;
                taskDB.Task_Date = model.TaskDate;
                taskDB.Created_Date = DateTime.Now;
                taskDB.AppointedTo = model.AppointedTo;
                taskDB.AssignedBy_Id = obj.Id;
                taskDB.Remark = model.Remark;
                taskDB.Committee_Id = model.Committee_Id;
                taskDB.Status = model.Status;
                db.Tasks.Add(taskDB);
                db.SaveChanges();
            }
            
            Message = "Task Added";
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAgenda(TaskModel model)
        {
            if(model.AgendaId>0)
            {
                TaskAgenda updateAgendaDB = db.TaskAgendas.Find(model.AgendaId);
                updateAgendaDB.Status = model.AgendaStatus;
                db.Entry(updateAgendaDB).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                TaskAgenda agendaDB = new TaskAgenda();
                agendaDB.Task_Id = model.Id;
                agendaDB.Agenda = model.AgendaTitle;
                agendaDB.Remark = model.AgendaRemark;
                agendaDB.Status = false;
                db.TaskAgendas.Add(agendaDB);
                db.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult AssignMember(TaskModel model)
        {

            Task_Member findMember = db.Task_Member.Where(x=>x.Task_Id == model.Id && x.Member_Id== model.AssignedMemberId).FirstOrDefault();
            if (findMember==null)
            {
                Task_Member taskMember = new Task_Member();
                taskMember.Member_Id = model.AssignedMemberId;
                taskMember.Task_Id = model.Id;
                db.Task_Member.Add(taskMember);
                db.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public List<int> GetAgendaIdList(int Id)
        {
            List<TaskAgenda> vm = db.TaskAgendas.Where(x => x.Task_Id == Id).ToList();
            var vms = vm.Select(s => s.Id).ToList();
            return vms;
        }
        public List<TaskModel> GetAgendaList(int Id)
        {
            List<TaskModel> vm = db.TaskAgendas.Where(x => x.Task_Id == Id).Select(x => new TaskModel
            {
                AgendaId = x.Id,
                AgendaTitle = x.Agenda,
                AgendaRemark = x.Remark,
                AgendaStatus = x.Status
            }).ToList();
            return vm;
        }

        public List<TaskModel> GetAssignedMemberList(int Id)
        {
            List<TaskModel> vm = db.Task_Member.Where(x => x.Task_Id == Id).Select(x => new TaskModel
            {
               Id = x.Id,
               AssignedMemberName = x.Member.Name
            }).ToList();
            return vm;
        }
        public List<string> GetAgendaTitleList(int Id)
        {
            List<TaskAgenda> vm = db.TaskAgendas.Where(x => x.Task_Id == Id).ToList();
            var vms = vm.Select(s => s.Agenda).ToList();
            return vms;
        }
        public List<string> GetAgendaRemarkList(int Id)
        {
            List<TaskAgenda> vm = db.TaskAgendas.Where(x => x.Task_Id == Id).ToList();
            var vms = vm.Select(s => s.Remark).ToList();
            return vms;
        }
          public List<bool ?> GetAgendaStatusList(int Id)
        {
            List<TaskAgenda> vm = db.TaskAgendas.Where(x => x.Task_Id == Id).ToList();
            var vms = vm.Select(s => s.Status).ToList();
            return  vms;
        }
    
    }
}