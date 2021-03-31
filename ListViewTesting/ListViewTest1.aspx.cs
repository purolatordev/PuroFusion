using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class ListViewTest1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RadListView1_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        RadListView1.DataSource = ListViewData;
    }
    List<SampleData> ListViewData = Enumerable.Range(1, 20).Select(x => new SampleData
    {
        Id = x,
        Name = "Name " + x,
        Team = "Team " + x % 3
    }).ToList();

    public class SampleData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
    }
}