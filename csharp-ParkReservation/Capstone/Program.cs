using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Capstone.Models;
using Capstone.DAL;
using System.Collections.Generic;

namespace Capstone
{
    class Program
    {
       
        static void Main(string[] args)
        {
            

            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("Project");

            CapstoneCLI capstoneCli = new CapstoneCLI(connectionString);


            capstoneCli.DisplayParks();

            Console.ReadLine();


            //string userDirtyInput = Console.ReadLine();
            //string userInput = CampCLIHelper.GetUserString(userDirtyInput);


            //CODE BELOW TESTS GETTING THE USER DICTATED PARK
            //ParkSqlDAO testPark = new ParkSqlDAO(connectionString);
            //IList<Park> test = new List<Park>(testPark.GetUserPark(userInput));
            //foreach (Park item in test)
            //{
            //    Console.WriteLine($"| {item.ParkId} | {item.Name} | {item.Location} | {item.EstablishDate} | {item.Area} | {item.Visitors}| \n {item.Description} \n");
            //}

            //CODE BELOW TO GRAB CAMPGROUNDS FROM USER INPUTTED PARK
            //CampgroundSqlDAO testCgx = new CampgroundSqlDAO(connectionString);
            //IList<Campground> testCg = new List<Campground>(testCgx.GetCampgroundsFromPark(userInput));

            //foreach  (Campground campground in testCg)
            //{
            //    Console.WriteLine($"| {campground.CampgroundId} | {campground.Name} | {campground.DailyFee} |\n");

            //}

            //The code below is to test getting the sites based on the user inputted Campground

            //the code below grabs the sites available under the camp the user selects (int userInput = (whatever))
            //int userInput = 1;
            //DateTime.TryParse("10/21/2019", out DateTime startDate);
            //DateTime.TryParse("10/26/2019", out DateTime endDate);
            //SiteSqlDAO testSite = new SiteSqlDAO(connectionString);
            //IList<Site> test = new List<Site>(testSite.GetUserInputtedSite(userInput, startDate, endDate));
            //foreach (Site item in test)
            //{
            //    Console.WriteLine($"| {item.CampgroundId} | {item.SiteNumber} | {item.MaxOccupancy} | {item.Accessible} | {item.MaxRvLength} | {item.Utilities}|\n");
            //}

            //ReservationSqlDAO newReservation = new ReservationSqlDAO(connectionString);
            //newReservation.PlaceReservation(1, "Campbells", Convert.ToDateTime("06/16/2020"), Convert.ToDateTime("06/20/2020"));
            //Console.WriteLine("Success!");
            //Console.ReadLine();
            
        }
       
    }
}
