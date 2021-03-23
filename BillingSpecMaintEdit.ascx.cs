using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BillingSpecMaintEdit : System.Web.UI.UserControl
{
    PuroTouchRepository repository = new PuroTouchRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        loadEmployees();
    }

    protected void loadEmployees()
    {
        try
        {
            List<ClsEmployee> emplist = repository.GetListClsEmployees();
            rddlEmployee.DataSource = emplist;
            rddlEmployee.DataTextField = "UserName";
            rddlEmployee.DataValueField = "idEmployee";
            rddlEmployee.DataBind();
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
        }
    }
}