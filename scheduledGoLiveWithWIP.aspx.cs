using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class scheduledGoLiveWithWIP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void rgGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
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
            cmd = new SqlCommand("sp_scheduledGoLiveWithWIP", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);
            rgGrid.DataSource = dt;

        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
        }
        finally
        {
            cnn.Close();
        }


    }

    protected void rgGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        if (e.Item is GridFilteringItem)
        {

            GridFilteringItem item = (GridFilteringItem)e.Item;
            if (item != null)
            {

                LiteralControl literalTo = item["TargetGoLive"].Controls[3] as LiteralControl;
                literalTo.Text = "<br />&nbsp;&nbsp;To-:&nbsp;&nbsp;";

                LiteralControl literalTo2 = item["CurrentGoLive"].Controls[3] as LiteralControl;
                literalTo2.Text = "<br />&nbsp;&nbsp;To-:&nbsp;&nbsp;";

                LiteralControl literalTo3 = item["ActualGoLive"].Controls[3] as LiteralControl;
                literalTo3.Text = "<br />&nbsp;&nbsp;To-:&nbsp;&nbsp;";


            }

        }

    }

    protected void rgGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            //adding all the links to the hyperlink column, to navigate when it is clicked. 
            GridDataItem item = (GridDataItem)e.Item;

            //string val = item.GetDataKeyValue("idRequest").ToString();
            //int requestID = Convert.ToInt32(val);

            HyperLink hLink = (HyperLink)item["CustomerName"].Controls[0];
            hLink.ForeColor = System.Drawing.Color.Blue;
            //ClsDiscoveryRequest row = (ClsDiscoveryRequest)item.DataItem;
            GridDataItem dataItem = e.Item as GridDataItem;
            TableCell cell = dataItem["idRequest"];
            string idRequest = cell.Text;
            hLink.Attributes["onclick"] = "OpenWin('" + idRequest + "');";



        }
    }

    protected void rgGrid_ItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName == "EditRequest")
        {
            try
            {

            }
            catch (Exception ex)
            {
            }



        }

    }
}