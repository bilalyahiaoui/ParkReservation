using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    interface ICampgroundSqlDAO
    {
        //gets available campgrounds based on inputted park
        List<Campground> GetCampgroundsFromPark(string inputtedPark);
        //converts SQL reader data to campground model
        Campground ConvertReaderToCampground(SqlDataReader reader);
    }
}
