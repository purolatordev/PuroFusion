using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using context = System.Web.HttpContext;

public partial class EDI214 : System.Web.UI.UserControl
{
    const int INVOICE_COURIER_EDI = 3;
    const int SHIPMENT_STATUS_COURIER_EDI = 4;
    const int SHIPMENT_STATUS_NON_COURIER_EDI = 6;

    public event EventHandler RemoveUserControl2;
    public event EventHandler UserControlSaved;
    public event EventHandler buttonClick;
    //public UserControlParams Params = new UserControlParams();
    public UserControlParams Params;
    public void LoadParams(UserControlParams p)
    {
        Params = p;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            txtBoxMultiDebug2.Text +=
                "Page_Load Start- " +
                //RadPanelBar1.Items[0].Text + ": " +
                comboxCommunicationMethod.SelectedText.ToString() + ": " +
                comboxCommunicationMethod.SelectedValue.ToString() + "\r\n";
            txtBoxMultiDebug2.Visible = true;
        }
        else
            txtBoxMultiDebug2.Visible = false;

        clsEDIRecipReq qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqsByID(Params.idEDIRecipReqs);
        if (qEDIRecipReq != null && !IsPostBack)
        {
            GetFileFormats();
            GetCommunicationMethods();
            GetTriggerMechanisms();
            GetTiming();
            if( Params.ct == UserControlParams.CourierType.CourierEDI)
                GetStatusCodesCourier();
            else if(Params.ct == UserControlParams.CourierType.NonCourierEDI)
                GetStatusCodesNonCourier();
            UpdateControls(qEDIRecipReq);
        }
        else if (Params.bNewDialog)
        {
            Params.CheckPassBacks(Params.idEDIRecipReqs, false);
            RemoveUserControl2(sender, e);
            GetFileFormats();
            GetCommunicationMethods();
            GetTriggerMechanisms();
            GetTiming();
            if (Params.ct == UserControlParams.CourierType.CourierEDI)
                GetStatusCodesCourier();
            else if (Params.ct == UserControlParams.CourierType.NonCourierEDI)
                GetStatusCodesNonCourier();
            UpdateControls(qEDIRecipReq);
        }
        SetFileFormatControls();
        SetCommunicationMethodControls();
        SetFileFormatControls();
        SetCommunicationMethodControls();
        SetTimeOfFileControls();

        if (!string.IsNullOrEmpty(qEDIRecipReq.PanelTitle))
            RadPanelBar1.Items[0].Text = qEDIRecipReq.PanelTitle;
        else
            RadPanelBar1.Items[0].Text = "Record num: " + (Params.iRecordID + 1).ToString();

        RadPanelBar1.Items[0].Expanded = false;
        textBoxPanelTitle.Text = RadPanelBar1.Items[0].Text;

        if (Params.ct == UserControlParams.CourierType.NonCourierEDI)
        {
            comboxTriggerMechanism.Visible = false;
            comboxTiming.Visible = false;
            lblTiming.Visible = false;
            lblTriggerMechanism.Visible = false;
        }
        if(!Params.bUseTimeOfFile)
        {
            lblTimeofFile.Visible = false;
            timeTimeofFile.Visible = false;
        }
        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            txtBoxMultiDebug2.Text +=
                "Page_Load End- " +
                RadPanelBar1.Items[0].Text + ": " +
                comboxCommunicationMethod.SelectedText.ToString() + ": " +
                comboxCommunicationMethod.SelectedValue.ToString() + "\r\n";
        }
    }
    [MethodImpl(MethodImplOptions.NoInlining)]
    public string GetCurrentMethod()
    {
        var st = new StackTrace();
        var sf = st.GetFrame(1);
        return sf.GetMethod().Name;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
    }
    void RoomForm_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            int er = 0;
            er++;
        }
    }
    protected internal void btnRemove_Click(object sender, System.EventArgs e)
    {
        //Raise this event so the parent page can handle it   
        RemoveUserControl2(sender, e);
    }

    protected void FileFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetFileFormatControls();
    }

    private void SetFileFormatControls()
    {
        if (comboBxFileFormat214.SelectedText.ToString().Contains("X12"))
        {
            lblISA.Visible = true;
            txtBoxISA.Visible = true;
            lblGS.Visible = true;
            txtBoxGS.Visible = true;
            lblQualifier.Visible = true;
            txtBoxQualifier.Visible = true;
        }
        else
        {
            lblISA.Visible = false;
            txtBoxISA.Visible = false;
            lblGS.Visible = false;
            txtBoxGS.Visible = false;
            lblQualifier.Visible = false;
            txtBoxQualifier.Visible = false;
        }
    }

    protected void CommunicationMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCommunicationMethodControls();
        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            txtBoxMultiDebug2.Text +=
                "CommunicationMethod- " +
                RadPanelBar1.Items[0].Text + ": " +
                comboxCommunicationMethod.SelectedText.ToString() + ": " +
                comboxCommunicationMethod.SelectedValue.ToString() + "\r\n";
        }
        Save();
    }

    private void SetCommunicationMethodControls()
    {
        if (comboxCommunicationMethod.SelectedText.ToString().ToLower().Contains("ftp"))
        {
            lblFTPAddress.Visible = true;
            textBoxFTPAddress.Visible = true;
            lblUserName.Visible = true;
            textBoxUserName.Visible = true;
            lblPassword.Visible = true;
            textBoxPassword.Visible = true;
            lblFolderPath.Visible = true;
            textBoxFolderPath.Visible = true;
            lblEmail.Visible = false;
            textBoxEmail.Visible = false;
        }
        else
        {
            lblFTPAddress.Visible = false;
            textBoxFTPAddress.Visible = false;
            lblUserName.Visible = false;
            textBoxUserName.Visible = false;
            lblPassword.Visible = false;
            textBoxPassword.Visible = false;
            lblFolderPath.Visible = false;
            textBoxFolderPath.Visible = false;
            if (comboxCommunicationMethod.SelectedText.ToString().ToLower().Contains("email"))
            {
                lblEmail.Visible = true;
                textBoxEmail.Visible = true;
            }
        }
    }

    protected void Timing_SelectedIndexChanged (object sender, EventArgs e)
    {
        SetTimeOfFileControls();
    }

    private void SetTimeOfFileControls()
    {
        if (comboxTiming.SelectedText.ToString().Contains("Once a Day"))
        {
            lblTimeofFile.Visible = true;
            timeTimeofFile.Visible = true;
        }
        else
        {
            lblTimeofFile.Visible = false;
            timeTimeofFile.Visible = false;
        }
    }

    protected void btnfirst_Click(object sender, EventArgs e)
    {
        int er = 0;
        er++;
    }
    protected void GetFileFormats()
    {
        try
        {
            bool bNonCourierEDI = false;
            if (Params.ct == UserControlParams.CourierType.NonCourierEDI)
            {
                bNonCourierEDI = true;
            }
            List<ClsFileType> qFileTypes = SrvFileType.GetFileTypes(bNonCourierEDI);
            comboBxFileFormat214.DataSource = qFileTypes;
            comboBxFileFormat214.DataTextField = "FileType";
            comboBxFileFormat214.DataValueField = "idFileType";
            comboBxFileFormat214.DataBind();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { Method = GetCurrentMethod(), ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now, CreatedBy = Session["userName"].ToString() };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
    }
    protected void GetCommunicationMethods()
    {
        try
        {
            List<ClsCommunicationMethod> qCommMeth = SrvCommunicationMethod.GetCommunicationMethods();
            comboxCommunicationMethod.DataSource = qCommMeth;
            comboxCommunicationMethod.DataTextField = "CommunicationMethod";
            comboxCommunicationMethod.DataValueField = "idCommunicationMethod";
            comboxCommunicationMethod.DataBind();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { Method = GetCurrentMethod(), ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now, CreatedBy = Session["userName"].ToString() };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            txtBoxMultiDebug2.Text +=
                "GetCommunicationMethods- " +
                RadPanelBar1.Items[0].Text + ": " +
                comboxCommunicationMethod.SelectedText.ToString() + ": " +
                comboxCommunicationMethod.SelectedValue.ToString() + "\r\n";
        }
    }
    protected void GetTriggerMechanisms()
    {
        try
        {
            List<clsTriggerMechanism> qTrigMeth = SrvTriggerMechanism.GetTriggerMechanisms();
            comboxTriggerMechanism.DataSource = qTrigMeth;
            comboxTriggerMechanism.DataTextField = "TriggerMechanism";
            comboxTriggerMechanism.DataValueField = "idTriggerMechanism";
            comboxTriggerMechanism.DataBind();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { Method = GetCurrentMethod(), ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now, CreatedBy = Session["userName"].ToString() };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
    }
    protected void GetTiming()
    {
        try
        {
            List<clsTiming> qTiming = SrvTiming.GetTiming();
            comboxTiming.DataSource = qTiming;
            comboxTiming.DataTextField = "Timing";
            comboxTiming.DataValueField = "idTiming";
            comboxTiming.DataBind();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { Method = GetCurrentMethod(), ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now, CreatedBy = Session["userName"].ToString() };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
    }
    protected void GetStatusCodesCourier()
    {
        try
        {
            List<clsStatusCodeCourierEDI> qStatusCode = SrvStatusCodeCourierEDI.GetStatusCodes();
            comboxStatusCodes.DataSource = qStatusCode;
            comboxStatusCodes.DataTextField = "StatusCode";
            comboxStatusCodes.DataValueField = "idStatusCodesCourierEDI";
            comboxStatusCodes.DataBind();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { Method = GetCurrentMethod(), ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now, CreatedBy = Session["userName"].ToString() };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
    }
    protected void GetStatusCodesNonCourier()
    {
        try
        {
            List<clsStatusCodeNonCourierEDI> qStatusCode = SrvStatusCodeNonCourierEDI.GetStatusCodes();
            comboxStatusCodes.DataSource = qStatusCode;
            comboxStatusCodes.DataTextField = "StatusCode";
            comboxStatusCodes.DataValueField = "idStatusCodesNonCourierEDI";
            comboxStatusCodes.DataBind();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { Method = GetCurrentMethod(), ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now, CreatedBy = Session["userName"].ToString() };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
    }
    protected void btnSubmitChanges_Click(object sender, EventArgs e)
    {
        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            txtBoxMultiDebug2.Text +=
                "Save Changes- " +
                RadPanelBar1.Items[0].Text + ": " +
                comboxCommunicationMethod.SelectedText.ToString() + ": " +
                comboxCommunicationMethod.SelectedValue.ToString() + "\r\n";
        }
        Save();
        UserControlSaved(sender, e);
    }

    private void Save()
    {
        int iFileformat = int.Parse(comboBxFileFormat214.SelectedValue);
        int iCommMethod = int.Parse(comboxCommunicationMethod.SelectedValue);
        int iTriggerMech = int.Parse(comboxTriggerMechanism.SelectedValue);
        int iTiming = int.Parse(comboxTiming.SelectedValue);

        clsEDIRecipReq qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqsByID(Params.idEDIRecipReqs);

        qEDIRecipReq.idFileType = iFileformat;
        qEDIRecipReq.X12_ISA = txtBoxISA.Text;
        qEDIRecipReq.X12_GS = txtBoxGS.Text;
        qEDIRecipReq.X12_Qualifier = txtBoxQualifier.Text;
        qEDIRecipReq.idCommunicationMethod = iCommMethod;
        qEDIRecipReq.FTPAddress = textBoxFTPAddress.Text;
        qEDIRecipReq.UserName = textBoxUserName.Text;
        qEDIRecipReq.Password = textBoxPassword.Text;
        qEDIRecipReq.FolderPath = textBoxFolderPath.Text;
        qEDIRecipReq.Email = textBoxEmail.Text;
        qEDIRecipReq.idTriggerMechanism = iTriggerMech;
        qEDIRecipReq.idTiming = iTiming;

        TimeSpan? ts = timeTimeofFile.SelectedTime;
        qEDIRecipReq.TimeOfFile = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ts.Value.Hours, ts.Value.Minutes, 0);
        qEDIRecipReq.PanelTitle = textBoxPanelTitle.Text;
        RadPanelBar1.Items[0].Text = qEDIRecipReq.PanelTitle;
        qEDIRecipReq.UpdatedBy = Session["userName"].ToString();
        qEDIRecipReq.UpdatedOn = DateTime.Now;
        SrvEDIRecipReq.Insert(qEDIRecipReq);
        int idStatusCodesAll = 0;
        if (Params.ct == UserControlParams.CourierType.CourierEDI)
        {
            foreach (RadComboBoxItem rad in comboxStatusCodes.Items)
            {
                clsStatusCodeCourierEDI cEDI = SrvStatusCodeCourierEDI.GetStatusCodes().Where(p => p.idStatusCodesCourierEDI == int.Parse(rad.Value.ToString())).FirstOrDefault();
                if (rad.Checked)
                {
                    SrvStatusCodeAll.UpdatetatusCodeAll(new clsStatusCodeAll() { idStatusCodesAll = idStatusCodesAll, idEDIRecipReqs = qEDIRecipReq.idEDIRecipReqs, idStatusCodes = int.Parse(rad.Value.ToString()), StatusCode = cEDI.StatusCode, idEDITranscationType = SHIPMENT_STATUS_COURIER_EDI, CreatedBy = qEDIRecipReq.UpdatedBy, CreatedOn = DateTime.Now });
                }
                else
                {
                    SrvStatusCodeAll.Remove(qEDIRecipReq.idEDIRecipReqs, SHIPMENT_STATUS_COURIER_EDI, cEDI.idStatusCodesCourierEDI);
                }
            }
        }
        else if (Params.ct == UserControlParams.CourierType.NonCourierEDI)
        {
            foreach (RadComboBoxItem rad in comboxStatusCodes.Items)
            {
                clsStatusCodeNonCourierEDI cEDI = SrvStatusCodeNonCourierEDI.GetStatusCodes().Where(p => p.idStatusCodesNonCourierEDI == int.Parse(rad.Value)).FirstOrDefault();
                if (rad.Checked)
                {
                    SrvStatusCodeAll.UpdatetatusCodeAll(new clsStatusCodeAll() { idStatusCodesAll = idStatusCodesAll, idEDIRecipReqs = qEDIRecipReq.idEDIRecipReqs, idStatusCodes = int.Parse(rad.Value.ToString()), StatusCode = cEDI.StatusCode, idEDITranscationType = SHIPMENT_STATUS_NON_COURIER_EDI, CreatedBy = qEDIRecipReq.UpdatedBy, CreatedOn = DateTime.Now });
                }
                else
                {
                    SrvStatusCodeAll.Remove(qEDIRecipReq.idEDIRecipReqs, SHIPMENT_STATUS_NON_COURIER_EDI, cEDI.idStatusCodesNonCourierEDI);
                }
            }
        }
    }

    protected void UpdateControls(clsEDIRecipReq qEDIRecipReq)
    {
        comboBxFileFormat214.SelectedValue = qEDIRecipReq.idFileType.ToString();
        comboxCommunicationMethod.SelectedValue = (qEDIRecipReq.idCommunicationMethod == 0 )? "1" : qEDIRecipReq.idCommunicationMethod.ToString();
        comboxTriggerMechanism.SelectedValue = qEDIRecipReq.idTriggerMechanism.ToString();
        comboxTiming.SelectedValue = qEDIRecipReq.idTiming.ToString();

        if (Params.ct == UserControlParams.CourierType.CourierEDI)
        {
            List<clsStatusCodeAll> qStatusCode = SrvStatusCodeAll.GetStatusCodesAllByID(qEDIRecipReq.idEDIRecipReqs, SHIPMENT_STATUS_COURIER_EDI);
            foreach(RadComboBoxItem rad in comboxStatusCodes.Items)
            {
                var q = qStatusCode.Where(p => p.idStatusCodes == int.Parse(rad.Value)).FirstOrDefault();
                if(q != null)
                    rad.Checked = true;
                else
                    rad.Checked = false;
            }
        }
        else if (Params.ct == UserControlParams.CourierType.NonCourierEDI)
        {
            List<clsStatusCodeAll> qStatusCode = SrvStatusCodeAll.GetStatusCodesAllByID(qEDIRecipReq.idEDIRecipReqs, SHIPMENT_STATUS_NON_COURIER_EDI);
            foreach (RadComboBoxItem rad in comboxStatusCodes.Items)
            {
                var q = qStatusCode.Where(p => p.idStatusCodes == int.Parse(rad.Value)).FirstOrDefault();
                if (q != null)
                    rad.Checked = true;
                else
                    rad.Checked = false;
            }
        }

        txtBoxISA.Text = qEDIRecipReq.X12_ISA;
        txtBoxGS.Text = qEDIRecipReq.X12_GS;
        txtBoxQualifier.Text = qEDIRecipReq.X12_Qualifier;
        textBoxFTPAddress.Text = qEDIRecipReq.FTPAddress;
        textBoxUserName.Text = qEDIRecipReq.UserName;
        textBoxPassword.Text = qEDIRecipReq.Password;
        textBoxFolderPath.Text = qEDIRecipReq.FolderPath;
        textBoxEmail.Text = qEDIRecipReq.Email;
        TimeSpan? ts = new TimeSpan (qEDIRecipReq.TimeOfFile.Value.Hour, qEDIRecipReq.TimeOfFile.Value.Minute, 0);
        timeTimeofFile.SelectedTime = ts;

        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            txtBoxMultiDebug2.Text += 
                "UpdateControls- " + 
                RadPanelBar1.Items[0].Text + ": " + 
                comboxCommunicationMethod.SelectedText.ToString() + ": " + 
                comboxCommunicationMethod.SelectedValue.ToString() + "\r\n";
        }
        return;
    }
}