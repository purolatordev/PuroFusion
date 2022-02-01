using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using context = System.Web.HttpContext;

public partial class EDI210 : System.Web.UI.UserControl
{
    const int INVOICE_COURIER_EDI = 3;
    const int SHIPMENT_STATUS_COURIER_EDI = 4;
    const int INVOICE_NON_COURIER_EDI = 5;
    const int SHIPMENT_STATUS_NON_COURIER_EDI = 6;
    const int PUROPOST_NON_COURIER_EDI = 7;

    public event EventHandler RemoveUserControl;
    public event EventHandler UserControlSaved;
    public event EventHandler buttonClick;
    public UserControlParams Params = new UserControlParams();
    public void LoadParams(UserControlParams p)
    {
        Params = p;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        clsEDIRecipReq qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqsByID(Params.idEDIRecipReqs);
        if (qEDIRecipReq != null && !IsPostBack)
        {
            GetFileFormats();
            GetCommunicationMethods();
            UpdateControls(qEDIRecipReq);
        }
        else if(Params.bNewDialog)
        {
            Params.CheckPassBacks(Params.idEDIRecipReqs,false);
            RemoveUserControl(sender, e);
            string strFileformat = comboBxFileFormat.SelectedValue;
            GetFileFormats();
            if (!String.IsNullOrEmpty(strFileformat))
            {
                comboBxFileFormat.SelectedValue = strFileformat;
            }
            string strCommunicationMethod = comboxCommunicationMethod.SelectedValue;
            GetCommunicationMethods();
            if (!String.IsNullOrEmpty(strCommunicationMethod))
            {
                comboxCommunicationMethod.SelectedValue = strCommunicationMethod;
            }
            UpdateControls(qEDIRecipReq);
        }
        SetFileFormatControls();
        SetCommunicationMethodControls();

        if (!string.IsNullOrEmpty(qEDIRecipReq.PanelTitle))
            RadPanelBar1.Items[0].Text = qEDIRecipReq.PanelTitle;
        else
            RadPanelBar1.Items[0].Text = "Record num: " + (Params.iRecordID + 1).ToString();

        RadPanelBar1.Items[0].Expanded = false;
        textBoxPanelTitle.Text = RadPanelBar1.Items[0].Text;
    }
    [MethodImpl(MethodImplOptions.NoInlining)]
    public string GetCurrentMethod()
    {
        var st = new StackTrace();
        var sf = st.GetFrame(1);
        return sf.GetMethod().Name;
    }
    private void SetFileFormatControls()
    {
        if (comboBxFileFormat.SelectedText.ToString().Contains("X12"))
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

    protected void Page_Init(object sender, EventArgs e)
    {
        //this. += new DetailsViewCommandEventHandler(RoomForm_ItemCommand);
    }
    void RoomForm_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            int er = 0;
            er++;
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetFileFormatControls();
    }
    protected void CommunicationMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCommunicationMethodControls();
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

    protected void btnfirst_Click(object sender, EventArgs e)
    {
        int er = 0;
        er++;
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        RemoveUserControl(sender,e);
    }
    protected void btnSubmit210Changes_Click(object sender, EventArgs e)
    {
        Save();

        UserControlSaved(sender, e);
    }

    private void Save()
    {
        int iCommMethod = int.Parse(comboxCommunicationMethod.SelectedValue);
        clsEDIRecipReq qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqsByID(Params.idEDIRecipReqs);
        if (qEDIRecipReq.idEDITranscationType != PUROPOST_NON_COURIER_EDI)
        {
            int iFileformat = int.Parse(comboBxFileFormat.SelectedValue);
            qEDIRecipReq.idFileType = iFileformat;
            qEDIRecipReq.X12_ISA = txtBoxISA.Text;
            qEDIRecipReq.X12_GS = txtBoxGS.Text;
            qEDIRecipReq.X12_Qualifier = txtBoxQualifier.Text;
        }
        qEDIRecipReq.idCommunicationMethod = iCommMethod;
        qEDIRecipReq.FTPAddress = textBoxFTPAddress.Text;
        qEDIRecipReq.UserName = textBoxUserName.Text;
        qEDIRecipReq.Password = textBoxPassword.Text;
        qEDIRecipReq.FolderPath = textBoxFolderPath.Text;
        qEDIRecipReq.Email = textBoxEmail.Text;
        qEDIRecipReq.PanelTitle = textBoxPanelTitle.Text;
        RadPanelBar1.Items[0].Text = qEDIRecipReq.PanelTitle;
        qEDIRecipReq.UpdatedBy = Session["userName"].ToString();
        qEDIRecipReq.UpdatedOn = DateTime.Now;
        SrvEDIRecipReq.Insert(qEDIRecipReq);
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
            comboBxFileFormat.DataSource = qFileTypes;
            comboBxFileFormat.DataTextField = "FileType";
            comboBxFileFormat.DataValueField = "idFileType";
            comboBxFileFormat.DataBind();
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
    }
    protected void UpdateControls(clsEDIRecipReq qEDIRecipReq)
    {
        if (qEDIRecipReq.idEDITranscationType == PUROPOST_NON_COURIER_EDI)
        {
            comboBxFileFormat.Visible = false;
            lblEDI210FileFormat.Visible = false;
            lblEDIIfX12.Visible = false;
        }
        else
        {
            comboBxFileFormat.SelectedValue = qEDIRecipReq.idFileType.ToString();
        }
        comboxCommunicationMethod.SelectedValue = qEDIRecipReq.idCommunicationMethod.ToString();
        txtBoxISA.Text = qEDIRecipReq.X12_ISA;
        txtBoxGS.Text = qEDIRecipReq.X12_GS;
        txtBoxQualifier.Text = qEDIRecipReq.X12_Qualifier;
        textBoxFTPAddress.Text = qEDIRecipReq.FTPAddress;
        textBoxUserName.Text = qEDIRecipReq.UserName;
        textBoxPassword.Text = qEDIRecipReq.Password;
        textBoxFolderPath.Text = qEDIRecipReq.FolderPath;
        textBoxEmail.Text = qEDIRecipReq.Email;
        return;
    }
}