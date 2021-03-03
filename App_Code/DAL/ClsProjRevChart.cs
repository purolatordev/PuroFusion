using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsProjRevChart
/// </summary>
public class ClsProjRevChart
{
    public string MonthName { get; set; }
    public string month { get; set; }
    public decimal Rev { get; set; }

	public ClsProjRevChart()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public List<ClsProjRevChart> getProjectedRevenueAll()
    {
        SqlConnection cnn;
        List<ClsProjRevChart> projRevList = new List<ClsProjRevChart>();
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetProjRevenueAll", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

            //turn dt into a List
            foreach (DataRow dr in dt.Rows)
            {
                projRevList.Add(new ClsProjRevChart { MonthName = dr[0].ToString(), month = dr[1].ToString(), Rev = (Convert.ToDecimal(dr[2])) });
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
        return projRevList;
    }

    public List<ClsProjRevChart> getProjectedRevenueSales(string userLogin)
    {
        SqlConnection cnn;
        List<ClsProjRevChart> projRevList = new List<ClsProjRevChart>();
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetProjRevenueSales", cnn);
            cmd.Parameters.Add(new SqlParameter("@salesUserLogin", userLogin));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

            //turn dt into a List
            foreach (DataRow dr in dt.Rows)
            {
                projRevList.Add(new ClsProjRevChart { MonthName = dr[0].ToString(), month = dr[1].ToString(), Rev = (Convert.ToDecimal(dr[2])) });
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
        return projRevList;
    }
    public List<ClsProjRevChart> getProjectedRevenueDistrict(string district)
    {
        SqlConnection cnn;
        List<ClsProjRevChart> projRevList = new List<ClsProjRevChart>();
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetProjRevenueDistrict", cnn);
            cmd.Parameters.Add(new SqlParameter("@district", district));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

            //turn dt into a List
            foreach (DataRow dr in dt.Rows)
            {
                projRevList.Add(new ClsProjRevChart { MonthName = dr[0].ToString(), month = dr[1].ToString(), Rev = (Convert.ToDecimal(dr[2])) });
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
        return projRevList;
    }

    public List<ClsProjRevChart> getProjectedRevenueITBA(string userLogin)
    {
        SqlConnection cnn;
        List<ClsProjRevChart> projRevList = new List<ClsProjRevChart>();
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetProjRevenueITBA", cnn);
            cmd.Parameters.Add(new SqlParameter("@ITBAUserLogin", userLogin));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

            //turn dt into a List
            foreach (DataRow dr in dt.Rows)
            {
                projRevList.Add(new ClsProjRevChart { MonthName = dr[0].ToString(), month = dr[1].ToString(), Rev = (Convert.ToDecimal(dr[2])) });
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
        return projRevList;
    }
}