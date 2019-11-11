using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    interface ISiteSqlDAO
    {
        //gets relavent information regarding a site based on user input
        List<Site> GetUserInputtedSite(int userCampgroundId, DateTime startDate, DateTime endDate);
        //converts SQL reader data to a site model
        Site ConvertReaderToSite(SqlDataReader reader);
    }
}
