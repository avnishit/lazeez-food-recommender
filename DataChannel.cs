using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Collections;
using System.Data;
using System.Configuration;

namespace FoodReco
{
    public class DataChannel
    {
        private OdbcConnection odbcConn = null;
        //private DataChannel dataChannel = null;

        public OdbcConnection GetDatabaseConnection 
        {
            get
            {
                return odbcConn;
            }

        }

        //public DataChannel GetDataChannel()
        //{
        //    dataChannel = new DataChannel();
        //    return dataChannel;
        //}

        public DataChannel()
        {
            try
            {
                odbcConn = new OdbcConnection();
                odbcConn.ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ=F:\\Academics\\Advanced Data Mining\\Assignment 2\\Dhruv_temp_1\\FoodRecommendations\\FoodRecoDB.mdb;UID=;PWD=;" ;
                
            }
            catch (OdbcException ex)
            {

            }
        }

        public void closeConn()
        {
            if (odbcConn != null)
            {
                odbcConn.Dispose();
            }
        }
    }
}
