using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for clsvwDiscoveryRequest
/// </summary>
public class clsvwDiscoveryRequest
{
    public int idRequest { get; set; }
    public bool flagNewRequest { get; set; }
    public string SalesRepName { get; set; }
    public string SalesRepEmail { get; set; }
    public int idOnBoardingphase { get; set; }
    public string District { get; set; }
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zipcode { get; set; }
    public string Country { get; set; }
    public string Commodity { get; set; }
    public decimal ProjectedRevenue { get; set; }
    public string CurrentSolution { get; set; }
    public string ProposedCustoms { get; set; }
    public DateTime? CallDate1 { get; set; }
    public DateTime? CallDate2 { get; set; }
    public DateTime? CallDate3 { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool ActiveFlag { get; set; }
    public int idNote { get; set; }
    public int idTaskType { get; set; }
    public DateTime? noteDate { get; set; }
    public int timespent { get; set; }
    public string publicnote { get; set; }
    public string privateNote { get; set; }
    public int idRequestSvc { get; set; }
    public int idShippingSvc { get; set; }
    public int volume { get; set; }
    public string TaskType { get; set; }
    public string serviceDesc { get; set; }
    public string OnBoardingPhase { get; set; }

	public clsvwDiscoveryRequest()
	{
    }

    public List<clsvwDiscoveryRequest> getallRequests()
    {
        PuroTouchSQLDataContext puroDB = new PuroTouchSQLDataContext();
        List<clsvwDiscoveryRequest> lst = new List<clsvwDiscoveryRequest>();

        lst = (from d in puroDB.vw_DiscoveryRequests
               select new clsvwDiscoveryRequest
               {
                   idRequest = d.idRequest,
                   SalesRepName = d.SalesRepName
               }).ToList();
        return lst;
    }
}