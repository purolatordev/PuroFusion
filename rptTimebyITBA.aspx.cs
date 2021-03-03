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

public partial class rptTimebyITBA : System.Web.UI.Page
{
    PuroTouchRepository repository = new PuroTouchRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userName"] != null && Session["appName"] != null)
            {

                getITBAs();
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dpInvoiceDate1.SelectedDate = firstDayOfMonth;
                dpInvoiceDate2.SelectedDate = DateTime.Now;
                initializeData();


            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

    }
    protected void getITBAs()
    {
        try
        {
            List<ClsITBA> balist = repository.GetITBAs();
            ClsITBA all = new ClsITBA();
            all.ITBA = "All";
            all.idITBA = 0;
            balist.Insert(0, all);
            rddlITBA.DataSource = balist;
            rddlITBA.DataTextField = "ITBA";
            rddlITBA.DataValueField = "idITBA";
            rddlITBA.DataBind();
            rddlITBA.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            r.ReportPath = Server.MapPath("~/rptTimebyITBA.rdlc");
            r.DataSources.Add(rptData1);

            ReportViewer1.LocalReport.DataSources.Add(rptData1);

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/rptTimebyITBA.rdlc");
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
        ReportViewer1.LocalReport.DisplayName = "rptTimebyITBA";
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
        int selval = Convert.ToInt16(rddlITBA.SelectedValue);
        try
        {
            cmd = new SqlCommand("sp_OnboardingTimePerITBA", cnn);
            cmd.Parameters.Add(new SqlParameter("@fromdate", invdate1));
            cmd.Parameters.Add(new SqlParameter("@todate", invdate2));
            cmd.Parameters.Add(new SqlParameter("@idITBA", selval));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 10800;
            da.SelectCommand = cmd;
            da.Fill(dt);

            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rptData = new ReportDataSource("DataSet1", dt);

            LocalReport r = new LocalReport();
            r.ReportPath = Server.MapPath("~/rptTimebyITBA.rdlc");
            r.DataSources.Add(rptData);

            ReportViewer1.LocalReport.DataSources.Add(rptData);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/rptTimebyITBA.rdlc");
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.DisplayName = "TimePerITBA";


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