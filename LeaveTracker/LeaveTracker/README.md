Leave tracker contains four classes as follows :
1) LeaveTracker : contains main() method. It is tester class contains all static methods.
2) Leave : It is POCO class represents leave from leaves.csv file.
3) Employee : It is POCO class represents Employee from employee.csv file. 
4)FileHandler : Provide access to both leaves.csv and employee.csv

for unit testing LeaveTracker.test testing application designed. For each method there is at least one test case. In FileHandler class there are two static fields so thats why all test cases are written in one class which is distributed in three files to avoid ambiguity.