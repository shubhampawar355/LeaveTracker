using System;
using System.Collections.Generic;

namespace LeaveTracker {
    public class LeaveTracker {
        public static Employee CurrentUser = null;

        static void Main (string[] args) {
            if (LeaveTracker.LogIn ()) {
                int choice;
                while ((choice = GetChoice()) != 0) {
                        switch (choice) 
                        {
                            case 1:
                                LeaveTracker.CreateLeave();
                                break;
                            case 2:
                                LeaveTracker.GetMyLeaves();
                                break;
                            case 3:
                                LeaveTracker.UpdateLeave();
                                break;
                            case 4:
                                LeaveTracker.SearchLeave();
                                break;
                            default:
                                Console.WriteLine("Enter valid choice");
                                break;
                        }
                }
            } else {
                Console.WriteLine ("Login failed!!!");
            }
        }

        private static bool LogIn () {
            System.Console.WriteLine ("Enter Your Employee Id");
            try 
            { 
                int empId = int.Parse(Console.ReadLine());
                CurrentUser = Employee.LogIn(empId);
            }
            catch(FormatException)
            {
                    Console.WriteLine("Invalid type of input ");
            }
            if (CurrentUser == null)
                return false;
            return true;
        }

        private static void CreateLeave () {
            Console.WriteLine ("Enter Title");
            string title = Console.ReadLine ();
            Console.WriteLine ("Enter Description");
            string description = Console.ReadLine ();
            Console.WriteLine ("Enter Start date");
            DateTime startDate = GetDate ();
            Console.WriteLine ("Enter End date");
            DateTime endDate = GetDate ();
            CurrentUser.CreateLeave (title, description, startDate, endDate).WriteLeaveToFile();
        }
 
 
        private static DateTime GetDate () {
            while (true) {
                try {
                    Console.WriteLine ("Enter Year");
                    int year = int.Parse(Console.ReadLine());
                    Console.WriteLine ("Enter Month");
                    int month = int.Parse(Console.ReadLine());
                    Console.WriteLine ("Enter day");
                    int day = int.Parse(Console.ReadLine());
                    return new DateTime (year, month, day);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid type of input ");
                }
                catch (Exception) {
                    Console.WriteLine ("Please Enter correct date values");
                }
            }
        }

        private static void GetMyLeaves () {
            List<Leave> leaves = CurrentUser.GetMyLeaves ();
            if (leaves.Count == 0)
                Console.WriteLine ("You don't have any leaves!");
            else {
                foreach (Leave leave in leaves) {
                    Console.WriteLine (leave);
                }
            }
        }

        private static void UpdateLeave () {
            try
            {
                List<Leave> leaves = PrintLevesForUpdation();
                if (leaves.Count == 0)
                    return;
                Console.WriteLine("Enter LeaveId you want to update");
                int leaveId = int.Parse(Console.ReadLine());
                Leave Updateleave = FileHandler.GetLeave(leaveId);
                if (Updateleave != null)
                {
                    switch (UpdateLeaveMenuListOne())
                    {
                        case 1:
                            Updateleave.LeaveStatus = LeaveStatus.PENDING;
                            break;
                        case 2:
                            Updateleave.LeaveStatus = LeaveStatus.APPROVED;
                            break;
                        case 3:
                            Updateleave.LeaveStatus = LeaveStatus.REJECTED;
                            break;
                    }
                    Updateleave.UpdateLeave();
                }
            else
                Console.WriteLine("Record not found for given leaveId! Enter valid leaveId");
            }
            catch (FormatException)
            {
                Console.WriteLine("Input type is not valid! Must be a number");
            }
        }
        
        private static int UpdateLeaveMenuListOne()
        {
            try
            {
                Console.WriteLine("1.set status as Pending");
                Console.WriteLine("2.set status as Approved");
                Console.WriteLine("3.set status as Rejected");
                return int.Parse(Console.ReadLine()); 
            }
            catch (FormatException) 
            {
                Console.WriteLine("Input type is not valid! Must be a number");
                return 0;
            }
            
        }
        private static int UpdateLeaveMenuListSecond()
        {
            try
            {
                Console.WriteLine("0.Get all leaves");
                Console.WriteLine("1.Get leaves with Pending status" );
                Console.WriteLine("2.Get leaves with Approved status");
                Console.WriteLine("3.Get leaves with Rejected status");
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Input type is not valid! Must be a number");
                return 0;
            }

        }

        public static List<Leave> PrintLevesForUpdation()
        {
            int choice = UpdateLeaveMenuListSecond();
            List<Leave> leaves = CurrentUser.GetAllLeavesApplications();
            if (choice == 1)
                leaves = CurrentUser.GetAllLeavesApplicationsByStatus(LeaveStatus.PENDING);
            else if (choice == 2)
                leaves = CurrentUser.GetAllLeavesApplicationsByStatus(LeaveStatus.APPROVED);
            else if (choice == 3)
                leaves = CurrentUser.GetAllLeavesApplicationsByStatus(LeaveStatus.REJECTED);
            PrintLeaves(leaves);
            return leaves;
        }

        private static void PrintLeaves(List<Leave> leaves)
        {
            if (leaves.Count == 0)
                Console.WriteLine("You don't have any leave Application!");
            else
            {
                foreach (Leave leave in leaves)
                {
                    Console.WriteLine(leave);
                }
            }
        }

        private static void SearchLeave()
        {
            int choice;
            while ((choice= GetChoiceForSearchLeave())!=0)
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Leave Title");
                        Leave leave = CurrentUser.GetMyLeavesByTitle(Console.ReadLine());
                        if (leave != null)
                            Console.WriteLine(leave.ToString());
                        else
                            Console.WriteLine("No leaves with current title");
                        break;
                    case 2:
                        Console.WriteLine("Enter Leave Status");
                        try
                        {
                            LeaveStatus status = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), Console.ReadLine(),true);
                            List<Leave> leaves = CurrentUser.GetMyLeavesByStatus(status);
                            PrintLeaves(leaves);
                        }
                        catch(Exception)
                        {
                            Console.WriteLine("invalid value!!! Enter value from Approved,Rejected,Pending");
                        }
                        break;
                    default:
                        Console.WriteLine("Enter valid Choice");
                        break;
                }
            }
        }
        

        private static int GetChoiceForSearchLeave () {
            try
            {
                Console.WriteLine("0.Exit");
                Console.WriteLine("1.By Leave Title");
                Console.WriteLine("2.By Leave Status");
                return int.Parse(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Invalid Input type! Must be a number");
                return 0;
            }
        }
        
        
        private static int GetChoice () {
            try
            {
                Console.WriteLine("0.Exit");
                Console.WriteLine("1.Create Leave");
                Console.WriteLine("2.List my Leaves");
                Console.WriteLine("3.Update leaves");
                Console.WriteLine("4.Search Leave");
                Console.WriteLine("Enter your choice :");
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input type! Must be a number");
                return 0;
            }
        }
    }
}