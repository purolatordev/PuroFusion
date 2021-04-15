using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EDI214 : System.Web.UI.UserControl
{
    public event EventHandler RemoveUserControl2;
    public event EventHandler buttonClick;
    public UserControlParams Params = new UserControlParams();

    protected void Page_Load(object sender, EventArgs e)
    {
        SetFileFormatControls();
        SetCommunicationMethodControls();
        SetTimeOfFileControls();
        RadPanelBar1.Items[0].Text = "Record num: " + (Params.iRecordID + 1).ToString();
        RadPanelBar1.Items[0].Expanded = false;
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
        if (comboBxFileFormat.SelectedValue.ToString().Contains("X12"))
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
    }

    private void SetCommunicationMethodControls()
    {
        if (comboxCommunicationMethod.SelectedValue.ToString().ToLower().Contains("ftp"))
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
            if (comboxCommunicationMethod.SelectedValue.ToString().ToLower().Contains("email"))
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
        if (comboxTiming.SelectedValue.ToString().Contains("Once a Day"))
        {
            lblTimeofFile.Visible = true;
            textBoxTimeofFile.Visible = true;
        }
        else
        {
            lblTimeofFile.Visible = false;
            textBoxTimeofFile.Visible = false;
        }
    }

    protected void btnfirst_Click(object sender, EventArgs e)
    {
        int er = 0;
        er++;
    }
   
}