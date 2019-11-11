using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    interface IParkSqlDAO
    {
        //method to view all parks
        List<Park> GetParks();
        //method to get info of a user specified park
        Park GetUserPark(string inputtedPark);
        //method to convert the SQL data received to the park model
        Park ConvertReaderToPark(SqlDataReader reader);
    }
}
