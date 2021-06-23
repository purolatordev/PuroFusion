using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

public partial class DiscoveryTracker : System.Web.UI.Page
{
    public class SessionStorageProvider : IStateStorageProvider
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
        static string storageKey;

        public static string StorageProviderKey
        {
            set { storageKey = value; }
        }


        public void SaveStateToStorage(string key, string serializedState)
        {
            session[storageKey] = serializedState;
        }


        public string LoadStateFromStorage(string key)
        {
            string strstorageKey = "failed";
            try
            {
                if(session[storageKey] != null)
                    strstorageKey =  session[storageKey].ToString();
            }
            catch (Exception ex)
            {
                strstorageKey = ex.Message.ToString();
            }
            return strstorageKey;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        RadPersistenceManager1.StorageProvider = new SessionStorageProvider();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Session["userName"] == null || Session["appName"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            //Restore State to Grid if there is something to restore
            try
            {
                string user = Session["userName"].ToString();
                SessionStorageProvider.StorageProviderKey = user;
                RadPersistenceManager1.LoadState();
                rgRequests.Rebind();
            }
            catch (Exception ex)
            {
               
            }

            

              //Role Based Viewing
            string userRole = Session["userRole"].ToString().ToLower();
            if (userRole == "admin" || userRole == "itadmin" || userRole == "itmanager")
            {
                //show delete column
                rgRequests.MasterTableView.GetColumn("DeleteLink").Visible = true;
            }
            else
            {
                //Hide delete column
                rgRequests.MasterTableView.GetColumn("DeleteLink").Visible = false;
            }

        }
    }

    protected void rgRequests_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
      //  ClsDiscoveryRequest dr = new ClsDiscoveryRequest();
        PuroTouchRepository rep = new PuroTouchRepository();
        List<ClsDiscoveryRequest> oDRList = rep.GetAllDiscoveryRequests();
        rgRequests.DataSource = oDRList;
    }

    protected void rgRequests_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            //adding all the links to the hyperlink column, to navigate when it is clicked. 
            GridDataItem item = (GridDataItem)e.Item;

            //string val = item.GetDataKeyValue("idRequest").ToString();
            //int requestID = Convert.ToInt32(val);

            HyperLink hLink = (HyperLink)item["CustomerName"].Controls[0];
            hLink.ForeColor = System.Drawing.Color.Blue;
            ClsDiscoveryRequest row = (ClsDiscoveryRequest)item.DataItem;
            hLink.Attributes["onclick"] = "OpenWin('" + row.idRequest + "');";



            //if (item["flagNewRequest"].Text == "True")
            //{
            //    item["flagNewRequest"].Text = "Yes";
            //}
            //else
            //{
            //    item["flagNewRequest"].Text = "No";
            //}
                 

            
        }
    }

    protected void rgRequests_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            ClsDiscoveryRequest cr = new ClsDiscoveryRequest();           
            int rownum = e.Item.ItemIndex;
            GridDataItem item = (GridDataItem)e.Item;
            string val = item.GetDataKeyValue("idRequest").ToString();
            int requestID = Convert.ToInt32(val);
            ClsDiscoveryRequest currentrow = cr.GetDiscoveryRequest(requestID);
            string user = Session["userName"].ToString();
            cr.deActivateDiscoveryRequest(requestID,user);
            rgRequests.Rebind();

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
            e.Canceled = true;
        }
    }

    protected void rgRequests_ItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName == "EditRequest")
        {
            try
            {
                //Save State
                bool saveStateEnabled = true;
                Session["saveStateEnabled"] = saveStateEnabled;
                string user = Session["userName"].ToString();
                SessionStorageProvider.StorageProviderKey = user;
                RadPersistenceManager1.SaveState();
            }
             catch (Exception ex)
            {           
            }
           

            Response.Redirect("DiscoveryRequestForm.aspx?from=dt&requestID=" + e.CommandArgument);

        }

    }
}