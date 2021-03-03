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

public partial class getStatsDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void rgGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        string phase = "";
        if (Request.QueryString["phase"] != null)
        {

            try
            {
                 phase = Request.QueryString["phase"].Trim().ToString();
              
            }
            catch (Exception ex)
            {
                //pnlDanger.Visible = true;
                //lblDanger.Text = ex.Message.ToString();
            }

        }
        //string phase = "3";
        //int iphase = Convert.ToInt32(phase);

        //int yrval = DateTime.Now.Year;
        ////string timeframe = "py";
        //if (Request.QueryString["timeframe"] != null)
        //{

        //    try
        //    {
        //        string timeframe = Request.QueryString["timeframe"].Trim().ToString();
        //        if (timeframe == "py")
        //        {
        //            yrval = yrval - 1;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //pnlDanger.Visible = true;
        //        //lblDanger.Text = ex.Message.ToString();
        //    }

        //}
       

        SqlConnection cnn;
        List<ClsPieChart> pieChartList = new List<ClsPieChart>();
        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_GetDetailByPhase", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@OnboardingPhase", phase));
            //cmd.Parameters.Add(new SqlParameter("@yrval", yrval));
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

        //if (e.Item is GridFilteringItem)
        //{

        //    GridFilteringItem item = (GridFilteringItem)e.Item;
        //    if (item != null)
        //    {

        //        LiteralControl literalTo = item["TargetGoLive"].Controls[3] as LiteralControl;
        //        literalTo.Text = "<br />&nbsp;&nbsp;To-:&nbsp;&nbsp;";


        //    }

        //}

    }

    protected void rgGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            //adding all the links to the hyperlink column, to navigate when it is clicked. 
            //GridDataItem item = (GridDataItem)e.Item;
            ////GridDataItem dataItem = e.Item as GridDataItem;
            //TableCell cellitemp = item["Onboarding Phase"];
            //string phase = cellitemp.Text;


            ////Prior Year
            //HyperLink hLinkp = (HyperLink)item["NumberRequestsPY"].Controls[0];
            //hLinkp.ForeColor = System.Drawing.Color.Blue;
            //hLinkp.Attributes["onclick"] = "OpenWinPY('" + phase + "');";


            ////Current Year
            //HyperLink hLinkc = (HyperLink)item["NumberRequestsCY"].Controls[0];
            //hLinkc.ForeColor = System.Drawing.Color.Blue;
            //hLinkc.Attributes["onclick"] = "OpenWinCY('" + phase + "');";


        }
    }


    protected void rgGrid_ItemCommand(object source, GridCommandEventArgs e)
    {

        //if (e.CommandName == "EditRequest")
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //    }



        //}

    }
}