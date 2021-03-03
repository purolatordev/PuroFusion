using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsOnboardingPhase
/// </summary>
public class ClsOnboardingPhase
{
    public int idOnboardingPhase { get; set; }
    public string OnboardingPhase { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public int? SortValue { get; set; }

    public string InsertOnboardingPhase(ClsOnboardingPhase data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblOnboardingPhase oNewRow = new tblOnboardingPhase()
            {
                //idTaskType = (Int32)data.idTaskType,
                OnboardingPhase = data.OnboardingPhase,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag,
                SortValue = data.SortValue
            };



            puroTouchContext.GetTable<tblOnboardingPhase>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();
            //newID = oNewRow.idTaskType;

        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string UpdateOnboardingPhase(ClsOnboardingPhase data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idOnboardingPhase > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblOnboardingPhase>()
                    where qdata.idOnboardingPhase == data.idOnboardingPhase
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblOnboardingPhase updRow in query)
                {

                    updRow.OnboardingPhase = data.OnboardingPhase;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idOnboardingPhase = data.idOnboardingPhase;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;
                    updRow.SortValue = data.SortValue;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Onboarding Phase with ID = " + "'" + data.idOnboardingPhase + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

   
}