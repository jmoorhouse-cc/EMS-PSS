//
// FILE: Program.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: Contains the Main() function for the EMS program. Runs the UI for the user to interact with.
//   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supporting;
using AllEmployees;
using TheCompany;
using Presentation;


namespace EMS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 35);
            Logging log = new Logging();
            log.writeLog("test");
            UIMenu UI = new UIMenu();
            //UI.MenuFourToCreate("FT");
            UI.MenuOne();

        }
    }
}
