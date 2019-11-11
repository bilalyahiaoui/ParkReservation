using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Capstone
{
    public class CapstoneCLI
    {

        //const string Command_ViewParks;
        const string Command_ViewCampgrounds = "1";
        const string Command_SearchForReservation = "2";
        const string Command_SearchForAvailableReservation = "1";
        const string Command_ReturnToPreviousMenu= "3";
        const string Command_ReturnToParkInfoMenu = "2";
        const string Command_Quit = "q";

        private ParkSqlDAO parks;

        private string ConnectionString;
        private string ChosenPark;

        public CapstoneCLI(string connectionString)
        {
            parks = new ParkSqlDAO(connectionString);
            ConnectionString = connectionString;
        }
        
        public void ParkInfoMenu()
        {
            while (true)
            {
                string command = CampCLIHelper.GetUserString("Select a Command");

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_ViewCampgrounds:
                        DisplayCamgrounds();
                        DisplayParkCampgroundMenu();
                        return;
                    case Command_SearchForReservation:
                        DisplayCamgrounds();
                        DisplayParkCampgroundMenu();
                        return;
                    case Command_ReturnToPreviousMenu:
                        DisplayParks();
                        return;
                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }
               DisplayParkInformationMenu();
            }
        }


        public void CampgroundMenu()
        {
            while (true)
            {
                string command = CampCLIHelper.GetUserString("Select a Command");

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_SearchForAvailableReservation:
                        DisplayCamgrounds();
                        DisplayMenuAskingUserForDate();
                        return;
                    case Command_ReturnToParkInfoMenu:
                        DisplayPark(ChosenPark);
                        DisplayParkInformationMenu();
                        return;
                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }
                DisplayParkInformationMenu();
            }
        }


        public void DisplayParks()
        {
            List<Park> parkList = new List<Park>();
            parkList = parks.GetParks();
            int number = 1;
            foreach (Park park in parkList)
            {
                Console.WriteLine(number + ") " + park.Name);
                number += 1;

            }
            Console.WriteLine("Q) Quit");

            string userInput = CampCLIHelper.GetUserString("Please select a Park: ");
            if (userInput.ToLower() == "q")
            {
                System.Environment.Exit(0);
            }
            else
            {
                DisplayPark(userInput);
            }
        }
        public void DisplayPark(string name)
        {
                Park park = new Park();

                // we have to get the list of parks from ParkSqlDAO
                park = parks.GetUserPark(name);
                // we have to display the park info
                Console.WriteLine(park.Name + " National Park");
                Console.WriteLine("Location: " + park.Location );
                Console.WriteLine( "Established: " + park.EstablishDate.ToShortDateString());
                Console.WriteLine("Area: "+ park.Area.ToString("N0") + " sq km");
                Console.WriteLine("Annual Visitors:  " +park.Visitors.ToString("N0"));
                Console.WriteLine(park.Description);

                Console.WriteLine();
                ChosenPark = name;
                DisplayParkInformationMenu();
            
           
        }
        public void DisplayParkInformationMenu()
        {
            Console.WriteLine("1) View Campgrounds ");
            Console.WriteLine("2) Search For Reservation ");
            Console.WriteLine("3) Return To Previous Screen ");

            ParkInfoMenu();
        }

        public void DisplayCamgrounds()
        {
            int number = 1;
            CampgroundSqlDAO campgroundSqlDAO = new CampgroundSqlDAO(ConnectionString);
            List<Campground> listOfCampgrounds = campgroundSqlDAO.GetCampgroundsFromPark(ChosenPark);
            Console.WriteLine("      Name                 Open              Close         Daily Fee");
            foreach (Campground campground in listOfCampgrounds)
            {
                Console.Write("#" + number + " " + campground.Name + "            ");
                Console.Write(DateTimeFormatInfo.CurrentInfo.GetMonthName(campground.OpenFromMonth) + "             ");
                Console.Write(DateTimeFormatInfo.CurrentInfo.GetMonthName(campground.OpenToMonth) + "             ");
                Console.Write(campground.DailyFee.ToString("C2") + "    ");
                Console.WriteLine();
                number = number + 1;
            }


        }
        public void DisplayParkCampgroundMenu()
        {
            Console.WriteLine("1) Search For Available Reservation ");

            Console.WriteLine("2) Return to previous screen");
            CampgroundMenu();
        }
        public void DisplayMenuAskingUserForDate()
        {
            Console.WriteLine("Which Campground (enter 0 to cancel): ");
            int campGround = int.Parse(Console.ReadLine());

            if (campGround == 0)
            {
                DisplayParkCampgroundMenu();
            }
            else
            {
                
                DateTime arrivalDate = CampCLIHelper.GetUserDateTime("What is the arrival date? ");
                DateTime departureDate = CampCLIHelper.GetUserDateTime("What is the departure date ?");
                SiteSqlDAO site = new SiteSqlDAO(ConnectionString);
                List<Site> listOfSites = site.GetUserInputtedSite(campGround, arrivalDate, departureDate);
                Console.WriteLine("Site No.    Max Occup.    Accessible?    Max RV Length            Utility           Cost");

                foreach (Site item in listOfSites)
                {
                    Console.Write(item.SiteID + "           ");
                    Console.Write(item.MaxOccupancy + "             ");
                    Console.Write(CampCLIHelper.BoolToString(item.Accessible) + "                 ");
                    Console.Write(CampCLIHelper.NumberToString(item.MaxRvLength) + "                         ");
                    Console.Write(CampCLIHelper.BoolToString(item.Utilities) + "              ");
                    Console.Write(item.Cost.ToString("C2") + "                     ");
                    Console.WriteLine();
                }


                int sites = CampCLIHelper.GetUserInt("Which site should be reserved (enter 0 to cancel):");
                if (sites == 0)
                {
                    DisplayParkCampgroundMenu();
                }
                else
                {
                    
                    string name = CampCLIHelper.GetUserString("What name should the reservation be made under ?");
                    ReservationSqlDAO reservation = new ReservationSqlDAO(ConnectionString);
                    int reservationId = reservation.PlaceReservation(sites, name, arrivalDate, departureDate);

                Console.WriteLine($"The reservation has been made and the Confirmation id is: {reservationId}");
                }
            }

            Console.ReadLine();
        }
       

    }
}
