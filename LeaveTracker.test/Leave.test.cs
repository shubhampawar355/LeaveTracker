using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace LeaveTracker.test {
    public partial class Tester {
        [Fact]
        public void TestEquals () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
            Leave leave1 = new Employee (105, "Olive Yew", 1).GetMyLeavesByTitle ("eeeeeeeee");
            Leave leave2 = new Employee (105, "Olive Yew", 1).GetMyLeavesByTitle ("eeeeeeeee");
            Assert.True (leave1.Equals (leave2));
        }

        [Fact]
        public void TestGetHashCodeReturnSameHashCodeForSameId () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
            Leave leave1 = new Employee (105, "Olive Yew", 1).GetMyLeavesByTitle ("eeeeeeeee");
            Leave leave2 = new Employee (105, "Olive Yew", 1).GetMyLeavesByTitle ("eeeeeeeee");
            Assert.Equal (leave1.GetHashCode (), leave2.GetHashCode ());
        }

        [Fact]
        public void TestUpdateLeave () {
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\UpdateLeave.csv";
            Leave leave = new Employee (105, "Olive Yew", 1).GetMyLeavesByTitle ("eeeeeeeee");
            leave.Title = "My new title";
            leave.UpdateLeave ();
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\UpdateLeave.csv";
            Leave changed = new Employee (105, "Olive Yew", 1).GetMyLeavesByTitle ("My new title");
            leave.Title = "eeeeeeeee";
            leave.UpdateLeave ();
            Assert.NotNull (changed);
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
        }

        [Fact]
        public void TestWriteToFile () {
            Leave leave = new Employee (105, "Olive Yew", 1).GetMyLeavesByTitle ("eeeeeeeee");
            leave.Manager="shubham";
            File.Create (@"..\..\..\SampleInput\TestWriteToFile.csv").Close ();
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\TestWriteToFile.csv";
            leave.WriteLeaveToFile ();
            string[] lines=File.ReadAllLines(@"..\..\..\SampleInput\TestWriteToFile.csv");
            Assert.True(lines[lines.Length-1].Split('|',StringSplitOptions.RemoveEmptyEntries)[2].Equals("shubham"));
            FileHandler.LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";
        }
    }
}