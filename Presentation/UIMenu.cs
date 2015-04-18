//
// FILE: UIMenu.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: All input/output from/to the user will be handeled by the methods and properties
// defined in this class.
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    /// <summary>
    /// PurposeThis class will contain meathods to display
    /// Menus to the user. That the user will then interact with
    /// calling the functional meathods of the program
    /// </summary>
    public class UIMenu
    {
        ///<VAR choice="int">the choice made by the user 
        ///as a single number</VAR>
        public int choice;


        ///<summary>
        ///Method:MenuOne
        ///Purpose:The MenuOne meathod will display the first menu
        ///the user will be interacting with
        ///Depending on the users choice a diffrent menu will be called apon
        ///Inputs:None
        ///Returns:Void
        ///</summary>
        public void MenuOne()
        {
            Console.WriteLine("Menu 1: Main Menu");
            Console.WriteLine("1. Manage EMS DBase Files");
            Console.WriteLine("2. Manage Employees");
            Console.WriteLine("3.Quit");

            choice = int.Parse(Console.ReadLine()); 
            ///To DO: 
            ///Call to Validate Number() is in range
            
            if (choice == 1)
            {
                MenuTwo();
            }

            if (choice == 2)
            {
                MenuThree();
            }

            if (choice == 3)
            {
                ///TO DO:
                ///Save and exit Program
            }

        }
    


        ///<summary>
        ///Method:MenuTwo
        ///Purpose:The MenuTwo Meathod will display the Second 
        ///menu the user will interact with
        ///This Menu will Allow the User to either Save Or 
        ///Load a pre-existing DataBase
        ///Inputs:None
        ///Returns:Void
        ///</summary>
        public void MenuTwo()
        {
            Console.WriteLine("Menu 2: File Managment Menu");
            Console.WriteLine("1. Load EMS DBase From File");
            Console.WriteLine("2. Save Employee set to EMS Dbase");
            Console.WriteLine("9. Return To Main Menu ");

            choice = int.Parse(Console.ReadLine());
            ///To DO: 
            ///Call to Validate Number() is in range
            

            if (choice == 1 )
            {
                ///TO DO:
                ///Call to readfile()
                ///Display File
            }

               if (choice == 2 ) 
            {
                ///TO DO:
                ///Call to WriteFile()
            }

               if (choice == 9)
            {
                MenuOne();
            }


        }

        ///<summary>
        ///Method:MenuThree
        ///Purpose:The MenuThree Meathod will display the third 
        ///menu the user will interact with
        ///This Menu will allow the user to Manage Employees
        ///either creating,viewing or Deleting
        ///Inputs:None
        ///Returns:Void
        ///</summary>
        public void MenuThree()
        {
            Console.WriteLine("Menu 3: Employee Managment Menu");
            Console.WriteLine("1. Display Employee Set");
            Console.WriteLine("2. Create a NEW Employee");
            Console.WriteLine("3. Modify an EXISTING Employee ");
            Console.WriteLine("4. Remove an EXISTING Employee");
            Console.WriteLine("9. Return to Main Menu");

            ///<VAR manType = "String">The users specified type of manipulation
            ///either to create or Remove an Employee</Var>
            String manType = "";

            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                ///TO DO:
                ///Call Employee Set()
                ///Traverse Employees
                ///CAll Details()
            }


            if (choice == 2)
            {
                manType = "Create";
                MenuFour(manType);
            }

            if (choice == 3)
            {
                manType = "Remove";
                MenuFour(manType);
            }

            if (choice == 4)
            {
                choice = 0;
                Console.WriteLine("Are You Sure You Would Like To Remove Employee?");
                Console.WriteLine("Enter 1 for YES.");
                Console.WriteLine("Enter 2 for NO.");
               
                choice = int.Parse(Console.ReadLine());

                if (choice==1)
                {
                    ///TO DO Call RemoveEmployee()
                    Console.WriteLine("Employee Removed Successfully.");
                    MenuThree();
                }
                if (choice == 2)
                {
                    Console.WriteLine("Employee Was NOT Removed");
                    MenuThree();
                }
                
       
            }

            if (choice == 9)
            {
                MenuOne();
            }

        }

        ///<summary>
        ///Method:MenuFour
        ///Purpose:The MenuFour Meathod will display the forth
        ///menu the user will interact with
        ///This Menu will Allow a user to Delete an existing Employee
        ///Or Create a new one. If creating an Employee the user will
        ///be prompted Appropriatly for the type of Employee being added
        ///Inputs:String,ManType
        ///Return:Void
        ///</summary>
        ///<param name="ManType">This paramater is either Create Or Delete Specifying if the user 
        ///wishes to create or delte an Employee</param>

        public void MenuFour(String ManType)
        {
            if (ManType == "Remove")
            {
                ///TO DO: Call REMOVE();
            }

                ///Menu 4
            else if (ManType == "Create")
            {
                Console.WriteLine("Menu 4: Employee Details Menu");
                Console.WriteLine("1. Specify Base Employee");
                Console.WriteLine("2. Specify Full Time Employee");
                Console.WriteLine("3. Specify Part Time Employee ");
                Console.WriteLine("4. Specify Contract Employee");
                Console.WriteLine("5. Specify Seasonal Employee");
                Console.WriteLine("9. Return to Main Menu");

                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    Console.WriteLine("Please Enter Employee Type");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee Last Name");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee First Name");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee SIN");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee Date Of Birth");

                }

                if (choice == 2)
                {
                    Console.WriteLine("Please Enter Employee Date Of Hire");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee Date Of Termination");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee Salary");
                    //Console.Readline();
                }

                if (choice == 3)
                {
                    Console.WriteLine("Please Enter Employee Date Of Hire");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee Date Of Termination");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee Hourly Rate");
                    //Console.Readline();

                }

                if (choice == 4)
                {
                    Console.WriteLine("Please Enter Employee Contract Start Date");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee Contract Stop Date");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Employee Fixed Contract Amount");
                    //Console.Readline();
                }

                if (choice == 5)
                {
                    Console.WriteLine("Please Enter Season of Work Period");
                    //Console.Readline();
                    Console.WriteLine("Please Enter Piece Pay Salary");
                    //Console.Readline();
                }

                if (choice == 9)
                {
                    MenuThree();
                }
            }
        }
    }
}

