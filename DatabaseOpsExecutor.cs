using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using System.Data.SqlClient;

namespace FoodReco
{
    public class DatabaseOpsExecutor
    {
        public DataSet executeQuery(String query, OdbcConnection conn)
        {
            DataSet myDataSet = new DataSet();
            //OleDbCommand CommandObject = new OleDbCommand(query);
            OdbcDataAdapter myDataAdapter = new OdbcDataAdapter(query, conn);
            myDataAdapter.Fill(myDataSet, "Data");
            myDataAdapter.Dispose();
            return myDataSet;
        }

        //public int executeNonQuery(String query, OdbcConnection con)
        //{
        //    OdbcCommand CommandObject = new OdbcCommand(query);
        //    int rowsAffected;
        //    CommandObject.Connection = con;
        //    try
        //    {
        //        if (CommandObject.Connection != null)
        //            CommandObject.Connection.Open();
        //        else
        //            return -10;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    rowsAffected = CommandObject.ExecuteNonQuery();
        //    try
        //    {
        //        if (CommandObject.Connection != null)
        //            CommandObject.Connection.Close();
        //        else
        //            return -10;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    CommandObject.Dispose();
        //    return rowsAffected;
        //}
    }
}
