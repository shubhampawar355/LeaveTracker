using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace LeaveTracker.test {
    public partial class Tester {
        [Fact]
        public void TestGetEmployee () {
            Employee king = FileHandler.GetEmployee (333);
            Assert.True (king.Name.Equals ("King Shubham"));
        }

        [Fact]
        public void TestGetAllEmployee () {
            List<Employee> all = FileHandler.GetAllEmployees ();
            Assert.Equal (12, all.Count);
        }

        [Fact]
        public void TestGetLastLeaveId () {
            int actual = FileHandler.GetLastLeaveId ();
            Assert.Equal (3, actual);
        }

        [Fact]
        public void TestGetLeave () {
            Leave leave = FileHandler.GetLeave (3);
            Assert.NotNull (leave);
        }

        [Fact]
        public void TestUpdateLeaveWorks () {

            HashSet<Leave> leaves = FileHandler.GetAllLives ();
            HashSet<Leave> leaves2 = FileHandler.GetAllLives ();
            foreach (var item in leaves) {
                if (item.LeaveId == 1)
                    item.Title = "new title";
            }
            FileHandler.UpdateLeave (leaves);
            Leave leave = FileHandler.GetLeave (1);
            Assert.Equal (leave.Title, "new title");
            FileHandler.UpdateLeave (leaves2);
        }

        [Fact]
        public void TestGetAllLeavesWorks () {
            HashSet<Leave> leaves = FileHandler.GetAllLives ();
            Assert.Equal (3, leaves.Count);
        }

        [Fact]
        public void TestGetAllLeavesForEmployee () {
            List<Leave> list = FileHandler.GetAllLeavesForEmployee ("Olive Yew");
            Assert.Equal (3, list.Count);
        }

        [Fact]
        public void TestGetAllLeavesByManagerId () {
            List<Leave> list = FileHandler.GetAllLeavesByManagerName ("Ann Chovey");
            Assert.Equal (3, list.Count);
        }

        [Fact]
        public void TestAddHeaderToDestination () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\TestingAddHeader.csv";
            FileHandler.WriteLeaveToFile ("empty line");
            string str = " ID | Creator | Manager | Title | Description | Start - Date | End - Date | Status";
            Assert.True (File.ReadAllLines (@"..\..\..\SampleInput\TestingAddHeader.csv") [0].Equals (str));
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
            File.Delete (@"..\..\..\SampleInput\TestingAddHeader.csv");
        }

        [Fact]
        public void TestWriteLeaveToFile () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\FileHandlerTestWriteLeaveToFile.csv";
            FileHandler.WriteLeaveToFile ("empty line");
            string[] lines=File.ReadAllLines (@"..\..\..\SampleInput\FileHandlerTestWriteLeaveToFile.csv");
            string str = " ID | Creator | Manager | Title | Description | Start - Date | End - Date | Status";
            Assert.True (lines[0].Equals (str));
            Assert.True (lines[lines.Length-1].Equals ("empty line"));
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
            File.Delete (@"..\..\..\SampleInput\FileHandlerTestWriteLeaveToFile.csv");
        }

    }
}