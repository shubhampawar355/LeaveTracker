using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
namespace LeaveTracker.test {
    public partial class Tester {
        [Fact]
        public void TestEmployeeHasDefaultCtor () {
            Employee emp = new Employee ();
        }

        [Fact]
        public void TestEmployeeHasParametrizedCtor () {
            Employee emp = new Employee (1, "shubham", 100);
        }

        [Fact]
        public void TestCreateLeave () {
            File.Create (@"..\..\..\SampleInput\TestCreateLeave.csv").Close ();
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\TestCreateLeave.csv";
            Employee emp = new Employee (1, "shubham", 100);
            emp.CreateLeave ("For Testing", "TestCreateLeave()", DateTime.Now, DateTime.Now).WriteLeaveToFile ();
            HashSet<Leave> leaves = FileHandler.GetAllLives ();
            Leave myLeave = leaves.Last ();
            Assert.True (myLeave.Title.Equals ("For Testing"));
            Assert.True (myLeave.Description.Equals ("TestCreateLeave()"));
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
            File.Delete (@"..\..\..\SampleInput\TestCreateLeave.csv");
        }

        [Fact]
        public void TestLeaveInitialStatusSetToPending () {
            File.Create (@"..\..\..\SampleInput\TestCreateLeave.csv").Close ();
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\TestCreateLeave.csv";
            Employee emp = new Employee (1, "shubham", 100);
            emp.CreateLeave ("For Testing", "TestCreateLeave()", DateTime.Now, DateTime.Now).WriteLeaveToFile ();
            HashSet<Leave> leaves = FileHandler.GetAllLives ();
            Leave myLeave = leaves.Last ();
            Assert.True (myLeave.LeaveStatus.Equals (LeaveStatus.PENDING));
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
            File.Delete (@"..\..\..\SampleInput\TestCreateLeave.csv");
        }

        [Fact]
        public void TestGetAllLeavesApplications () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\TestGetAllLeavesApplications.csv";
            List<Leave> result = new Employee (101, "Ann Chovey", 1).GetAllLeavesApplications ();
            Assert.Equal (3, result.Count);
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
        }

        [Fact]
        public void TestLogIn () {
            Employee employee = Employee.LogIn (101);
            Assert.Equal (101, employee.EmpId);
            Assert.True (employee.Name.Equals ("Ann Chovey"));
            Assert.Equal (100, employee.ManagerId);
        }

        [Fact]
        public void TestGetAllLeavesApplicationsByStatus () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\GetAllLeavesApplicationsByStatus.csv";
            Employee employee = Employee.LogIn (101);
            List<Leave> APPROVED = employee.GetAllLeavesApplicationsByStatus (LeaveStatus.APPROVED);
            List<Leave> PENDING = employee.GetAllLeavesApplicationsByStatus (LeaveStatus.PENDING);
            List<Leave> REJECTED = employee.GetAllLeavesApplicationsByStatus (LeaveStatus.REJECTED);

            Assert.Equal (3, APPROVED.Count);
            Assert.Equal (6, PENDING.Count);
            Assert.Equal (0, REJECTED.Count);
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";

        }

        [Fact]
        public void TestGetMyLeaves () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\GetMyLeaves.csv";
            List<Leave> result = new Employee (105, "Olive Yew", 101).GetMyLeaves ();
            Assert.Equal (3, result.Count);
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
        }

        [Fact]
        public void TestGetMyLeavesByTitle () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\GetMyLeavesByTitle.csv";
            Leave leave = new Employee (105, "Olive Yew", 101).GetMyLeavesByTitle ("Mytitle");
            Assert.True (leave.Title.Equals("Mytitle"));
            Assert.True (leave.Description.Equals("eeeeeeeeeeeee"));
            Assert.True (leave.LeaveStatus.Equals(LeaveStatus.APPROVED));
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
        }

        [Fact]
        public void TestGetMyLeavesByStatus () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\GetMyLeavesByTitle.csv";
            List<Leave> result = new Employee (105, "Olive Yew", 101).GetMyLeavesByStatus (LeaveStatus.PENDING);
            Assert.Equal (2, result.Count);
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";

        }

    }
}