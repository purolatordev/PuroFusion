using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsPieChart
/// </summary>
public class ClsPieChart
{
    public string Desc { get; set; }
    public Int16 Amount { get; set; }
    //public string formatAmount { get; set; }


    public List<ClsPieChart> getOnboardingPhaseCountAll()
    {
        SqlConnection cnn;
        List<ClsPieChart> pieChartList = new List<ClsPieChart>();
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetOnboardingPhaseCountAll", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

            //turn dt into a List
            foreach (DataRow dr in dt.Rows)
            {
                pieChartList.Add(new ClsPieChart { Desc = dr[0].ToString(), Amount = (Convert.ToInt16(dr[1])) });
            }
        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
        }
        finally
        {
            cnn.Close();
        }
        return pieChartList;
    }

    public List<ClsPieChart> getOnboardingPhaseCountSales(string userLogin)
    {
        SqlConnection cnn;
        List<ClsPieChart> pieChartList = new List<ClsPieChart>();
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetOnboardingPhaseCountSales", cnn);
            cmd.Parameters.Add(new SqlParameter("@salesUserLogin", userLogin));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

            //turn dt into a List
            foreach (DataRow dr in dt.Rows)
            {
                pieChartList.Add(new ClsPieChart { Desc = dr[0].ToString(), Amount = (Convert.ToInt16(dr[1])) });
            }
        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
        }
        finally
        {
            cnn.Close();
        }
        return pieChartList;
    }

    public List<ClsPieChart> getOnboardingPhaseCountDistrict(string district)
    {
        SqlConnection cnn;
        List<ClsPieChart> pieChartList = new List<ClsPieChart>();
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetOnboardingPhaseCountDistrict", cnn);
            cmd.Parameters.Add(new SqlParameter("@district", district));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

            //turn dt into a List
            foreach (DataRow dr in dt.Rows)
            {
                pieChartList.Add(new ClsPieChart { Desc = dr[0].ToString(), Amount = (Convert.ToInt16(dr[1])) });
            }
        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
        }
        finally
        {
            cnn.Close();
        }
        return pieChartList;
    }

    public List<ClsPieChart> getOnboardingPhaseCountITBA(string userLogin)
    {
        SqlConnection cnn;
        List<ClsPieChart> pieChartList = new List<ClsPieChart>();
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetOnboardingPhaseCountITBA", cnn);
            cmd.Parameters.Add(new SqlParameter("@ITBAUserLogin", userLogin));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

            //turn dt into a List
            foreach (DataRow dr in dt.Rows)
            {
                pieChartList.Add(new ClsPieChart { Desc = dr[0].ToString(), Amount = (Convert.ToInt16(dr[1])) });
            }
        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
        }
        finally
        {
            cnn.Close();
        }
        return pieChartList;
    }

}