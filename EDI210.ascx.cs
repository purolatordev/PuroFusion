using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class UserControlParams
{
    public int idRequest { get; set; }
    public string FirstName { get; set; }
    public string Heading { get; set; }
    public int iRecords { get; set; }

    public IList<int> xTimes;
    public IList<string> xNames;
    public IList<string> yNames;

    public UserControlParams()
    {
        xTimes = new List<int>();
        xNames = new List<string>();
        yNames = new List<string>();
    }
    public UserControlParams(int iCount)
    {
        xTimes = new List<int>();
        xNames = new List<string>();
        yNames = new List<string>();
        iRecords = iCount;
        for(int i = 0; i < iCount; i++) 
        {
            xTimes.Add(100+i);
            xNames.Add("xNames: " + (100 + i).ToString());
            yNames.Add("yNames: " + (100 + i).ToString());
        }
    }
}

public partial class EDI210 : System.Web.UI.UserControl
{
    public event EventHandler RemoveUserControl; 
    public event EventHandler buttonClick;
    public UserControlParams Params = new UserControlParams();
    private string m_FirstName = string.Empty;
    public string FirstName
    {
        get { return m_FirstName; }
        set { m_FirstName = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //txtBoxTestValue.Text = (!string.IsNullOrEmpty(Params.FirstName) ? Params.FirstName :"nothing");
        //txtName.Text = (!string.IsNullOrEmpty(Params.Heading) ? Params.Heading : "nothing");
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
    protected void btnfirst_Click(object sender, EventArgs e)
    {
        //txtBoxTestValue.Text = "Button was pressed";
        int er = 0;
        er++;
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        RemoveUserControl(sender,e);
    }
}