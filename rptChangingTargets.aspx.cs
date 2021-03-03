using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class rptChangingTargets : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userName"] != null && Session["appName"] != null)
            {


                var firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);
                dpInvoiceDate1.SelectedDate = firstDayOfYear;
                var lastDayOfYear = new DateTime(DateTime.Now.Year, 12, 31);
                dpInvoiceDate2.SelectedDate = lastDayOfYear;                
                initializeData();


            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    protected void initializeData()
    {

        DataTable dt1 = new DataTable();

        try
        {

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rptData1 = new ReportDataSource("DataSet1", dt1);

            LocalReport r = new LocalReport();
            r.ReportPath = Server.MapPath("~/rptChangingTargets.rdlc");
            r.DataSources.Add(rptData1);

            ReportViewer1.LocalReport.DataSources.Add(rptData1);

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/rptChangingTargets.rdlc");
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = false;


        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
            lblDanger.Text = errMsg;
            pnlDanger.Visible = true;
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        loadData();


    }

    private void SetReportParameters()
    {

        ReportParameter[] Params = new ReportParameter[2];
        Params[0] = new ReportParameter("invDate1", dpInvoiceDate1.SelectedDate.ToString());
        Params[1] = new ReportParameter("invDate2", dpInvoiceDate2.SelectedDate.ToString());
        this.ReportViewer1.LocalReport.SetParameters(Params);
        ReportViewer1.LocalReport.DisplayName = "ChangingTargets";
    }

    protected void loadData()
    {
        //SetReportParameters();
        DateTime? invdate1 = dpInvoiceDate1.SelectedDate;
        DateTime? invdate2 = dpInvoiceDate2.SelectedDate;

        String strConnString = ConfigurationManager.ConnectionStrings["PuroTouchDBSQLConnectionString"].ConnectionString;
        SqlConnection cnn;
        cnn = new SqlConnection(strConnString);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_ChangingTargetDates", cnn);
            cmd.Parameters.Add(new SqlParameter("@fromdate", invdate1));
            cmd.Parameters.Add(new SqlParameter("@todate", invdate2));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 10800;
            da.SelectCommand = cmd;
            da.Fill(dt);

            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rptData = new ReportDataSource("DataSet1", dt);

            LocalReport r = new LocalReport();
            r.ReportPath = Server.MapPath("~/rptChangingTargets.rdlc");
            r.DataSources.Add(rptData);

            ReportViewer1.LocalReport.DataSources.Add(rptData);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/rptChangingTargets.rdlc");
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.DisplayName = "ChangingTargets";


        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
            lblDanger.Text = errMsg;
            pnlDanger.Visible = true;
        }
        finally
        {
            cnn.Close();
        }
    }
}