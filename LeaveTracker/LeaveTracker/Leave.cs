using System;
using System.Collections.Generic;

namespace LeaveTracker {
    public enum LeaveStatus {
        PENDING = 0,
        APPROVED = 1,
        REJECTED = -1
    }
    public class Leave : IEquatable<Leave> {
        private static int LastLeaveId;
        public int LeaveId { get; set; }
        public string Creator { get; set; }
        public string Manager { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus LeaveStatus { get; set; }

        static Leave () {
            LastLeaveId = FileHandler.GetLastLeaveId ();
        }
        public Leave (string Creator, string manager, string title, string description, DateTime startDate, DateTime endDate) {
            this.LeaveId = ++LastLeaveId;
            this.Creator = Creator;
            this.Manager = manager;
            this.Title = title;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.LeaveStatus = LeaveStatus.PENDING;
        }

        public Leave (int leaveId, string Creator, string manager, string title, string description,
            DateTime startDate, DateTime endDate, LeaveStatus leaveStatus) {
            this.LeaveId = leaveId;
            this.Creator = Creator;
            this.Manager = manager;
            this.Title = title;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.LeaveStatus = leaveStatus;
        }

        public bool Equals (Leave other) {
            return this.LeaveId == other.LeaveId;
        }
        
        public override int GetHashCode () {
            return this.LeaveId.GetHashCode ();
        }
        public void UpdateLeave () {
            HashSet<Leave> leaves = FileHandler.GetAllLives ();
            leaves.Remove (this);
            leaves.Add (this);
            FileHandler.UpdateLeave (leaves);
        }
        public void WriteLeaveToFile () {
            FileHandler.WriteLeaveToFile (this.ToString ());
        }
        public override string ToString () {
            return (LeaveId + "|" + Creator + "|" + Manager + "|" + Title + "|" + Description + "|" +
                StartDate.ToString ("dd-MM-yyyy") + "|" + EndDate.ToString ("dd-MM-yyyy") + "|" + LeaveStatus.ToString ());
        }
    }
}