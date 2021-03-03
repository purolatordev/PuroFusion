using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsColumnChart
/// </summary>
public class clsColumnChart
{
    public string customer { get; set; }
    public int reqCount { get; set; }
    public int phaseCount { get; set; }
    public int reqMonth { get; set; }

    public static DataTable getSPColumnChartData()
    {
        SqlConnection cnn;
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_SPColumnChartData", cnn);

            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
        }
        finally
        {
            cnn.Close();
        }
        return dt;
    }
    public static List<clsColumnChart> getSPColumnChartDataList()
    {
        DataTable dt = clsColumnChart.getSPColumnChartData();
        List<clsColumnChart> reqChartList = new List<clsColumnChart>();
        clsColumnChart ocolChrt = new clsColumnChart();

        foreach (DataRow row in dt.Rows)
        {
            reqChartList.Add(new clsColumnChart { reqCount = Convert.ToInt16(row[0].ToString()), reqMonth = Convert.ToInt16(row[2].ToString()) });
        }

        return reqChartList;
    }
}