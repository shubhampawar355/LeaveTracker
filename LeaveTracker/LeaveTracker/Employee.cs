using System.Collections.Generic;
using System;
using System.Linq;
namespace LeaveTracker {
    public class Employee {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public int? ManagerId { get; set; }

        public Employee () { }
        public Employee (int empId, string name, int? managerId) {
            this.EmpId = empId;
            this.Name = name;
            this.ManagerId = managerId;
        }

        public Leave CreateLeave (string title, string description, DateTime startDate, DateTime endDate) {
            return new Leave(Name, FileHandler.GetEmployee((int)ManagerId).Name, title, description, startDate, endDate);
        }

        public List<Leave> GetAllLeavesApplications () {
            return FileHandler.GetAllLeavesByManagerName (this.Name);
        }

        public static Employee LogIn(int empId)
        {
            return FileHandler.GetEmployee(empId);
        }
        public List<Leave> GetAllLeavesApplicationsByStatus (LeaveStatus status) {
            List<Leave> leaves = this.GetAllLeavesApplications ();
            List<Leave> list = new List<Leave> ();
            foreach (Leave leave in leaves) {
                if (leave.LeaveStatus == status)
                    list.Add (leave);
            }
            return list;
        }

        public List<Leave> GetMyLeaves () {
            return FileHandler.GetAllLeavesForEmployee (this.Name);
        }
        public Leave GetMyLeavesByTitle (string title) {
            List<Leave> leaves = GetMyLeaves ();
            foreach (Leave leave in leaves) {
                if (leave.Title.Equals(title))
                    return leave;
            }
            return null;
        }
        public List<Leave> GetMyLeavesByStatus (LeaveStatus status) {
            List<Leave> leaves = GetMyLeaves ();
            List<Leave> list = new List<Leave> ();
            foreach (Leave leave in leaves) {
                if (leave.LeaveStatus == status)
                    list.Add (leave);
            }
            return list;
        }
        
    }
}