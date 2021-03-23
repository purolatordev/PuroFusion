using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


interface IPuroTouch : IDisposable
{
    List<ClsTaskType> GetTaskTypes();
    List<ClsITBA> GetITBAs();
    List<ClsDiscoveryRequest> GetAllDiscoveryRequests();
    List<ClsAppUsers> GetListClsAppUsers(int idPIApplication);
    List<ClsAppUsers> GetListClsAppUsers(string appName);

    List<ClsEmployee> GetListClsEmployees();
    List<ClsApp> GetListClsApps();
    List<ClsDiscoveryRequestSvcs> GetProposedServices(int idRequest);
    List<ClsDistrict> GetDistricts();
    List<ClsNotes> GetNotes(int DRid);
    int GetTotalTimeSpent(int requestID);
    List<ClsOnboardingPhase> GetOnboardingPhasesOnlyActive();
    List<ClsOnboardingPhase> GetOnboardingPhasesAll();
    List<ClsRelationshipName> GetRelationships();
    List<ClsShippingChannel> GetShippingChannels();
    List<ClsShippingService> GetServices();
    ClsZipCode GetZipInfo(string zipcode);
}




