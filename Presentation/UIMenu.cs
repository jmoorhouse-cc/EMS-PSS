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
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Reflection;
using Supporting;
using TheCompany;
using AllEmployees;


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
        ConsoleKeyInfo choice = new ConsoleKeyInfo();
        //string dBaseFile = Path.Combine(Directory.GetCurrentDirectory(), "DBase", "DB.csv");
        FileIO fileClass = new FileIO();
        Container company = new Container();
        Validation val = new Validation();
        Logging log = new Logging();

        ContractEmployee ctEmp = new ContractEmployee();
        FulltimeEmployee ftEmp = new FulltimeEmployee();
        SeasonalEmployee snEmp = new SeasonalEmployee();
        ParttimeEmployee ptEmp = new ParttimeEmployee();
        Employee baseEmp = new Employee();


        /// <summary>
        /// Method: MenuOne
        /// This method will display the main menu to the user and allow input from the user to 
        /// manipulate it.
        /// </summary>
        public void MenuOne()
        {
            ///To DO: 
            ///Call to Validate Number() is in range
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu 1: Main Menu");
                Console.WriteLine("1. Manage EMS DBase Files");
                Console.WriteLine("2. Manage Employees");
                Console.WriteLine("9. Quit");

                choice = Console.ReadKey();
                if (choice.KeyChar == '1')  //Manage EMS DB File
                {
                    MenuTwo();
                    //break;
                }
                else if (choice.KeyChar == '2') //Manage Employees
                {
                    MenuThree();
                    //break;
                }
                else if (choice.KeyChar == '9') //Quit
                {
                    Console.Clear();
                    Console.WriteLine("Would you like to quit? ('y')");
                    choice = Console.ReadKey();
                    if (choice.KeyChar == 'y')
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Method: MenuTwo
        /// This method will display the File managment menu to the user and allow input from the user to 
        /// manipulate it.
        /// </summary>
        public void MenuTwo()
        {
            Console.Clear();
            Console.WriteLine("Menu 2: File Managment Menu");
            Console.WriteLine("1. Load EMS DBase From File");
            Console.WriteLine("2. Save Employee set to EMS Dbase");
            Console.WriteLine("9. Return To Main Menu ");
            choice = Console.ReadKey();
            if (choice.KeyChar == '1')
            {                    
                ///Display File List (option to feed the file name to loadcontainer??)
                if(!company.LoadContainer())
                {
                    Console.WriteLine("\n\n>>>>>>>>>>> One or more of the database entries was corrupt. Load anyway? 'y' or 'n'");

                    while (true)
                    {
                        choice = Console.ReadKey();
                        if (choice.KeyChar == 'y')
                        {
                            break;
                        }
                        else if (choice.KeyChar == 'n')
                        {
                            company.container.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nDatabase Successfully loaded");
                    Console.WriteLine("\n\nPress Any Key to Return to Previous Menu");
                    Console.ReadKey();
                }
                //Console.Clear();
            }
                ///Will ensure user wants to save the container to a file,
                ///and apon proper input will save the container to database file.
            else if (choice.KeyChar == '2')
            {
                if (company.container.Count == 0)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n[ ! ] The Container is Empty and DB File will be overwritten with EMPTY File");
                        Console.WriteLine("\tAre You Sure You Would Like To Save the Current Container?");
                        Console.WriteLine("\tYES[y] No[n]");
                        choice = Console.ReadKey();
                        if (choice.KeyChar == 'y')
                        {
                            if (company.SaveContainer())
                            {
                                Console.Clear();
                                Console.WriteLine("\nDatabase Successfully Saved");
                                Console.WriteLine("\n\nPress Any Key to Return to Previous Menu");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\nZERO entries Saved");
                                Console.WriteLine("\n\nPress Any Key to Return to Previous Menu");
                                Console.ReadKey();
                            }
                            break;
                        }
                        else if (choice.KeyChar == 'n')
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (company.SaveContainer())
                    {
                        Console.Clear();
                        Console.WriteLine("\nDatabase Successfully Saved");
                        Console.WriteLine("\n\nPress Any Key to Return to Previous Menu");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nZERO entries Saved");
                        Console.WriteLine("\n\nPress Any Key to Return to Previous Menu");
                        Console.ReadKey();
                    }
                }
                ///Call to WriteFile()

            }

            else if (choice.KeyChar == '9')
            {
                MenuOne();
            }

        }

        /// <summary>
        /// Method: AskForEmployeeType
        /// This method will display a list of emloyee types to the user
        /// and allow user to specify which type they would like to create
        /// </summary>
        public String AskForEmployeeType()
        {
            String employeeType = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please select employee type:");
                Console.WriteLine("\t1. Full Time Employee");
                Console.WriteLine("\t2. Part Time Employee");
                Console.WriteLine("\t3. Contract Employee");
                Console.WriteLine("\t4. Seasonal Employee");
                choice = Console.ReadKey();
                ///Sets employee type based on users choice
                switch (choice.KeyChar)
                {
                    case '1':
                        employeeType = "FT";
                        break;
                    case '2':
                        employeeType = "PT";
                        break;
                    case '3':
                        employeeType = "CT";
                        break;
                    case '4':
                        employeeType = "SN";
                        break;
                }
                if ((choice.KeyChar == '1') || (choice.KeyChar == '2') || (choice.KeyChar == '3') || (choice.KeyChar == '4'))
                {
                    break;
                }
            }
            return employeeType;
        }

        /// <summary>
        /// Method: MenuThree
        /// This method will display the Employee managment menu to the user
        /// and allow the user to decide what they would like to do
        /// either create, modify or remove an employee from the container
        /// </summary>
        public void MenuThree()
        {
            ///<VAR manType = "String">The users specified type of manipulation
            ///either to create or Remove an Employee</Var>
            String input = "";
            Employee e = new Employee();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu 3: Employee Managment Menu");
                Console.WriteLine("1. Display Employee Set");
                Console.WriteLine("2. Create a NEW Employee");
                Console.WriteLine("3. Modify an EXISTING Employee ");
                Console.WriteLine("4. Remove an EXISTING Employee");
                Console.WriteLine("9. Return to Main Menu");

                choice = Console.ReadKey();
                if (choice.KeyChar == '1')
                {
                    int count = company.Display();
                    Console.WriteLine("\n\n>>>>>>>>>>>> " +count + " Employees stored successfully");
                    Console.WriteLine("Press Any Key to Go to Previous Menu");
                    Console.ReadKey();
                }
                else if (choice.KeyChar == '2')
                {
                    MenuFourToCreate(AskForEmployeeType(), new Object(), false);
                    //break;
                }

                else if (choice.KeyChar == '3')
                {
                    MenuFourToModify();
                    break;
                }
                    ///If the user choses to remove an employee Will 
                    ///check to ensure employee exists and double check with
                    ///user before removing the selected employee
                else if (choice.KeyChar == '4')
                {
                    Console.Clear();
                    Console.WriteLine("Please enter SIN/BN of Employee to delete:");
                    input = Console.ReadLine();
                    input.Replace(" ", "");
                    foreach(Object o in company.container)
                    {
                        e = (Employee)o;
                        if(e.GetSIN() == input)
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Found:");
                                e.Details(false);
                                Console.WriteLine("Are You Sure You Would Like To Remove Employee?");
                                Console.WriteLine("Press 1 for YES.");
                                Console.WriteLine("Press 2 for NO.");
                                choice = Console.ReadKey();
                                if (choice.KeyChar == '1')
                                {
                                    company.container.Remove(o);
                                    Console.WriteLine("Employee Removed Successfully. Press any key to continue:");
                                    Console.ReadKey();
                                    break;
                                }
                                else if (choice.KeyChar == '2')
                                {
                                    Console.WriteLine("Employee Was NOT Removed. Press any key to continue:");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                else if (choice.KeyChar == '9')
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Method:PrintMenuFourOptions
        /// Will display the Employee detail menu and
        /// allow user to input fields of an employee based on its type
        ///<VAR type="String">the choice made by the user 
        ///of what type of employee will be added</VAR>
        /// </summary>
        public void PrintMenuFourOptions(String type)
        {
            Console.Clear();
            Console.WriteLine("Menu 4: Employee Details Menu");
            Console.WriteLine("1. Specify Base Employee Details");

            switch (type)
            {
                    ///For full time employee
                case "FT":
                    Console.WriteLine("2. Set Full Time Employee Date Of Hire");
                    Console.WriteLine("3. Set Full Time Employee Date Of Termination");
                    Console.WriteLine("4. Set Full Time Employee Salary");
                    break;
                    ///for Part time employee
                case "PT":
                    Console.WriteLine("2. Set Part Time Employee Date Of Hire");
                    Console.WriteLine("3. Set Part Time Employee Date Of Termination");
                    Console.WriteLine("4. Set Part Time Employee Hourly rate");
                    break;
                    ///for Contract employee
                case "CT":
                    Console.WriteLine("2. Set Contract Employee Contract Start Date");
                    Console.WriteLine("3. Set Contract Employee Contract Stop Date");
                    Console.WriteLine("4. Set Contract Employee Fixed Contract Amount");
                    break;
                    ///For Seasonal employee
                case "SN":
                    Console.WriteLine("2. Set Seasonal Employee Season");
                    Console.WriteLine("3. Set Seasonal Employee Piece Pay");
                    break;
            }
            ///Displayed for all employee types
            Console.WriteLine("");
            Console.WriteLine("6. Preview Employee Data Entered So Far");
            Console.WriteLine("7. Discard Employee Data Entered So Far");
            Console.WriteLine("8. Save Employee");
            Console.WriteLine("9. Return to Employee Management Menu");
        }

        /// <summary>
        /// Method: SpecifyNameOrRateOrDateOrSIN()
        /// Will take either name, or rate or date or sin to serch for employee
        /// </summary>
        public String SpecifyNameOrRateOrDateOrSIN(String what, String question)
        {
            String input = "";
            Console.Clear();
            Console.WriteLine(question);
            Console.Write("\t>> " + what + ":\t");
            input = Console.ReadLine();
            input = input.Trim();
            return input;
        }

        /// <summary>
        /// Method: SpecifySeason()
        /// Will Diaply the options for what season the seasonal employee is hired for
        /// </summary>
        public String SpecifySeason()
        {
            String input = "";
            while (true)
            {
                 ///User can select wither Winter, Summer, Spring or Fall
                Console.Clear();
                Console.WriteLine("Specify Season for the Contract Employee");
                Console.WriteLine("\t1. Winter");
                Console.WriteLine("\t2. Spring");
                Console.WriteLine("\t3. Summer");
                Console.WriteLine("\t4. Fall");
                Console.WriteLine("\t5. BLANK");
                char c = Console.ReadKey().KeyChar;
                switch (c)
                {
                    case '1':
                        input = "WINTER";
                        break;
                    case '2':
                        input = "SPRING";
                        break;
                    case '3':
                        input = "SUMMER";
                        break;
                    case '4':
                        input = "FALL";
                        break;
                    case '5':
                        input = "";
                        break;
                }
                if ((choice.KeyChar == '1') || (choice.KeyChar == '2') ||
                    (choice.KeyChar == '3') || (choice.KeyChar == '4') || (choice.KeyChar == '5'))
                {
                    break;
                }
            }
            return input;

        }

        /// <summary>
        /// Method: SpecifiyBaseEmployee()
        /// Will Diaply the base atributes of all employees
        ///<VAR type="String">the choice made by the user 
        ///of what type of employee will be added</VAR>
        /// </summary>
        public Employee SpecifiyBaseEmployee(String type)
        {
            String fName, lName, dob, SIN;
            //If not a contract employee has been selected
            if (type != "CT")
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Menu 4.1: Specify Base Employee Details");
                    Console.WriteLine("1. Set First Name");
                    Console.WriteLine("2. Set Last Name");
                    Console.WriteLine("3. Set Date Of Birth");
                    Console.WriteLine("4. Set Social Insurance Number");
                    Console.WriteLine("5. Go Back to Employee Details Menu");
                    choice = Console.ReadKey();
                    ///For option one, allows entry of name then verifys it
                    if (choice.KeyChar == '1')
                    {
                        fName = SpecifyNameOrRateOrDateOrSIN("First Name", "Please Enter First Name of " + type);
                        Console.WriteLine("You wrote " + fName);
                        if (baseEmp.SetFirstName(fName))
                        {
                            Console.WriteLine("First Name Successfully Set to \"" + baseEmp.GetFirstName() + "\"");
                        }
                        else
                        {
                            Console.WriteLine("Failed to Set First Name to \"" + fName + "\"; Remain as \"" + baseEmp.GetFirstName() + "\"");
                        }
                    }
                        ///For option two allows entry of last name then verifys it
                    else if (choice.KeyChar == '2')
                    {
                        lName = SpecifyNameOrRateOrDateOrSIN("Last Name", "Please Enter Last Name of " + type);
                        if (baseEmp.SetLastName(lName))
                        {
                            Console.WriteLine("Last Name Successfully Set to \"" + baseEmp.GetLastName() + "\"");
                        }
                        else
                        {
                            Console.WriteLine("Failed to Set Last Name to \"" + lName + "\"; Remain as \"" + baseEmp.GetLastName() + "\"");
                        }
                    }
                        ///For option three allows entry of date of birth and verifys it
                    else if (choice.KeyChar == '3')
                    {
                        dob = SpecifyNameOrRateOrDateOrSIN("Date of Birth", "Please Enter Date of Birth of " + type);
                        if (baseEmp.SetDOB(dob))
                        {
                            if (dob == "N/A")
                            {
                                Console.WriteLine("Date of Birth Successfully Set to \"N/A\"");
                            }
                            else
                            {
                                Console.WriteLine("Date of Birth Successfully Set to \"" + ((DateTime)baseEmp.GetDOB()).ToString("yyyy-MM-dd") + "\"");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to Set Date of Birth to \"" + dob + "\"; Remain as \""
                                + (baseEmp.GetDOB().HasValue ? ((DateTime)baseEmp.GetDOB()).ToString("yyyy-MM-dd") : "N/A") + "\"");
                        }
                    }
                        ///For option 4 allows entry of Scocial insurance number and verifys it
                    else if (choice.KeyChar == '4')
                    {
                        SIN = SpecifyNameOrRateOrDateOrSIN("Social Insurance Number", "Please Enter SIN number of " + type);
                        if (baseEmp.SetSIN(SIN))
                        {
                            Console.WriteLine("SIN Number Successfully Set to \"" + baseEmp.GetSIN() + "\"");
                        }
                        else
                        {
                            Console.WriteLine("Failed to Set SIN Number to \"" + SIN + "\"; Remain as \"" + baseEmp.GetSIN() + "\"");

                        }
                    }
                        ///For option 5 returns to employee details menu
                    else if (choice.KeyChar == '5')
                    {
                        break;
                    }

                    if ((choice.KeyChar == '1') || (choice.KeyChar == '2') || (choice.KeyChar == '3') || (choice.KeyChar == '4') || (choice.KeyChar == '5'))
                    {
                        baseEmp.Details(false);
                        Console.WriteLine("Press Any Key to Go Back to Previous Menu and Make Other Changes");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                ///For contrat employee shows special attributes
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Menu 4.1: Specify Base Employee Details");
                    Console.WriteLine("2. Set Business Name");
                    Console.WriteLine("3. Set Date Of Incorporation");
                    Console.WriteLine("4. Set Business Number");
                    Console.WriteLine("5. Go Back to Employee Details Menu");
                    choice = Console.ReadKey();
                    ///For option two allows entry of buissness name and verifys it
                    if (choice.KeyChar == '2')
                    {
                        lName = SpecifyNameOrRateOrDateOrSIN("Business Name", "Please Enter Business Name of " + type);
                        if (baseEmp.SetLastName(lName))
                        {
                            Console.WriteLine("Business Name Successfully Set to \"" + baseEmp.GetLastName() + "\"");
                        }
                        else
                        {
                            Console.WriteLine("Failed to Set Business Name to \"" + lName + "\"; Remain as \"" + baseEmp.GetLastName() + "\"");
                        }
                    }
                        ///For choice three Takes date of incorporation and verifys it
                    else if (choice.KeyChar == '3')
                    {
                        dob = SpecifyNameOrRateOrDateOrSIN("Date of Incorporation", "Please Enter Date of Incorporation of " + type);
                        if (baseEmp.SetDOB(dob))
                        {
                            if (dob == "N/A")
                            {
                                Console.WriteLine("Date of incorporation Successfully Set to \"N/A\"");
                            }
                            else
                            {
                                Console.WriteLine("Date of Incorporation Successfully Set to \"" + ((DateTime)baseEmp.GetDOB()).ToString("yyyy-MM-dd") + "\"");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to Set Date of Incorporation to \"" + dob + "\"; Remain as \""
                                + (baseEmp.GetDOB().HasValue ? ((DateTime)baseEmp.GetDOB()).ToString("yyyy-MM-dd") : "N/A") + "\"");
                        }
                    }
                        ///for option four allows entry of buissness number and verifys it
                    else if (choice.KeyChar == '4')
                    {
                        SIN = SpecifyNameOrRateOrDateOrSIN("Business Number", "Please Enter BN number of " + type);
                        if (ctEmp.validateBusinessNumber(SIN, (DateTime)baseEmp.GetDOB()))
                        {
                            baseEmp.SetSIN(SIN);
                            Console.WriteLine("BN Number Successfully Set to \"" + baseEmp.GetSIN() + "\"");
                        }
                        else
                        {
                            Console.WriteLine("Failed to Set BN Number to \"" + SIN + "\"; Remain as \"" + baseEmp.GetSIN() + "\"");

                        }
                    }
                        ///For entry five returns to previous menu
                    else if (choice.KeyChar == '5')
                    {
                        break;
                    }

                    if ((choice.KeyChar == '2') || (choice.KeyChar == '3') || (choice.KeyChar == '4') || (choice.KeyChar == '5'))
                    {
                        Console.WriteLine(baseEmp.ConsoleDetails("CT"));
                        Console.WriteLine("Press Any Key to Go Back to Previous Menu and Make Other Changes");
                        Console.ReadKey();
                    }
                }
            }
            return baseEmp;
        }


        /// <summary>
        /// Method: MenuFourToCreate()
        /// Creates instance of a new employee and sets the attributs to what the user has previously defined
        ///<VAR shortType="String">the choice made by the user 
        ///of what type of employee will be added</VAR>
        ///<VAR toDelete="Object">the choice made by the user 
        ///of what type of employee will be deleted</VAR>
        ///<VAR modify="Bool">A true of false check to see if employee will be
        ///Deleted or modify</VAR>
        /// </summary>
        public void MenuFourToCreate(String shortType, Object toDelete, bool modify)
        {
            if (!modify)
            {
                ftEmp = new FulltimeEmployee();
                ptEmp = new ParttimeEmployee();
                snEmp = new SeasonalEmployee();
                ctEmp = new ContractEmployee();
            }
            while (true)
            {
                //Shows the options for menu four
                PrintMenuFourOptions(shortType);
                
                choice = Console.ReadKey();
                if (choice.KeyChar == '1')
                {
                    baseEmp = SpecifiyBaseEmployee(shortType);
                    ftEmp.SetFirstName(baseEmp.GetFirstName());
                    ptEmp.SetFirstName(baseEmp.GetFirstName());
                    snEmp.SetFirstName(baseEmp.GetFirstName());

                    ftEmp.SetLastName(baseEmp.GetLastName());
                    ptEmp.SetLastName(baseEmp.GetLastName());
                    snEmp.SetLastName(baseEmp.GetLastName());
                    ctEmp.SetLastName(baseEmp.GetLastName());

                    ftEmp.SetDOB(baseEmp.GetDOB());
                    ptEmp.SetDOB(baseEmp.GetDOB());
                    snEmp.SetDOB(baseEmp.GetDOB());
                    ctEmp.SetDOB(baseEmp.GetDOB());

                    ftEmp.SetSIN(baseEmp.GetSIN());
                    ptEmp.SetSIN(baseEmp.GetSIN());
                    snEmp.SetSIN(baseEmp.GetSIN());
                    ctEmp.SetSIN(baseEmp.GetSIN());
                }
                else
                {
                    #region FULLTIME
                    if (shortType == "FT")
                    {
                        String type = "Full Time";
                        String hDateStr, tDateStr, salaryStr;
                        if (choice.KeyChar == '2')
                        {
                            hDateStr = SpecifyNameOrRateOrDateOrSIN("Hiring Date", "Please Enter Hiring Date of " + type);
                            if (ftEmp.SetDateOfHire(hDateStr))
                            {
                                if (hDateStr == "N/A")
                                {
                                    Console.WriteLine("Hiring Date Successfully Set to \"N/A\"");
                                }
                                else
                                {
                                    Console.WriteLine("Hiring Date Successfully Set to \"" + ((DateTime)ftEmp.GetDateOfHire()).ToString("yyyy-MM-dd") + "\"");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Hiring Date to \"" + hDateStr + "\"; Remain as \""
                                     + (ftEmp.GetDateOfHire().HasValue ? ((DateTime)ftEmp.GetDateOfHire()).ToString("yyyy-MM-dd") : "N/A") + "\"");
                            }
                        }
                        else if (choice.KeyChar == '3')
                        {
                            tDateStr = SpecifyNameOrRateOrDateOrSIN("Termination Date", "Please Enter Termination Date of " + type);
                            if (ftEmp.SetDateOfTermination(tDateStr))
                            {
                                if (tDateStr == "N/A")
                                {
                                    Console.WriteLine("Termination Date Successfully Set to \"N/A\"");
                                }
                                else
                                {
                                    Console.WriteLine("Termination Date Successfully Set to \"" + ((DateTime)ftEmp.GetDateOfTermination()).ToString("yyyy-MM-dd") + "\"");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Termination Date to \"" + tDateStr + "\"; Remain as \""
                                     + (ftEmp.GetDateOfTermination().HasValue ? ((DateTime)ftEmp.GetDateOfTermination()).ToString("yyyy-MM-dd") : "N/A") + "\"");
                            }
                        }
                        else if (choice.KeyChar == '4')
                        {
                            salaryStr = SpecifyNameOrRateOrDateOrSIN("Salary", "Please Enter Salary of " + type);
                            if (ftEmp.SetSalary(salaryStr))
                            {
                                Console.WriteLine("Salary Successfully Set to \"" + ftEmp.GetSalary() + "\"");
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Salary to \"" + salaryStr + "\"; Remain as \"" + ftEmp.GetSalary() + "\"");
                            }
                        }
                        else if (choice.KeyChar == '6')
                        {
                            Console.Clear();
                            ftEmp.Details(true);
                        }
                        else if (choice.KeyChar == '7')
                        {
                            Console.Clear();
                            Console.WriteLine("Are you sure you want to discard data entered for this employee? YES[y] No[any_key]");
                            ftEmp.Details(false);
                            ConsoleKeyInfo c = new ConsoleKeyInfo();
                            c = Console.ReadKey();
                            if (c.KeyChar == 'y')
                            {
                                Console.Clear();
                                ftEmp = new FulltimeEmployee();
                                ptEmp = new ParttimeEmployee();
                                ctEmp = new ContractEmployee();
                                snEmp = new SeasonalEmployee();
                                Console.WriteLine(type + "Employee data has been successfully reset");
                                ftEmp.Details(false);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine(type + "Employee data has NOT been reset");
                                ftEmp.Details(false);
                            }
                        }
                        else if (choice.KeyChar == '8')
                        {
                            Employee e = new Employee();
                            bool duplicate = false;
                            if ((ftEmp.Validate()).Length <= 0)
                            {
                                foreach (Object o in company.container)
                                {
                                    e = (Employee)o;
                                    if (e.GetSIN() == ftEmp.GetSIN())
                                    {
                                        company.container.Remove(e);
                                        break;
                                    }
                                }
                                if (duplicate == false)
                                {
                                    company.container.Remove(toDelete);
                                    company.AddFullTimeEmployee(ftEmp);
                                    Console.Clear();
                                    Console.WriteLine(type + "Employee with the following details has been successfully added to the Company container");
                                    ftEmp.Details(false);
                                    ftEmp = new FulltimeEmployee();
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: An employee with this sin number already exists");
                                }
                            }
                            else
                            {
                                Console.Clear();

                                Console.WriteLine(ftEmp.Validate());
                                Console.WriteLine(type + "Employee data has NOT been added");

                            }
                        }
                        else if (choice.KeyChar == '9')
                        {
                            bool finish = false;
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Discard Changes? Press 'y' to continue or 'n' to go back");
                                choice = Console.ReadKey();
                                if (choice.KeyChar == 'y')
                                {
                                    finish = true;
                                    break;
                                }
                                else if (choice.KeyChar == 'n')
                                {
                                    break;
                                }
                            }
                            if (finish == true)
                            {
                                break;
                            }
                        }
                        if ((choice.KeyChar == '2') || (choice.KeyChar == '3') || (choice.KeyChar == '4') || (choice.KeyChar == '6') || (choice.KeyChar == '7') || (choice.KeyChar == '8'))
                        {
                            Console.WriteLine("Press Any Key to Go Back to Previous Menu and Make Other Changes");
                            Console.ReadKey();
                        }
                    }//FULL TIME EMPLOYEE
                    #endregion
                    #region PARTTIME
                    else if (shortType == "PT")
                    {
                        String type = "Part Time";
                        String hDateStr, tDateStr, hourlyRateStr;
                        if (choice.KeyChar == '2')
                        {
                            hDateStr = SpecifyNameOrRateOrDateOrSIN("Hiring Date", "Please Enter Hiring Date of " + type);
                            if (ptEmp.SetDateOfHire(hDateStr))
                            {
                                if (hDateStr == "N/A")
                                {
                                    Console.WriteLine("Hiring Date Successfully Set to \"N/A\"");
                                }
                                else
                                {
                                    Console.WriteLine("Hiring Date Successfully Set to \"" + ((DateTime)ptEmp.GetDateOfHire()).ToString("yyyy-MM-dd") + "\"");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Hiring Date to \"" + hDateStr + "\"; Remain as \"" + ((DateTime)ptEmp.GetDateOfHire()).ToString("yyyy-MM-dd") + "\"");
                            }
                        }
                        else if (choice.KeyChar == '3')
                        {
                            tDateStr = SpecifyNameOrRateOrDateOrSIN("Termination Date", "Please Enter Termination Date of " + type);
                            if (ptEmp.SetDateOfTermination(tDateStr))
                            {
                                if (tDateStr == "N/A")
                                {
                                    Console.WriteLine("Termination Date Successfully Set to \"N/A\"");
                                }
                                else
                                {
                                    Console.WriteLine("Termination Date Successfully Set to \"" + ((DateTime)ptEmp.GetDateOfTermination()).ToString("yyyy-MM-dd") + "\"");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Termination Date to \"" + tDateStr + "\"; Remain as \"" + ((DateTime)ptEmp.GetDateOfTermination()).ToString("yyyy-MM-dd") + "\"");
                            }
                        }
                        else if (choice.KeyChar == '4')
                        {
                            hourlyRateStr = SpecifyNameOrRateOrDateOrSIN("Hourly Rate", "Please Enter Salary of " + type);
                            if (ptEmp.SetHourlyRate(hourlyRateStr))
                            {
                                Console.WriteLine("Hourly Rate Successfully Set to \"" + ptEmp.GetHourlyRate().ToString("0.00") + "\"");
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Hourly Rate to \"" + hourlyRateStr + "\"; Remain as \"" + ptEmp.GetHourlyRate().ToString("0.00") + "\"");
                            }
                        }
                        else if (choice.KeyChar == '6')
                        {
                            Console.Clear();
                            ptEmp.Details(true);
                        }
                        else if (choice.KeyChar == '7')
                        {
                            Console.Clear();
                            Console.WriteLine("Are you sure you want to discard data entered for this employee? YES[y] No[any_key]");
                            ftEmp.Details(false);
                            ConsoleKeyInfo c = new ConsoleKeyInfo();
                            c = Console.ReadKey();
                            if (c.KeyChar == 'y')
                            {
                                Console.Clear();
                                ftEmp = new FulltimeEmployee();
                                ptEmp = new ParttimeEmployee();
                                ctEmp = new ContractEmployee();
                                snEmp = new SeasonalEmployee();
                                Console.WriteLine(type + "Employee data has been successfully reset");
                                ptEmp.Details(false);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine(type + "Employee data has NOT been reset");
                                ptEmp.Details(false);
                            }
                        }
                        else if (choice.KeyChar == '8')
                        {
                            Employee e = new Employee();
                            if ((ptEmp.Validate()).Length <= 0)
                            {
                                foreach (Object o in company.container)
                                {
                                    e = (Employee)o;
                                    if (e.GetSIN() == ptEmp.GetSIN())
                                    {
                                        company.container.Remove(e);
                                        break;
                                    }
                                }
                                    company.AddPartTimeEmployee(ptEmp);
                                    Console.Clear();
                                    Console.WriteLine(type + "Employee with the following details has been successfully modified to the Company container");
                                    ptEmp.Details(false);
                                    ptEmp = new ParttimeEmployee();
                            }
                            else
                            {
                                Console.Clear();

                                Console.WriteLine(ptEmp.Validate());
                                Console.WriteLine(type + "Employee data has NOT been added");
                            }
                        }
                        else if (choice.KeyChar == '9')
                        {
                            bool finish = false;
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Discard Changes? Press 'y' to continue or 'n' to go back");
                                choice = Console.ReadKey();
                                if (choice.KeyChar == 'y')
                                {
                                    finish = true;
                                    break;
                                }
                                else if (choice.KeyChar == 'n')
                                {
                                    break;
                                }
                            }
                            if (finish == true)
                            {
                                break;
                            }
                        }
                        if ((choice.KeyChar == '2') || (choice.KeyChar == '3') || (choice.KeyChar == '4') || (choice.KeyChar == '6') || (choice.KeyChar == '7') || (choice.KeyChar == '8'))
                        {
                            Console.WriteLine("Press Any Key to Go Back to Previous Menu and Make Other Changes");
                            Console.ReadKey();
                        }
                    }//PART TIME EMPLOYEE
                    #endregion PARTTIME
                    #region CONTRACT
                    else if (shortType == "CT")
                    {
                        String type = "Contract";
                        String cStartDateStr, cEndDateStr, fixedContractAmtStr;
                        if (choice.KeyChar == '2')
                        {
                            cStartDateStr = SpecifyNameOrRateOrDateOrSIN("Contract Start Date", "Please Enter Contract Start Date of " + type);
                            if (ctEmp.SetContractStartDate(cStartDateStr))
                            {
                                if (cStartDateStr == "N/A")
                                {
                                    Console.WriteLine("Contract Start Date Successfully Set to \"N/A\"");
                                }
                                else
                                {
                                    Console.WriteLine("Contract Start Date Successfully Set to \"" + ((DateTime)ctEmp.GetContractStartDate()).ToString("yyyy-MM-dd") + "\"");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Contract Start Date to \"" + cStartDateStr + "\"; Remain as \""
                                    + (ctEmp.GetContractStartDate().HasValue ? ((DateTime)ctEmp.GetContractStartDate()).ToString("yyyy-MM-dd") : "N/A") + "\"");

                            }
                        }
                        else if (choice.KeyChar == '3')
                        {
                            cEndDateStr = SpecifyNameOrRateOrDateOrSIN("Contract End Date", "Please Enter Contract End Date of " + type);
                            if (ctEmp.SetContractEndDate(cEndDateStr))
                            {
                                if (cEndDateStr == "N/A")
                                {
                                    Console.WriteLine("Contract End Date Successfully Set to \"N/A\"");
                                }
                                else
                                {
                                    Console.WriteLine("Contract End Date Successfully Set to \"" + ((DateTime)ctEmp.GetContractEndDate()).ToString("yyyy-MM-dd") + "\"");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Contract End Date to \"" + cEndDateStr + "\"; Remain as \""
                                    + (ctEmp.GetContractEndDate().HasValue ? ((DateTime)ctEmp.GetContractEndDate()).ToString("yyyy-MM-dd") : "N/A") + "\"");
                            }
                        }
                        else if (choice.KeyChar == '4')
                        {
                            fixedContractAmtStr = SpecifyNameOrRateOrDateOrSIN("Fixed Contract Amount", "Please Enter Fixed Contract Amount of " + type);
                            if (ctEmp.SetFixedContractAmt(fixedContractAmtStr))
                            {
                                Console.WriteLine("Contract Fixed Contract Amount Successfully Set to \"" + ctEmp.GetFixedContractAmt().ToString("0.00") + "\"");
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Fixed Contract Amount to \"" + fixedContractAmtStr + "\"; Remain as \"" + ctEmp.GetFixedContractAmt().ToString("0.00") + "\"");
                            }
                        }
                        else if (choice.KeyChar == '6')
                        {
                            Console.Clear();
                            ctEmp.Details(true);
                        }
                        else if (choice.KeyChar == '7')
                        {
                            Console.Clear();
                            Console.WriteLine("Are you sure you want to discard data entered for this employee? YES[y] No[any_key]");
                            ctEmp.Details(false);
                            ConsoleKeyInfo c = new ConsoleKeyInfo();
                            c = Console.ReadKey();
                            if (c.KeyChar == 'y')
                            {
                                Console.Clear();
                                ftEmp = new FulltimeEmployee();
                                ptEmp = new ParttimeEmployee();
                                ctEmp = new ContractEmployee();
                                snEmp = new SeasonalEmployee();
                                Console.WriteLine(type + "Employee data has been successfully reset");
                                ctEmp.Details(false);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine(type + "Employee data has NOT been reset");
                                ctEmp.Details(false);
                            }
                        }
                        else if (choice.KeyChar == '8')
                        {
                            Employee e = new Employee();
                            if ((ctEmp.Validate()).Length <= 0)
                            {
                                 foreach (Object o in company.container)
                                {
                                    e = (Employee)o;
                                    if (e.GetSIN() == ctEmp.GetSIN())
                                    {
                                        company.container.Remove(e);
                                        break;
                                    }
                                }
                                company.AddContractEmployee(ctEmp);
                                Console.Clear();
                                Console.WriteLine(type + "Employee with the following details has been successfully modified to the Company container");
                                ctEmp.Details(false);
                                ctEmp = new ContractEmployee();
                            }
                            else
                            {
                                Console.Clear();

                                Console.WriteLine(ctEmp.Validate());
                                Console.WriteLine(type + "Employee data has NOT been added");

                            }
                        }
                        else if (choice.KeyChar == '9')
                        {
                            bool finish = false;
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Discard Changes? Press 'y' to continue or 'n' to go back");
                                choice = Console.ReadKey();
                                if (choice.KeyChar == 'y')
                                {
                                    finish = true;
                                    break;
                                }
                                else if (choice.KeyChar == 'n')
                                {
                                    break;
                                }
                            }
                            if (finish == true)
                            {
                                break;
                            }
                        }
                        if ((choice.KeyChar == '2') || (choice.KeyChar == '3') || (choice.KeyChar == '4') || (choice.KeyChar == '6') || (choice.KeyChar == '7') || (choice.KeyChar == '8'))
                        {
                            Console.WriteLine("Press Any Key to Go Back to Previous Menu and Make Other Changes");
                            Console.ReadKey();
                        }
                    }//CONTRACT
                    #endregion
                    #region SEASONAL
                    else if (shortType == "SN")
                    {
                        String type = "Seasonal";
                        String season, piecePay;
                        if (choice.KeyChar == '2')
                        {
                            season = SpecifySeason().ToUpper();
                            if (snEmp.SetSeason(season))
                            {
                                Console.WriteLine("Season Successfully Set to \"" + snEmp.GetSeason() + "\"");
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Season to \"" + season + "\"; Remain as \"" + snEmp.GetSeason() + "\"");
                            }
                        }
                        else if (choice.KeyChar == '3')
                        {
                            piecePay = SpecifyNameOrRateOrDateOrSIN("Piece Pay", "Please Enter Piece Pay of " + type);
                            if (snEmp.SetPiecePay(piecePay))
                            {
                                Console.WriteLine("Contract End Date Successfully Set to \"" + snEmp.GetPiecePay().ToString("0.00") + "\"");
                            }
                            else
                            {
                                Console.WriteLine("Failed to Set Contract End Date to \"" + piecePay + "\"; Remain as \"" + snEmp.GetPiecePay().ToString("0.00") + "\"");
                            }
                        }
                        else if (choice.KeyChar == '6')
                        {
                            Console.Clear();
                            snEmp.Details(true);
                        }
                        else if (choice.KeyChar == '7')
                        {
                            Console.Clear();
                            Console.WriteLine("Are you sure you want to discard data entered for this employee? YES[y] No[any_key]");
                            snEmp.Details(false);
                            ConsoleKeyInfo c = new ConsoleKeyInfo();
                            c = Console.ReadKey();
                            if (c.KeyChar == 'y')
                            {
                                Console.Clear();
                                ftEmp = new FulltimeEmployee();
                                ptEmp = new ParttimeEmployee();
                                ctEmp = new ContractEmployee();
                                snEmp = new SeasonalEmployee();
                                Console.WriteLine(type + "Employee data has been successfully reset");
                                snEmp.Details(false);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine(type + "Employee data has NOT been reset");
                                snEmp.Details(false);
                            }
                        }
                        else if (choice.KeyChar == '8')
                        {
                            Employee e = new Employee();
                            if ((snEmp.Validate()).Length <= 0)
                            {
                                foreach (Object o in company.container)
                                {
                                    e = (Employee)o;
                                    if (e.GetSIN() == snEmp.GetSIN())
                                    {
                                        company.container.Remove(e);
                                        break;
                                    }
                                }
                                company.AddSeasonalEmployee(snEmp);
                                Console.Clear();
                                Console.WriteLine(type + "Employee with the following details has been successfully modified to the Company container");
                                ctEmp.Details(false);
                                ctEmp = new ContractEmployee();
                            }
                            else
                            {
                                Console.Clear();

                                Console.WriteLine(ctEmp.Validate());
                                Console.WriteLine(type + "Employee data has NOT been added");

                            }
                        }
                        else if (choice.KeyChar == '9')
                        {
                            bool finish = false;
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Discard Changes? Press 'y' to continue or 'n' to go back");
                                choice = Console.ReadKey();
                                if (choice.KeyChar == 'y')
                                {
                                    finish = true;
                                    break;
                                }
                                else if (choice.KeyChar == 'n')
                                {
                                    break;
                                }
                            }
                            if (finish == true)
                            {
                                break;
                            }
                        }

                        if ((choice.KeyChar == '2') || (choice.KeyChar == '3') || (choice.KeyChar == '6') || (choice.KeyChar == '7') || (choice.KeyChar == '8'))
                        {
                            Console.WriteLine("Press Any Key to Go Back to Previous Menu and Make Other Changes");
                            Console.ReadKey();
                        }
                    }//SEASONAL
                    #endregion

                }
            }
        }

        /// <summary>
        /// Method: MenuFourToModify()
        /// Opens the modify employee menu and allows user to specify which feild they would like to modify
        /// </summary>
        public void MenuFourToModify()
        {
            String input = "";
            string type = "";
            Console.Clear();
            Console.WriteLine("Please enter SIN/BN of Employee you wish to modify:");
            input = Console.ReadLine();
            Employee e = new Employee();
           ///For each employee in the container checks for user defined sin number 
           ///Signifying which employee they would like to modify
            foreach (Object o in company.container)
            {
                e = (Employee)o;
                if (e.GetSIN() == input)
                {
                    Console.Clear();
                    Console.WriteLine("Found:");
                    e.Details(false);
                    Console.WriteLine("Press any key to continue:");
                    Console.ReadKey();
                    baseEmp = (Employee)o;
                    ///Checks the object for employee type
                    ///creates new employee object to be modified
                    ///if user is happy with the modifications object is stored to the container
                    ///replacing previouse employee data.
                    if (o is FulltimeEmployee)
                    {
                        type = "FT";
                        ftEmp = new FulltimeEmployee((FulltimeEmployee)o);
                    }
                    else if (o is ParttimeEmployee)
                    {
                        type = "PT";
                        ptEmp = new ParttimeEmployee((ParttimeEmployee)o);
                    }
                    else if (o is ContractEmployee)
                    {
                        type = "CT";
                        ctEmp = new ContractEmployee((ContractEmployee)o);
                    }
                    else if (o is SeasonalEmployee)
                    {
                        type = "SN";
                        snEmp = new SeasonalEmployee((SeasonalEmployee)o);
                    }
                    MenuFourToCreate(type, o, true);
                    break;
                }
            }
        }
    }
}

