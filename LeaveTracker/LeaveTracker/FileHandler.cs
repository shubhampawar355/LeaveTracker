using System;
using System.Collections.Generic;
using System.IO;

namespace LeaveTracker {
   public class FileHandler {
        public static string EmployeeCSVPath = @"..\..\..\SampleInput\employees.csv";
        public static string LeaveCSVPath = @"..\..\..\SampleInput\leaves.csv";

        private static Employee GetEmployee (string line) {
            string[] empData = line.Split (',',StringSplitOptions.RemoveEmptyEntries);
            try {
                return new Employee (int.Parse (empData[0]), empData[1], int.Parse (empData[2]));
            } catch (IndexOutOfRangeException) {
                return new Employee (int.Parse (empData[0]), empData[1], null);
            }
        }

        private static Leave GetLeave (string line) {
            string[] leaveData = line.Split ('|', StringSplitOptions.RemoveEmptyEntries);
            string[] dateSplit = leaveData[5].Split ('-', StringSplitOptions.RemoveEmptyEntries);
            DateTime startDate = new DateTime (int.Parse (dateSplit[2]), int.Parse (dateSplit[1]), int.Parse (dateSplit[0]));
            dateSplit = leaveData[6].Split ('-');
            DateTime endDate = new DateTime (int.Parse (dateSplit[2]), int.Parse (dateSplit[1]), int.Parse (dateSplit[0]));
            LeaveStatus leaveStatus = (LeaveStatus) Enum.Parse (typeof (LeaveStatus), leaveData[7],true);
            return new Leave (int.Parse (leaveData[0]), leaveData[1], leaveData[2], leaveData[3], leaveData[4], startDate, endDate, leaveStatus);
        }

        public static Employee GetEmployee(int empId)
        {
            List<Employee> list=GetAllEmployees();
            foreach (Employee emp in list)
            {
                if(emp.EmpId==empId)
                    return emp;
            }
            return null;
        }
        public static List<Employee> GetAllEmployees () {
            List<Employee> list = new List<Employee> ();
            string[] lines = File.ReadAllLines (EmployeeCSVPath);
            for (int i = 1; i < lines.Length; i++) {
                list.Add (GetEmployee (lines[i]));
            }
            return list;
        }

        public static int GetLastLeaveId()
        {
            string[] lines = File.ReadAllLines (LeaveCSVPath);
            if(lines.Length>1)
                return int.Parse(lines[lines.Length-1].Split('|',StringSplitOptions.RemoveEmptyEntries)[0]);
            return 0;
        }

        public static Leave GetLeave(int leaveId)
        {
            HashSet<Leave> leaves= GetAllLives();
            foreach (Leave leave in leaves)
            {
                if(leave.LeaveId==leaveId)
                    return leave;
            }
            return null;
        }
        public static void UpdateLeave(HashSet<Leave> leaves)
        {
            File.Delete(LeaveCSVPath);
            List<string> list = new List<string>();
            foreach (var leave in leaves)
            {
                list.Add(leave.ToString());
            }
            AddHeaderToDestination();
            File.AppendAllLines(LeaveCSVPath,list);
        }
        public static HashSet<Leave> GetAllLives()
        {
            HashSet<Leave> leaves=new HashSet<Leave>();
            string[] lines = File.ReadAllLines (LeaveCSVPath);
            for (int i = 1; i < lines.Length; i++) {
                leaves.Add(GetLeave(lines[i]));
            }
            return leaves;
        }
        public static List<Leave> GetAllLeavesForEmployee (string empName) {
            List<Leave> list = new List<Leave> ();
            string[] lines = File.ReadAllLines (LeaveCSVPath);
            for (int i = 1; i < lines.Length; i++) {
                Leave leave = GetLeave (lines[i]);
                if (leave.Creator.Equals (empName)) {
                    list.Add (leave);
                }
            }
            return list;
        }
        public static List<Leave> GetAllLeavesByManagerName (string managerName) {
            List<Leave> list = new List<Leave> ();
            string[] lines = File.ReadAllLines (LeaveCSVPath);
            for (int i = 1; i < lines.Length; i++) {
                Leave leave = GetLeave (lines[i]);
                if (leave.Manager.Equals (managerName)) {
                    list.Add (leave);
                }
            }
            return list;
        }

        public static void AddHeaderToDestination()
        { 
            string str = " ID | Creator | Manager | Title | Description | Start - Date | End - Date | Status\n";
            if (!File.Exists(LeaveCSVPath))
                File.Create(LeaveCSVPath).Close();
            if (new FileInfo(LeaveCSVPath).Length == 0)
                File.AppendAllText(LeaveCSVPath, str);
        }
        public static void WriteLeaveToFile(string leave)
        {
            AddHeaderToDestination();
            string[] arr = new string[] { leave };
            File.AppendAllLines(LeaveCSVPath,arr);
        }
    }
}